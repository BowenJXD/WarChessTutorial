using System;
using UnityEngine;

namespace WarChess
{
    /// <summary>
    /// Inherits from mono, needs to be attached to the game object, not destroying when switching scenes
    /// </summary>
    public class GameScene : MonoBehaviour
    {
        public Texture2D mouseTxt;
        protected float dt;
        protected static bool isLoaded = false;
        
        void Awake()
        {
            if (isLoaded)
            {
                Destroy(gameObject);
            }
            else
            {
                isLoaded = true;
                DontDestroyOnLoad(gameObject);
                GameApp.Instance.Init();
            }
        }

        void Start()
        {
            Cursor.SetCursor(mouseTxt, Vector2.zero, CursorMode.Auto);
            
            RegisterConfigs();
            
            GameApp.ConfigManager.LoadAllConfigs();

            GameApp.SoundManager.PlayBGM("login");
            
            RegisterModule();
            InitModule();
        }
        
        /// <summary>
        /// Register controllers
        /// </summary>
        void RegisterModule()
        {
            GameApp.ControllerManager.Register(EControllerType.GameUI, new GameUIController());
            GameApp.ControllerManager.Register(EControllerType.Game, new GameController());
            GameApp.ControllerManager.Register(EControllerType.Loading, new LoadingController());
            GameApp.ControllerManager.Register(EControllerType.Level, new LevelController());
            GameApp.ControllerManager.Register(EControllerType.Fight, new FightController());
        }

        /// <summary>
        /// Initialize controllers
        /// </summary>
        void InitModule()
        {
            GameApp.ControllerManager.InitAllModules();
        }

        /// <summary>
        /// Register config files
        /// </summary>
        void RegisterConfigs()
        {
            string[] configFiles = new string[]
            {
                "enemy",
                "level",
                "option",
                "player",
                "role",
                "skill",
            };

            foreach (var fileName in configFiles)
            {
                GameApp.ConfigManager.Register(fileName, new ConfigData(fileName));
            }
        }
        
        void Update()
        {
            dt = Time.deltaTime;
            GameApp.Instance.Update(dt);
        }
    }
}