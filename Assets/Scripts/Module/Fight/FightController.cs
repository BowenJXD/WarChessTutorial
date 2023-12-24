using System;

namespace WarChess
{
    /// <summary>
    /// Controls fight related views and events
    /// </summary>
    public class FightController : BaseController
    {
        public FightController() : base()
        {
            GameApp.ViewManager.Register(EViewType.FightView, new ViewInfo()
            {
                prefabName = "FightView",
                parentTf = GameApp.ViewManager.canvasTf,
                controller = this,
            });
            
            GameApp.ViewManager.Register(EViewType.FightSelectHeroView, new ViewInfo()
            {
                prefabName = "FightSelectHeroView",
                parentTf = GameApp.ViewManager.canvasTf,
                controller = this,
                sortingOrder = 1,
            });
            
            GameApp.ViewManager.Register(EViewType.DragHeroView, new ViewInfo()
            {
                prefabName = "DragHeroView",
                parentTf = GameApp.ViewManager.worldCanvasTf, // use world canvas
                controller = this,
                sortingOrder = 2,
            });
            
            GameApp.ViewManager.Register(EViewType.TipView, new ViewInfo()
            {
                prefabName = "TipView",
                parentTf = GameApp.ViewManager.canvasTf,
                controller = this,
                sortingOrder = 2,
            });
            
            GameApp.ViewManager.Register(EViewType.HeroDesView, new ViewInfo()
            {
                prefabName = "HeroDesView",
                parentTf = GameApp.ViewManager.canvasTf,
                controller = this,
                sortingOrder = 2,
            });
            
            GameApp.ViewManager.Register(EViewType.EnemyDesView, new ViewInfo()
            {
                prefabName = "EnemyDesView",
                parentTf = GameApp.ViewManager.canvasTf,
                controller = this,
                sortingOrder = 2,
            });
            
            InitModuleEvent();
        }

        public override void InitModuleEvent()
        {
            RegisterFunc(Defines.BeginFight, OnBeginFightCallback);
        }

        void OnBeginFightCallback(Object[] args)
        {
            GameApp.FightManager.ChangeState(EFightState.Enter);
            
            GameApp.ViewManager.Open(EViewType.FightView, args);
            GameApp.ViewManager.Open(EViewType.FightSelectHeroView, args);
        }
    }
}