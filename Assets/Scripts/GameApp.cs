
namespace WarChess
{
    /// <summary>
    /// 
    /// </summary>
    public class GameApp : Singleton<GameApp>
    {
        public static SoundManager SoundManager { get; set; }
        
        public static ControllerManager ControllerManager { get; set; }
        
        public static ViewManager ViewManager { get; set; }
        
        public static ConfigManager ConfigManager { get; set; }
        
        public static CameraManager CameraManager { get; set; }
        
        public static MessageCenter MessageCenter { get; set; }
        
        public static TimerManager TimerManager { get; set; }
        
        public static FightWorldManager FightManager { get; set; }
        
        public static MapManager MapManager { get; set; }
        
        public static GameDataManager DataManager { get; set; }
        
        public static UserInputManager InputManager { get; set; }
        
        public static CommandManager CommandManager { get; set; }
        
        public override void Init()
        {
            TimerManager = new TimerManager();
            
            SoundManager = new SoundManager();
            ControllerManager = new ControllerManager();
            ViewManager = new ViewManager();
            ConfigManager = new ConfigManager();
            CameraManager = new CameraManager();
            MessageCenter = new MessageCenter();
            
            MapManager = new MapManager();
            FightManager = new FightWorldManager();
            
            DataManager = new GameDataManager();
            InputManager = new UserInputManager();
            CommandManager = new CommandManager();
        }
        
        public override void Update(float dt)
        {
            TimerManager.OnUpdate(dt);
            FightManager.OnUpdate(dt);
            InputManager.OnUpdate(dt);
            CommandManager.OnUpdate(dt);
        }
    }
}