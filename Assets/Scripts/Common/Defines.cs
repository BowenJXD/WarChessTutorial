namespace WarChess
{
    /// <summary>
    /// Define all the event strings
    /// </summary>
    public static class Defines
    {
        // Controller event related
        public static readonly string OpenStartView = "OpenStartView";
        public static readonly string OpenSetView = "OpenSetView";
        public static readonly string OpenMessageView = "OpenMessageView";
        public static readonly string OpenLoadingView = "OpenLoadingView";
        public static readonly string OpenSelectLevelView = "OpenSelectLevelView";
        public static readonly string BeginFight = "BeginFight";
        
        // Global event related
        public static readonly string ShowLevelDesEvent = "ShowLevelDesEvent";
        public static readonly string HideLevelDesEvent = "HideLevelDesEvent";
        
        public static readonly string OnSelectEvent = "OnSelectEvent";
        public static readonly string OnUnselectEvent = "OnUnselectEvent";
    }
}