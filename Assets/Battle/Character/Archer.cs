using Gem;
using UnityEngine;

namespace SPRPG.Battle
{
	public struct ArcherEvadeShotArguments
	{
		public Tick Duration;
		public Damage Damage;

		public string Describe(string format)
		{
			format = SkillDescriptorHelper.Replace(format, "Duration", Duration);
			format = SkillDescriptorHelper.Replace(format, "Damage", Damage.Value);
			return format;
		}
	}

	public struct ArcherWeakPointArguments
	{
		public Damage Damage;
		public Damage DamageOnStatusCondition;

		public string Describe(string format)
		{
			format = SkillDescriptorHelper.Replace(format, "Damage", Damage.Value);
			format = SkillDescriptorHelper.Replace(format, "DamageOnStatusCondition", DamageOnStatusCondition.Value);
			return format;
		}
	}

	public struct ArcherArrowRainArguments
	{
		public Hp DamagePerArrow;
	}

	public class ArcherPassive : Passive
	{
		public int Arrows { get; private set; }
		public bool IsArrowLeft { get { return Arrows > 0; } }
		public bool IsArrowMax { get { return Arrows >= _data.ArrowMax; } }

		private readonly ArcherSpecialBalanceData _data;
		private readonly Tick _rechargeCooltime;
		private Tick _rechargeTickLeft;

		public ArcherPassive(Battle context, Character owner) : base(context, owner)
		{
			_data = CharacterBalance._.Find(CharacterId.Archer).Special.ToObject<ArcherSpecialBalanceData>();
			Arrows = _data.ArrowInitial;
			_rechargeCooltime = _data.ArrowRechargeCooltime;
			_rechargeTickLeft = _rechargeCooltime;
		}

		public bool CheckArrowLeft()
		{
			if (!IsArrowLeft)
			{
				Debug.LogError("No Arrows Left.");
				return false;
			}

			return true;
		}

		public override void Tick()
		{
			if (Owner == Context.Party.Leader) return;
			if (IsArrowMax) return;
			if (--_rechargeTickLeft > 0) return;
			_rechargeTickLeft = _rechargeCooltime;
			TryChargeArrow();
		}

		public bool TryChargeArrow()
		{
			if (IsArrowMax)
				return false;
			++Arrows;
			return true;
		}

		public bool TryDecreaseArrow()
		{
			if (!IsArrowLeft) 
				return false;
			--Arrows;
			return true;
		}

		public bool RemoveAllArrows()
		{
			var ret = Arrows > 0;
			Arrows = 0;
			return ret;
		}

#if UNITY_EDITOR
		public override void OnInspectorGUI()
		{
			GUILayout.Label("arrow left: " + Arrows);
			GUILayout.Label("arrow tick left: " + _rechargeTickLeft);
		}
#endif
	}

	public sealed class ArcherReloadSkillActor : SkillActor 
	{
		public ArcherReloadSkillActor(SkillBalanceData data, Battle context, Character owner) : base(data, context, owner)
		{ }

		protected override void DoStart()
		{
			var archerPassive = ((ArcherPassive)Owner.Passive);
			archerPassive.TryChargeArrow();
			Stop();
		}
	}

	public sealed class ArcherAttackSkillActor : AttackSkillActor
	{
		public ArcherAttackSkillActor(SkillBalanceData data, Battle context, Character owner) : base(data, context, owner)
		{ }

		protected override void Perform()
		{
			var archerPassive = ((ArcherPassive)Owner.Passive);
			if (!archerPassive.TryDecreaseArrow()) return;
			base.Perform();
		}
	}

	public sealed class ArcherEvadeShotSkillActor : SingleDelayedPerformSkillActor
	{
		private readonly ArcherEvadeShotArguments _arguments;

		public ArcherEvadeShotSkillActor(SkillBalanceData data, Battle context, Character owner) : base(data, context, owner)
		{
			_arguments = data.Arguments.ToObject<ArcherEvadeShotArguments>();
		}

		protected override void Perform()
		{
			var archerPassive = ((ArcherPassive)Owner.Passive);
			if (!archerPassive.TryDecreaseArrow()) return;
			Owner.Attack(Context.Boss, _arguments.Damage);
			Owner.Evade(_arguments.Duration);
		}
	}

	public sealed class ArcherWeakPointAttackActor : SingleDelayedPerformSkillActor
	{
		private readonly ArcherWeakPointArguments _arguments;

		public ArcherWeakPointAttackActor(SkillBalanceData data, Battle context, Character owner) : base(data, context, owner)
		{
			_arguments = data.Arguments.ToObject<ArcherWeakPointArguments>();
		}

		protected override void Perform()
		{
			var archerPassive = ((ArcherPassive)Owner.Passive);
			if (!archerPassive.TryDecreaseArrow()) return;
			var damage = Context.Boss.HasSomeStatusCondition ? _arguments.DamageOnStatusCondition : _arguments.Damage;
			Owner.Attack(Context.Boss, damage);
		}
	}

	public sealed class ArcherArrowRainSkillActor : SingleDelayedPerformSkillActor
	{
		private ArcherPassive OwnerPassive { get { return (ArcherPassive)Owner.Passive; } }

		public readonly ArcherArrowRainArguments Arguments;

		public ArcherArrowRainSkillActor(SkillBalanceData data, Battle context, Character owner) : base(data, context, owner, (Tick)3, (Tick)7)
		{
			Arguments = data.Arguments.ToObject<ArcherArrowRainArguments>();
		}

		protected override void Perform()
		{
			if (!OwnerPassive.IsArrowLeft) return;
			var dmgValue = ((Hp)((int)Arguments.DamagePerArrow * OwnerPassive.Arrows));
			var dmg = new Damage(dmgValue);
			Owner.Attack(Context.Boss, dmg);
			OwnerPassive.RemoveAllArrows();
		}
	}
}