namespace WarChess
{
    /// <summary>
    /// Player's turn
    /// </summary>
    public class FightPlayerUnit : FightUnitBase
    {
        public override void Init()
        {
            base.Init();
            GameApp.ViewManager.Open(EViewType.TipView, "Player's turn");
        }
    }
}