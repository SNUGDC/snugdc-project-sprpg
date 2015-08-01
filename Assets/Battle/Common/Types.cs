using System.Collections.Generic;

namespace SPRPG.Battle
{
	public enum InputType
	{
		Shift,
		Skill,
	}

	public enum InputGrade
	{
		Good, Bad,
	}

	public enum OriginalPartyIdx { _1 = PartyIdx._1, _2 = PartyIdx._2, _3 = PartyIdx._3, }
	public enum ShiftedPartyIdx { _1 = PartyIdx._1, _2 = PartyIdx._2, _3 = PartyIdx._3, }

	public struct StatusConditionTest
	{
		public StatusConditionType Type;
		public Percentage Percentage;
	}

	public static partial class BattleHelper
	{
		public static IEnumerable<OriginalPartyIdx> GetOriginalPartyIdxEnumerable()
		{
			yield return OriginalPartyIdx._1;
			yield return OriginalPartyIdx._2;
			yield return OriginalPartyIdx._3;
		}

		public static IEnumerable<ShiftedPartyIdx> GetShiftedPartyIdxEnumerable()
		{
			yield return ShiftedPartyIdx._1;
			yield return ShiftedPartyIdx._2;
			yield return ShiftedPartyIdx._3;
		}

		public static int ToArrayIndex(this OriginalPartyIdx thiz)
		{
			return ((PartyIdx)thiz).ToArrayIndex();
		}

		public static int ToArrayIndex(this ShiftedPartyIdx thiz)
		{
			return ((PartyIdx)thiz).ToArrayIndex();
		}
	}
}
