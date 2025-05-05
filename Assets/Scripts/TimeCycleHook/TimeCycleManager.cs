using EventSystem;
using UnityEngine;
using UnityEngine.Events;
using Utilities;

namespace TimeCycleHook
{
    public class TimeCycleManager : MonoBehaviour
    {
        // Events
        [SerializeField] private FloatEventChannel phaseTimeChange;
        public UnityEvent onTimePhaseChange;
        
        // Phase Duration
        [SerializeField] private float phaseDuration = 100f;
        
        private TimePhase _curTimePhase;
        private static readonly TimePhase[] TimePhases =
        {
            TimePhase.War, TimePhase.Deceive,
            TimePhase.Vanity, TimePhase.Lie
        };
        
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

        private void OnTimerEnd()
        {
            
        }
    }

    public enum TimePhase
    {
        War,
        Deceive,
        Vanity,
        Lie,
    }
}
