namespace WarChess
{
    /// <summary>
    /// Base command, can be extended to be move command, skill command, etc.
    /// </summary>
    public class BaseCommand
    {
        /// <summary>
        /// The model that the command is operating on
        /// </summary>
        public ModelBase model;
        
        /// <summary>
        /// Did the command finish?
        /// </summary>
        protected bool isFinished;

        public BaseCommand(ModelBase newModel)
        {
            model = newModel;
            isFinished = false;
        }

        public virtual bool Update(float dt)
        {
            return isFinished;
        }

        /// <summary>
        /// Execute the command
        /// </summary>
        public virtual void Do()
        {
            
        }

        /// <summary>
        /// Undo the command
        /// </summary>
        public virtual void Undo()
        {
            
        }
    }
}