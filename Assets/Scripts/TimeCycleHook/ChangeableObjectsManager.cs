using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TimeCycleHook {
    public class ChangeableObjectsManager : MonoBehaviour {
        [SerializeField] private TimePhase timePhase;
        private List<ITimeChangeable> _objects;
        
        private void OnValidate() {
            _objects = GetComponentsInChildren<ITimeChangeable>(true).ToList();
        }

        public void OnTimePhaseChange(TimePhase newTimePhase) {
            if (timePhase != newTimePhase) return;
            
            foreach (var _object in _objects) {
                _object.OnTimePhaseChanged();
            }
        }
    }
}