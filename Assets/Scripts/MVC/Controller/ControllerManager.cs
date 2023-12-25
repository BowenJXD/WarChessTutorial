using System.Collections.Generic;
using System.Linq;

namespace WarChess
{
    public class ControllerManager
    {
        protected Dictionary<int, BaseController> modules;
        
        public ControllerManager()
        {
            modules = new Dictionary<int, BaseController>();
        }

        public void Register(EControllerType type, BaseController ctl)
        {
            Register((int)type, ctl);
        }
        
        /// <summary>
        /// Register controller
        /// </summary>
        /// <param name="controllerKey"></param>
        /// <param name="controller"></param>
        public void Register(int controllerKey, BaseController controller)
        {
            if (!modules.ContainsKey(controllerKey))
            {
                modules.Add(controllerKey, controller);
            }
        }

        public void InitAllModules()
        {
            foreach (var item in modules)
            {
                item.Value.Init();
            }
        }
        
        /// <summary>
        /// Unregister controller
        /// </summary>
        /// <param name="controllerKey"></param>
        public void Unregister(int controllerKey)
        {
            if (modules.ContainsKey(controllerKey))
            {
                modules.Remove(controllerKey);
            }
        }

        public void Clear()
        {
            modules.Clear();
        }

        public void ClearAllModules()
        {
            List<int> keys = modules.Keys.ToList();
            
            for (int i = 0; i < keys.Count; i++)
            {
                modules[keys[i]].Destroy();
                modules.Remove(keys[i]);
            }
        }
        
        /// <summary>
        /// Trigger func across modules
        /// </summary>
        /// <param name="controllerKey"></param>
        /// <param name="eventName"></param>
        /// <param name="args"></param>
        public void ApplyFunc(int controllerKey, string eventName, object[] args)
        {
            if (modules.ContainsKey(controllerKey))
            {
                modules[controllerKey].ApplyFunc(eventName, args);
            }
        }
        
        public BaseModel GetControllerModel(int controllerKey)
        {
            if (modules.ContainsKey(controllerKey))
            {
                return modules[controllerKey].GetModel();
            }
            return null;
        }
    }
}