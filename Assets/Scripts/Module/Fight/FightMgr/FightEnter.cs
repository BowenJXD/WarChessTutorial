namespace WarChess
{
    /// <summary>
    /// Processes logics needed when enter fight
    /// </summary>
    public class FightEnter : FightUnitBase
    {
        public override void Init()
        {
            base.Init();
            GameApp.MapManager.Init();
            GameApp.FightManager.EnterFight();
        }
    }
}