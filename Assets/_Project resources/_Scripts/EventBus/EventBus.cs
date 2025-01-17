using System.Collections.Generic;
using System;
using UnityEngine;

namespace AirHockey
{
    public class EventBus
    {
        private Dictionary<string, List<object>> _eventCallbacks = new Dictionary<string, List<object>>();

        private EventBus _instance;
        public EventBus Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EventBus();
                }
                return _instance;
            }
        }

        public void Subscribe(Action<ISubscriber> callback)
        {
            string key = typeof(ISubscriber).Name;
            if (_eventCallbacks.ContainsKey(key))
            {
                _eventCallbacks[key].Add(callback);
            }
            else
            {
                _eventCallbacks.Add(key, new List<object>() { callback });
            }
        }


        public void Unsubscribe(Action<ISubscriber> callback)
        {
            string key = typeof(ISubscriber).Name;
            if (_eventCallbacks.ContainsKey(key))
            {
                _eventCallbacks[key].Remove(callback);
            }
            else
            {
                Debug.LogError($"Event type of {key} not exist");
            }
        }


        public void Invoke<T>(T signal)
            where T : ISubscriber
        {
            string key = typeof(T).Name;
            if (_eventCallbacks.ContainsKey(key))
            {
                foreach (var item in _eventCallbacks[key])
                {
                    var callback = item as Action<T>;
                    callback?.Invoke(signal);
                }
            }
        }


        public static EventBus operator +(EventBus eventBus, Action<ISubscriber> callback) => Debug.Log;










    }
}
