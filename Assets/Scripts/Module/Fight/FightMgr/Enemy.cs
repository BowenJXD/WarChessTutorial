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
            type = int.Parse(data["Type"]);
            maxHp = int.Parse(data["Hp"]);
            curHp = maxHp;
        }
        
        protected override void OnSelectCallback(object obj)
        {
            base.OnSelectCallback(obj);
            GameApp.ViewManager.Open(EViewType.EnemyDesView, this);
        }

        protected override void OnUnselectCallback(object obj)
        {
            base.OnUnselectCallback(obj);
            GameApp.ViewManager.Close(EViewType.EnemyDesView);
        }
    }
}