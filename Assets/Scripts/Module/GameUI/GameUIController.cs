using UnityEngine;
using Object = System.Object;

namespace WarChess
{
    /// <summary>
    /// Process some common user interfaces (setting, help, start will be registered via this controller)
    /// </summary>
    public class GameUIController : BaseController
    {
        public GameUIController() : base()
        {
            Transform parent = GameApp.ViewManager.canvasTf;
            
            // Start Game view
            GameApp.ViewManager.Register(EViewType.StartView, new ViewInfo()
            {
                prefabName = "StartView",
                parentTf = parent,
                controller = this,
            });
            
            // Setting view
            GameApp.ViewManager.Register(EViewType.SetView, new ViewInfo()
            {
                prefabName = "SetView",
                parentTf = parent,
                controller = this,
                sortingOrder = 1, // ensure that the setting view is on top of the start view
            });
            
            // Message view
            GameApp.ViewManager.Register(EViewType.MessageView, new ViewInfo()
            {
                prefabName = "MessageView",
                parentTf = parent,
                controller = this,
                sortingOrder = 999, // ensure that the message view is on top of everything
            });
            
            InitModuleEvent();
            InitGlobalEvent();
        }

        public override void InitModuleEvent()
        {
            RegisterFunc(Defines.OpenStartView, args => GameApp.ViewManager.Open(EViewType.StartView, args));
            RegisterFunc(Defines.OpenSetView, args => GameApp.ViewManager.Open(EViewType.SetView, args));
            RegisterFunc(Defines.OpenMessageView, args => GameApp.ViewManager.Open(EViewType.MessageView, args));
        }

        public override void InitGlobalEvent()
        {
            base.InitGlobalEvent();
        }
    }
}