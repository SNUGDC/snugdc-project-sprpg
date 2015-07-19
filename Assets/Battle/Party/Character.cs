using Gem;

namespace SPRPG.Battle
{
	public delegate void OnHpChanged(Hp cur, Hp old);
	public delegate void OnDead();

	public class Character
	{
		public CharacterId Id { get { return Data.Id; } }

		private readonly CharacterData _data;
		public CharacterData Data { get { return _data; } }

		public bool IsAlive { get { return Hp > 0; } }

		private Hp _hp;
		public Hp Hp
		{
			get { return _hp; }
			private set
			{
				value = (Hp)((int)value).Clamp(0, (int)HpMax);
				if (_hp == value) return;

				var oldHp = _hp;
				_hp = value;

				if (OnHpChanged != null) 
					OnHpChanged(_hp, oldHp);

				if (value == 0 && OnDead != null)
					OnDead();
			}
		}

		public readonly Hp HpMax;

		public readonly Passive Passive;

		private readonly SkillManager _skillManager;
		public SkillManager SkillManager { get { return _skillManager; } }

		private Tick _evadeDurationLeft;

		public OnHpChanged OnHpChanged;
		public OnDead OnDead;

		public Character(CharacterData data)
		{
			_data = data;
			Hp = HpMax = data.Stats.Hp.ToValue();
			Passive = PassiveFactory.Create(data.Passive);
			_skillManager = new SkillManager(data.SkillSet, this);
		}

		public void BeforeTurn()
		{
			if (CheckEvadeDuration())
				_evadeDurationLeft -= 1;
		}

		public void AfterTurn()
		{
		}

		public bool CheckEvadeDuration()
		{
			return _evadeDurationLeft > 0;
		}

		public void Hit(Damage dmg)
		{
			if (CheckEvadeDuration())
				return;
			Hp -= dmg.Value;
		}

		public void Heal(Hp val)
		{
			Hp += (int)val;
		}

		public void Evade(Tick duration)
		{
			_evadeDurationLeft = duration;
		}

		public void PerformSkill(SkillSlot idx)
		{
			_skillManager.Perform(idx);
		}
	}
}