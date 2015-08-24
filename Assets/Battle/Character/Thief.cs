using System;
using Gem;

namespace SPRPG.Battle
{
	public class ThiefPassive : Passive
	{
		public bool ShouldActTwiceOnNextSkill { get; private set; }

		public ThiefPassive(Battle context, Character owner) : base(context, owner)
		{
			Owner.OnMissHit += OnMissHit;
			Owner.OnEvade += OnEvade;
		}

		~ThiefPassive()
		{
			Owner.OnMissHit -= OnMissHit;
			Owner.OnEvade -= OnEvade;
		}

		public void SetNoActTwiceOnNextSkill()
		{
			ShouldActTwiceOnNextSkill = false;
		}

		private void OnMissHit(Character character)
		{
			ShouldActTwiceOnNextSkill = true;
		}

		private void OnEvade(Character character, Damage damage)
		{
			ShouldActTwiceOnNextSkill = true;
		}
	}

	public static class ThiefHelper
	{
		public static Job ScheduleSecondActAndResetIfFlagOn(Battle context, Character character, Tick delay, Action action)
		{
			var passive = (ThiefPassive)character.Passive;
			if (!passive.ShouldActTwiceOnNextSkill) return null;
			passive.SetNoActTwiceOnNextSkill();
			return context.AddPlayerSkill(delay, action);
		}
	}

	public abstract class ThiefTwiceSkillActor : SingleDelayedPerformSkillActor
	{
		private bool DidPerformedTwice { get { return _secondPerformJob == null; } }
		private Job _secondPerformJob;

		protected ThiefTwiceSkillActor(SkillBalanceData data, Battle context, Character owner) : base(data, context, owner)
		{
		}

		protected override void DoStart()
		{
			base.DoStart();
			_secondPerformJob = ThiefHelper.ScheduleSecondActAndResetIfFlagOn(Context, Owner, (Tick)6, Perform);
		}

		protected override void DoCancel()
		{
			base.DoCancel();
			if (_secondPerformJob != null)
			{
				_secondPerformJob.Cancel();
				_secondPerformJob = null;
			}
		}

		public override object MakeViewArgument()
		{
			return DidPerformedTwice;
		}
	}

	public class ThiefAttackAndTestStunSkillActor : ThiefTwiceSkillActor
	{
		private readonly AttackAndTestStunArguments _arguments;

		public ThiefAttackAndTestStunSkillActor(SkillBalanceData data, Battle context, Character owner) : base(data, context, owner)
		{
			_arguments = data.Arguments.ToObject<AttackAndTestStunArguments>();
		}

		protected override void Perform()
		{
			Owner.Attack(Context.Boss, _arguments.Damage);
			Context.Boss.TestAndGrant(_arguments.StunTest);
		}
	}

	public class ThiefAttackAndGrantStatusConditionSkillActor : ThiefTwiceSkillActor
	{
		private readonly AttackAndGrantStatusConditionArguments _arguments;

		public ThiefAttackAndGrantStatusConditionSkillActor(SkillBalanceData data, Battle context, Character owner) : base(data, context, owner)
		{
			_arguments = data.Arguments.ToObject<AttackAndGrantStatusConditionArguments>();
		}

		protected override void Perform()
		{
			Owner.Attack(Context.Boss, _arguments.Damage);
			Context.Boss.TestAndGrant(_arguments.StatusConditionTest);
		}
	}

	public class ThiefAssassinationSkillActor : ThiefTwiceSkillActor
	{
		private readonly AttackSkillArguments _arguments;

		public ThiefAssassinationSkillActor(SkillBalanceData data, Battle context, Character owner) : base(data, context, owner)
		{
			_arguments = new AttackSkillArguments(data.Arguments);
		}

		protected override void Perform()
		{
			Owner.Attack(Context.Boss, _arguments.Damage);
		}
	}
}