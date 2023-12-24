using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WarChess
{
    /// <summary>
    /// Select hero view
    /// </summary>
    public class FightSelectHeroView : BaseView
    {
        protected override void OnAwake()
        {
            base.OnAwake();
            Find<Button>("bottom/startBtn").onClick.AddListener(OnFightBtn);
        }

        // enter player's turn after selecting heroes
        void OnFightBtn()
        {
            // prompt if no hero is selected
            if (GameApp.FightManager.heroes.Count == 0)
            {
                Debug.Log("No hero selected");
            }
            else
            {
                GameApp.ViewManager.Close(ViewId);
                
                GameApp.FightManager.ChangeState(EFightState.Player);
            }
        }

        public override void Open(params object[] args)
        {
            base.Open(args);

            GameObject prefabObj = Find("bottom/grid/item");
            Transform gridTf = Find<Transform>("bottom/grid");
            
            foreach (var hero in GameApp.DataManager.heroes)
            {
                Dictionary<string, string> data = GameApp.ConfigManager.GetConfigData("player").GetDataById(hero);

                GameObject obj = Instantiate(prefabObj, gridTf);
                
                obj.SetActive(true);
                
                HeroItem item = obj.AddComponent<HeroItem>();
                item.Init(data);
            }
        }
    }
}