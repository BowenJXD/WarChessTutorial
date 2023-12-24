using UnityEngine;
using UnityEngine.UI;

namespace WarChess
{
    /// <summary>
    /// Start Game View
    /// </summary>
    public class StartView : BaseView
    {
        protected override void OnAwake()
        {
            base.OnAwake();
            
            Find<Button>("startBtn").onClick.AddListener(OnStartGameBtn);
            Find<Button>("setBtn").onClick.AddListener(OnSetBtn);
            Find<Button>("quitBtn").onClick.AddListener(OnQuitGameBtn);
        }

        void OnStartGameBtn()
        {
            GameApp.ViewManager.Close(ViewId);
            
            LoadingModel loadingModel = new LoadingModel()
            {
                sceneName = "map",
                callback = () =>
                {
                    Controller.ApplyControllerFunc(EControllerType.Level, Defines.OpenSelectLevelView);
                },
            };
            Controller.ApplyControllerFunc(EControllerType.Loading, Defines.OpenLoadingView, loadingModel);
        }
        
        void OnSetBtn()
        {
            ApplyFunc(Defines.OpenSetView);
        }
        
        void OnQuitGameBtn()
        {
            Controller.ApplyControllerFunc(EControllerType.GameUI, Defines.OpenMessageView, new MessageInfo()
            {
                msgTxt = "Are you sure you want to quit the game?",
                okCallback = Application.Quit,
            });
        }
    }
}