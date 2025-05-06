using EventSystem;
using UnityEngine;
using UnityEngine.Serialization;
using Utilities;

namespace TimeCycleHook
{
    public class TimeCycleManager : MonoBehaviour
    {
        // Events
        [SerializeField] private PhaseEventChannel timePhaseChannel;
        
        // Phase Duration
        [SerializeField] private float phaseDuration = 100f;
        
        // Current time phase
        private TimePhase _curTimePhase;
        
        // Phase timer
        private CountdownTimer _timeCycleTimer;

        private void Awake()
        {
            _timeCycleTimer = new CountdownTimer(phaseDuration);
        }

        private void OnEnable()
        {
            _timeCycleTimer.OnTimerStop += OnTimerEnd;
        }

        private void OnDisable()
        {
            _timeCycleTimer.OnTimerStop -= OnTimerEnd;
        }

        private void Start()
        {
            _timeCycleTimer.Start();
        }

        private void Update()
        {
            _timeCycleTimer.Tick(Time.deltaTime);
        }

        public void SetPhase(TimePhase timePhase) {
            // Set the new phase, even if it's the same - reset it
            _curTimePhase = timePhase;
            
            // Reset the timer
            _timeCycleTimer.Reset();
            
            // Invoke phase change event
            timePhaseChannel.Invoke(_curTimePhase);
        }

        private void OnTimerEnd() {
            // Increment the time phase to the next phase
            ++_curTimePhase;
            
            // If it exceeded the max phases, reset it
            if (_curTimePhase == TimePhase.MaxPhase)
                _curTimePhase = TimePhase.War;
            
            // Invoke phase change event
            timePhaseChannel.Invoke(_curTimePhase);
            
            // Start the timer again
            _timeCycleTimer.Start();
        }
    }

    public enum TimePhase
    {
        War,
        Deceive,
        Vanity,
        Lie,
        MaxPhase,
    }
}
