using System.Collections.Generic;

namespace WarChess
{
    public class Hero : ModelBase
    {
        public void Init(Dictionary<string, string> initData, int row, int col)
        {
            data = initData;
            rowIndex = row;
            colIndex = col;
            id = int.Parse(data["Id"]);
            step = int.Parse(data["Step"]);
            attack = int.Parse(data["Attack"]);
            type = int.Parse(data["Type"]);
            maxHp = int.Parse(data["Hp"]);
            curHp = maxHp;
        }
        
        protected override void OnSelectCallback(object obj)
        {
            base.OnSelectCallback(obj);
            GameApp.ViewManager.Open(EViewType.HeroDesView, this);
        }

        protected override void OnUnselectCallback(object obj)
        {
            base.OnUnselectCallback(obj);
            GameApp.ViewManager.Close(EViewType.HeroDesView);
        }
    }
}