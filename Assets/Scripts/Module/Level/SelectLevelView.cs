using UnityEngine.UI;

namespace WarChess
{
    /// <summary>
    /// Level selecting info view
    /// </summary>
    public class SelectLevelView : BaseView
    {
        protected override void OnStart()
        {
            base.OnStart();
            Find<Button>("close").onClick.AddListener(OnCloseBtn);
            Find<Button>("level/fightBtn").onClick.AddListener(OnFightBtn);
        } 

        void OnCloseBtn()
        {
            GameApp.ViewManager.Close(ViewId);
            
            LoadingModel loadingModel = new LoadingModel()
            {
                sceneName = "game",
                callback = () =>
                {
                    Controller.ApplyControllerFunc(EControllerType.GameUI, Defines.OpenStartView);
                },
            };
            Controller.ApplyControllerFunc(EControllerType.Loading, Defines.OpenLoadingView, loadingModel);
        }

        /// <summary>
        /// Show level description view
        /// </summary>
        public void ShowLevelDes()
        {
            Find("level").SetActive(true);
            LevelData current = Controller.GetModel<LevelModel>().current;
            Find<Text>("level/name/txt").text = current.name;
            Find<Text>("level/des/txt").text = current.description;
            
        }
        
        /// <summary>
        /// Hide level description view
        /// </summary>
        public void HideLevelDes()
        {
            Find("level")?.SetActive(false);
        }

        /// <summary>
        /// Switch to fight scene
        /// </summary>
        void OnFightBtn()
        {
            // close current view and reset camera position
            GameApp.ViewManager.Close(ViewId);
            GameApp.CameraManager.ResetPos();
            
            LoadingModel loadingModel = new LoadingModel()
            {
                sceneName = Controller.GetModel<LevelModel>().current.sceneName, // the fight scene to load
                callback = () =>
                {
                    // after loading map scene, open fight view
                    Controller.ApplyControllerFunc(EControllerType.Fight, Defines.BeginFight);
                },
            };
            Controller.ApplyControllerFunc(EControllerType.Loading, Defines.OpenLoadingView, loadingModel);
        }
    }
}