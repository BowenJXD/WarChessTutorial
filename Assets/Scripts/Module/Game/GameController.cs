namespace WarChess
{
    /// <summary>
    /// Main Controller of the game, processes starting, saving and quitting logics 
    /// </summary>
    public class GameController : BaseController
    {
        public GameController() : base()
        {
            InitModuleEvent();
            InitGlobalEvent();
        }

        public override void Init()
        {
            ApplyControllerFunc(EControllerType.GameUI, Defines.OpenStartView);
        }
    }
}