using UnityEngine;
using Utilities;

namespace TimeCycleHook {
    public class TimeTrigger : MonoBehaviour {
        [SerializeField] private TimeCycleManager timeCycleManager;
        [SerializeField] private TimePhase timePhaseToChangeTo;
        [SerializeField] private DissolveHelper dissolveHelper;
        
        private void OnTriggerEnter(Collider other) {
            if (!other.CompareTag("Player")) return;
            
            timeCycleManager.SetPhase(timePhaseToChangeTo);
            
            if(dissolveHelper) dissolveHelper.Dissolve();
        }
    }
}
