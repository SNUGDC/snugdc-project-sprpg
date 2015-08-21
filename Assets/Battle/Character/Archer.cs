using Gem;
using LitJson;
using UnityEngine;

namespace SPRPG.Battle
{
	public struct ArcherArrowRainArguments
	{
		public readonly Hp DamagePerArrow;

		public ArcherArrowRainArguments(JsonData args)
		{
			DamagePerArrow = (Hp)args["DamagePerArrow"].GetNatural();
		}
	}

	public class ArcherPassive : Passive
	{
		public int Arrows { get; private set; }
		public bool IsArrowLeft { get { return Arrows > 0; } }
		public bool IsArrowMax { get { return Arrows >= _data.ArrowMax; } }

		private readonly ArcherSpecialBalanceData _data;
		private readonly Tick _rechargeCooltime;
		private Tick _rechargeTickLeft;

		public ArcherPassive()
		{
			_data = CharacterBalance._.Find(CharacterId.Archer).Special.ToObject<ArcherSpecialBalanceData>();
			Arrows = _data.ArrowInitial;
			_rechargeCooltime = _data.ArrowRechargeCooltime;
			_rechargeTickLeft = _rechargeCooltime;
		}

		public override void Tick()
		{
			if (IsArrowMax) return;
			if (--_rechargeTickLeft > 0) return;
			_rechargeTickLeft = _rechargeCooltime;
			++Arrows;
		}

		public void DecreaseArrow()
		{
			if (Arrows > 0)
				--Arrows;
			else
			{
				Debug.LogError("No more arrows!");
			}
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

	public sealed class ArcherAttackSkillActor : AttackSkillActor
	{
		public ArcherAttackSkillActor(SkillBalanceData data, Battle context, Character owner) : base(data, context, owner)
		{ }

		protected override void Perform()
		{
			var archerPassive = ((ArcherPassive)Owner.Passive);
			if (!archerPassive.IsArrowLeft)
			{
				Debug.LogError("No Arrows Left.");
				return;
			}
			base.Perform();
			archerPassive.DecreaseArrow();
		}
	}

	public sealed class ArcherArrowRainSkillActor : SingleDelayedPerformSkillActor
	{
		private ArcherPassive OwnerPassive { get { return (ArcherPassive)Owner.Passive; } }

		public readonly ArcherArrowRainArguments Arguments;

		public ArcherArrowRainSkillActor(SkillBalanceData data, Battle context, Character owner) : base(data, context, owner, (Tick)3, (Tick)7)
		{
			Arguments = new ArcherArrowRainArguments(data.Arguments);
		}

		protected override void Perform()
		{
			if (!OwnerPassive.IsArrowLeft)
			{
				Debug.LogError("No Arrows Left.");
				return;
			}
			var dmgValue = ((Hp)((int)Arguments.DamagePerArrow * OwnerPassive.Arrows));
			var dmg = new Damage(dmgValue);
			Owner.Attack(Context.Boss, dmg);
			OwnerPassive.RemoveAllArrows();
		}
	}
}