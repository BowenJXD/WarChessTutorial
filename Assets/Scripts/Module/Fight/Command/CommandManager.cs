using System.Collections.Generic;

namespace WarChess
{
    public class CommandManager
    {
        /// <summary>
        ///  The command queue that will be executed, queue
        /// </summary>
        protected Queue<BaseCommand> willDoQueue;
        
        /// <summary>
        ///  The command queue that can be undone, stack
        /// </summary>
        protected Stack<BaseCommand> undoStack;
        
        protected BaseCommand current;
        
        public CommandManager()
        {
            willDoQueue = new Queue<BaseCommand>();
            undoStack = new Stack<BaseCommand>();
        }
        
        public bool IsRunningCommand
        {
            get
            {
                return current != null;
            }
        }
        
        public void AddCommand(BaseCommand cmd)
        {
            willDoQueue.Enqueue(cmd);
            undoStack.Push(cmd);
        }
        
        public void OnUpdate(float dt)
        {
            if (current == null)
            {
                if (willDoQueue.Count > 0)
                {
                    // execute the next command
                    current = willDoQueue.Dequeue();
                    current.Do();
                }
            }
            else
            {
                if (current.Update(dt))
                {
                    current = null;
                }
            }
        }
        
        public void Clear()
        {
            willDoQueue.Clear();
            undoStack.Clear();
            current = null;
        }
        
        public void Undo()
        {
            if (undoStack.Count > 0)
            {
                BaseCommand cmd = undoStack.Pop();
                cmd.Undo();
            }
        }
    }
}