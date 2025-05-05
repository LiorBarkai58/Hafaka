using System.Collections.Generic;
using UnityEngine;

namespace EventSystem {
    public abstract class EventChannel<T> : ScriptableObject {
        private readonly HashSet<EventListener<T>> _listeners = new();

        public void Invoke(T value) {
            foreach (var listener in _listeners) {
                listener.Raise(value);
            }
        }

        public void Register(EventListener<T> listener) => _listeners.Add(listener);
        public void Deregister(EventListener<T> listener) => _listeners.Remove(listener);
    }
    
    [CreateAssetMenu(menuName = "Events/FloatEventChannel")]
    public class FloatEventChannel : EventChannel<float> { }
    
    [CreateAssetMenu(menuName = "Events/IntEventChannel")]
    public class IntEventChannel : EventChannel<int> { }
}
