using UnityEngine;
using UnityEngine.EventSystems;

namespace WarChess
{
    /// <summary>
    /// Keyboard and mouse input manager
    /// </summary>
    public class UserInputManager
    {
        public void OnUpdate(float dt)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    
                }
                else
                {
                    Util.ScreenPointToRay2D(Camera.main, Input.mousePosition, col =>
                    {
                        if (col != null)
                        {
                            GameApp.MessageCenter.PostEvent(col.gameObject, Defines.OnSelectEvent);
                        }
                        else
                        {
                            GameApp.MessageCenter.PostEvent(Defines.OnUnselectEvent);
                        }
                    });
                }
            }
        }
    }
}