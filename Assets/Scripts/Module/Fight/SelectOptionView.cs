using System.Collections.Generic;
using UnityEngine;

namespace WarChess
{
    public class SelectOptionView : BaseView
    {
        protected Dictionary<int, OptionItem> opItems;
        protected Transform tf;

        public override void InitData()
        {
            base.InitData();
            opItems = new Dictionary<int, OptionItem>();

            if (Controller.GetModel() is not FightModel fightModel) return;
            List<OptionData> ops = fightModel.options;
            tf = Find("bg/grid").transform;
            GameObject prefabObj = Find("bg/grid/item");

            for (int i = 0; i < ops.Count; i++)
            {
                GameObject go = Instantiate(prefabObj, tf);
                go.SetActive(false);
                OptionItem item = go.AddComponent<OptionItem>();
                item.Init(ops[i]);
                opItems.Add(ops[i].id, item);
            }
        }

        public override void Open(params object[] args)
        {
            // first argument is the option id that corresponds to the hero's event string, need to be split (-)
            // second argument is the character's position
            // e.g. Event 1001-1002-1005
            string[] events = args[0].ToString().Split("-");
            tf.position = (Vector2)args[1];
            foreach (var item in events)
            {
                int id = int.Parse(item);
                opItems[id].gameObject.SetActive(true);
            }
        }
    }
}