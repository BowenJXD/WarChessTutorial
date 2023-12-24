using System;

namespace WarChess
{
    public class LoadingModel : BaseModel
    {
        public string sceneName;
        public Action callback;

        public LoadingModel() : base()
        {
        }
    }
}