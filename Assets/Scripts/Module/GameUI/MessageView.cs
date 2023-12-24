using System;
using UnityEngine.UI;

namespace WarChess
{
    public class MessageInfo
    {
        public string msgTxt;
        public Action okCallback;
        public Action noCallback;
    }

    /// <summary>
    /// Message View
    /// </summary>
    public class MessageView : BaseView
    {
        protected MessageInfo info;
        
        protected override void OnAwake()
        {
            base.OnAwake();
            Find<Button>("okBtn").onClick.AddListener(OnOkBtn);
            Find<Button>("noBtn").onClick.AddListener(OnNoBtn);
        }

        public override void Open(params object[] args)
        {
            base.Open(args);
            info = (MessageInfo) args[0];
            Find<Text>("content/txt").text = info.msgTxt;
        }

        void OnOkBtn()
        {
            info.okCallback?.Invoke();
            GameApp.ViewManager.Close(ViewId); // close self
        }

        void OnNoBtn()
        {
            info.noCallback?.Invoke();
            GameApp.ViewManager.Close(ViewId); // close self
        }
    }
}