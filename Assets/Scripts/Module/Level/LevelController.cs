namespace WarChess
{
    public class LevelController : BaseController
    {
        public LevelController() : base()
        {
            SetModel(new LevelModel());
            
            GameApp.ViewManager.Register(EViewType.SelectLevelView, new ViewInfo()
            {
                prefabName = "SelectLevelView",
                parentTf = GameApp.ViewManager.canvasTf,
                controller = this,
                sortingOrder = 1,
            });
            
            InitModuleEvent();
            InitGlobalEvent();
        }

        public override void Init()
        {
            base.Init();
            model.Init();
        }

        public override void InitModuleEvent()
        {
            RegisterFunc(Defines.OpenSelectLevelView, OpenSelectLevelViewCallback);
        }
        
        void OpenSelectLevelViewCallback(object[] args)
        {
            GameApp.ViewManager.Open(EViewType.SelectLevelView, args);
        }
        
        public override void InitGlobalEvent()
        {
            GameApp.MessageCenter.AddEvent(Defines.ShowLevelDesEvent, OnShowLevelDesCallback);
            GameApp.MessageCenter.AddEvent(Defines.HideLevelDesEvent, OnHideLevelDesCallback);
        }

        public override void RemoveGlobalEvent()
        {
            GameApp.MessageCenter.RemoveEvent(Defines.ShowLevelDesEvent, OnShowLevelDesCallback);
            GameApp.MessageCenter.RemoveEvent(Defines.HideLevelDesEvent, OnHideLevelDesCallback);
        }
        
        void OnShowLevelDesCallback(object arg)
        {
            LevelModel levelModel = GetModel<LevelModel>();
            levelModel.current = levelModel.GetLevelData(int.Parse(arg.ToString()));
            
            GameApp.ViewManager.GetView<SelectLevelView>((int)EViewType.SelectLevelView).ShowLevelDes();
        }

        void OnHideLevelDesCallback(object arg)
        {
            GameApp.ViewManager.GetView<SelectLevelView>((int)EViewType.SelectLevelView)?.HideLevelDes();
        }

    }
}