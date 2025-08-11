using UnityEngine;
using UnityEngine.Events;

namespace EventSystem {
    public abstract class EventListener<T> : MonoBehaviour {
        [SerializeField] private EventChannel<T> eventChannel;
        [SerializeField] private UnityEvent<T> unityEvent;

        public event UnityAction<T> OnEvent;

        protected void Awake() {
            eventChannel.Register(this);
        }

        protected void OnDestroy() {
            eventChannel.Deregister(this);
        }

        public void Raise(T value) {
            unityEvent?.Invoke(value);
            OnEvent?.Invoke(value);
        }
    }
}
