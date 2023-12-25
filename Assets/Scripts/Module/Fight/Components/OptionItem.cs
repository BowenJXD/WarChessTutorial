using System;
using UnityEngine;
using UnityEngine.UI;

namespace WarChess
{
    public class OptionItem : MonoBehaviour
    {
        protected OptionData opData;
        
        public void Init(OptionData data)
        {
            opData = data;
        }
        
        void Start()
        {
            GetComponent<Button>().onClick.AddListener(() =>
            {
                GameApp.MessageCenter.PostTempEvent(opData.eventName); // post event in opData
                GameApp.ViewManager.Close(EViewType.SelectOptionView);
            });
            
            transform.Find("txt").GetComponent<Text>().text = opData.name;
        }
    }
}