using UnityEngine;

namespace TimeCycleHook {
    public class TimeTrigger : MonoBehaviour {
        [SerializeField] private TimeCycleManager timeCycleManager;
        [SerializeField] private TimePhase timePhaseToChangeTo;
        
        private void OnTriggerEnter(Collider other) {
            if (!other.CompareTag("Player")) return;
            
            timeCycleManager.SetPhase(timePhaseToChangeTo);
        }
    }
}
