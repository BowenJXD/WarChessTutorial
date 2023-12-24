using UnityEngine.UI;

namespace WarChess
{
    /// <summary>
    /// volume setting view
    /// </summary>
    public class SetView : BaseView
    {
        protected override void OnAwake()
        {
            base.OnAwake();
            Find<Button>("bg/closeBtn").onClick.AddListener(OnCloseBtn);
            Find<Toggle>("bg/IsOpnSound").onValueChanged.AddListener(OnIsStopBtn);
            Find<Slider>("bg/soundCount").onValueChanged.AddListener(OnSliderBgmBtn);
            Find<Slider>("bg/effectCount").onValueChanged.AddListener(OnSliderEffectBtn);
            
            Find<Toggle>("bg/IsOpnSound").isOn = GameApp.SoundManager.IsStop;
            Find<Slider>("bg/soundCount").value = GameApp.SoundManager.BgmVolume;
            Find<Slider>("bg/effectCount").value = GameApp.SoundManager.EffectVolume;
        }

        /// <summary>
        /// Mute button
        /// </summary>
        /// <param name="isStop"></param>
        void OnIsStopBtn(bool isStop)
        {
            GameApp.SoundManager.IsStop = isStop;
        }

        /// <summary>
        /// background music volume slider
        /// </summary>
        /// <param name="val"></param>
        void OnSliderBgmBtn(float val)
        {
            GameApp.SoundManager.BgmVolume = val;
        }
        
        /// <summary>
        /// effect volume slider
        /// </summary>
        /// <param name="val"></param>
        void OnSliderEffectBtn(float val)
        {
            GameApp.SoundManager.EffectVolume = val;
        }
        
        void OnCloseBtn()
        {
            GameApp.ViewManager.Close(ViewId); // close self
        }
    }
}