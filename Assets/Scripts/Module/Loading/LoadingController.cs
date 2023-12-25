using UnityEngine;
using UnityEngine.SceneManagement;
using Object = System.Object;

namespace WarChess
{
    /// <summary>
    /// Scene loading controller
    /// </summary>
    public class LoadingController : BaseController
    {
        protected AsyncOperation asyncOp;
        
        public LoadingController() : base()
        {
            GameApp.ViewManager.Register(EViewType.LoadingView, new ViewInfo()
            {
                prefabName = "LoadingView",
                parentTf = GameApp.ViewManager.canvasTf,
                controller = this,
                sortingOrder = 999, // ensure that the loading view is on top of everything
            });
            
            InitModuleEvent();
        }

        public override void InitModuleEvent()
        {
            base.InitModuleEvent();
            RegisterFunc(Defines.OpenLoadingView, LoadSceneCallback);
        }
        
        void LoadSceneCallback(object[] args)
        {
            LoadingModel loadingModel = (LoadingModel) args[0];
            SetModel(loadingModel);
            
            // Open loading view
            GameApp.ViewManager.Open(EViewType.LoadingView, loadingModel);
            
            // Load scene
            asyncOp = SceneManager.LoadSceneAsync(loadingModel.sceneName);
            
            asyncOp.completed += OnLoadedEndCallback;
        }
        
        void OnLoadedEndCallback(AsyncOperation op)
        {
            op.completed -= OnLoadedEndCallback;
            
            // delay a bit
            GameApp.TimerManager.Register(0.25f, () =>
            {
                GetModel<LoadingModel>().callback?.Invoke(); // execute callback
            
                GameApp.ViewManager.Close(EViewType.LoadingView); // close loading view
            });
        }
    }
}