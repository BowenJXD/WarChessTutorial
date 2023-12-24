namespace WarChess
{
    public class BaseModel
    {
        public BaseController controller;
        
        public BaseModel(){}
        
        public BaseModel(BaseController ctl)
        {
            this.controller = ctl;
        }
        
        public virtual void Init()
        {
            
        }
    }
}