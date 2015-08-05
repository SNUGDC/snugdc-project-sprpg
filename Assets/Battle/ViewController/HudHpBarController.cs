namespace SPRPG.Battle.View
{
	public class HudHpBarController<T> where T : Pawn<T>
	{
		private readonly HudHpBar _view;
		private readonly Pawn<T> _pawn;
		
		public HudHpBarController(HudHpBar view, Pawn<T> pawn)
		{
			_view = view;
			_pawn = pawn;

			view.MaxHp = pawn.HpMax;
			pawn.OnHpChanged += OnHpChanged;
		}

		~HudHpBarController()
		{
			_pawn.OnHpChanged -= OnHpChanged;
		}

		private void OnHpChanged(T pawn, Hp hp, Hp oldHp)
		{
			_view.SetHp(pawn.Hp);
		}
	}
}
