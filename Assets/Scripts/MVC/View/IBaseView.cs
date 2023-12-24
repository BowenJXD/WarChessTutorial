namespace WarChess
{
    public interface IBaseView
    {
        int ViewId { get; set; }
        
        BaseController Controller { get; set; }
        
        bool IsInit();
        
        bool IsShow();

        void InitUI();

        void InitData();
        
        void Open(params object[] args);
        
        void Close(params object[] args);

        void DestroyView();

        /// <summary>
        /// Trigger this controller's event
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="args"></param>
        void ApplyFunc(string eventName, params object[] args);

        /// <summary>
        /// Trigger other controller's event
        /// </summary>
        /// <param name="controllerKey"></param>
        /// <param name="eventName"></param>
        /// <param name="args"></param>
        void ApplyControllerFunc(int controllerKey, string eventName, params object[] args);
        
        void SetVisible(bool visible);
    }
}