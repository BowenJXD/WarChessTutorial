using System;
using System.Collections.Generic;

namespace WarChess
{
    /// <summary>
    /// Message process center, need to be executed manually
    /// </summary>
    public class MessageCenter
    {
        /// <summary>
        /// Stores normal messages
        /// </summary>
        protected Dictionary<string, Action<object>> msgDic;
        
        /// <summary>
        /// Stores temporary messages, which will be cleared after being processed
        /// </summary>
        protected Dictionary<string, Action<object>> tempMsgDic;

        /// <summary>
        /// Store messages for specific objects
        /// </summary>
        protected Dictionary<Object, Dictionary<string, Action<object>>> objMsgDic;

        public MessageCenter()
        {
            msgDic = new Dictionary<string, Action<object>>();
            tempMsgDic = new Dictionary<string, Action<object>>();
            objMsgDic = new Dictionary<Object, Dictionary<string, Action<object>>>();
        }

        public void AddEvent(string eventName, Action<object> callback)
        {
            if (msgDic.ContainsKey(eventName))
            {
                msgDic[eventName] += callback;
            }
            else
            {
                msgDic.Add(eventName, callback);
            }
        }
        
        /// <summary>
        /// Add an event for specific object
        /// </summary>
        /// <param name="listenerObj"></param>
        /// <param name="eventName"></param>
        /// <param name="callback"></param>
        public void AddEvent(object listenerObj, string eventName, Action<object> callback)
        {
            if (objMsgDic.ContainsKey(listenerObj))
            {
                if (objMsgDic[listenerObj].ContainsKey(eventName))
                {
                    objMsgDic[listenerObj][eventName] += callback;
                }
                else
                {
                    objMsgDic[listenerObj].Add(eventName, callback);
                }
            }
            else
            {
                Dictionary<string, Action<object>> dic = new Dictionary<string, Action<object>>();
                dic.Add(eventName, callback);
                objMsgDic.Add(listenerObj, dic);
            }
        }
        
        public void AddTempEvent(string eventName, Action<object> callback)
        {
            if (tempMsgDic.ContainsKey(eventName))
            {
                tempMsgDic[eventName] = callback; // temp event will override the previous one instead of adding
            }
            else
            {
                tempMsgDic.Add(eventName, callback);
            }
        }
        
        public void RemoveEvent(string eventName, Action<object> callback)
        {
            if (msgDic.ContainsKey(eventName))
            {
                msgDic[eventName] -= callback;
                if (msgDic[eventName] == null)
                {
                    msgDic.Remove(eventName);
                }
            }
        }
        
        /// <summary>
        /// Remove an event for specific object
        /// </summary>
        /// <param name="listenerObj"></param>
        /// <param name="eventName"></param>
        /// <param name="callback"></param>
        public void RemoveEvent(object listenerObj, string eventName, Action<object> callback)
        {
            if (objMsgDic.ContainsKey(listenerObj))
            {
                if (objMsgDic[listenerObj].ContainsKey(eventName))
                {
                    objMsgDic[listenerObj][eventName] -= callback;
                    if (objMsgDic[listenerObj][eventName] == null)
                    {
                        objMsgDic[listenerObj].Remove(eventName);
                        if (objMsgDic[listenerObj].Count == 0)
                        {
                            objMsgDic.Remove(listenerObj);
                        }
                    }
                }
            }
        }
        
        public void RemoveTempEvent(string eventName, Action<object> callback)
        {
            if (tempMsgDic.ContainsKey(eventName))
            {
                tempMsgDic[eventName] -= callback;
                if (tempMsgDic[eventName] == null)
                {
                    tempMsgDic.Remove(eventName);
                }
            }
        }
        
        /// <summary>
        /// Remove all events for specific object
        /// </summary>
        /// <param name="listenerObj"></param>
        public void RemoveObjAllEvent(object listenerObj)
        {
            if (objMsgDic.ContainsKey(listenerObj))
            {
                objMsgDic.Remove(listenerObj);
            }
        }
        
        /// <summary>
        /// Process event
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="arg"></param>
        public void PostEvent(string eventName, object arg = null)
        {
            if (msgDic.ContainsKey(eventName))
            {
                msgDic[eventName].Invoke(arg);
            }
        }

        /// <summary>
        /// Process event for specific object
        /// </summary>
        /// <param name="listenerObj"></param>
        /// <param name="eventName"></param>
        /// <param name="arg"></param>
        public void PostEvent(object listenerObj, string eventName, object arg = null)
        {
            if (objMsgDic.ContainsKey(listenerObj))
            {
                if (objMsgDic[listenerObj].ContainsKey(eventName))
                {
                    objMsgDic[listenerObj][eventName].Invoke(arg);
                }
            }
        }
        
        public void PostTempEvent(string eventName, object arg = null)
        {
            if (tempMsgDic.ContainsKey(eventName))
            {
                tempMsgDic[eventName].Invoke(arg);
                tempMsgDic[eventName] = null;
                tempMsgDic.Remove(eventName);
            }
        }
    }
}