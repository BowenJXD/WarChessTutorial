using System;

namespace WarChess
{
    public class Enemy : ModelBase
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            
            data = GameApp.ConfigManager.GetConfigData("enemy").GetDataById(id);
            
            step = int.Parse(data["Step"]);
            attack = int.Parse(data["Attack"]);
            type = (EModelType) int.Parse(data["Type"]);
            maxHp = int.Parse(data["Hp"]);
            curHp = maxHp;
        }
        
        protected override void OnSelectCallback(object arg)
        {
            base.OnSelectCallback(arg);
            GameApp.ViewManager.Open(EViewType.EnemyDesView, this);
        }

        protected override void OnUnselectCallback(object arg)
        {
            base.OnUnselectCallback(arg);
            GameApp.ViewManager.Close(EViewType.EnemyDesView);
        }
    }
}