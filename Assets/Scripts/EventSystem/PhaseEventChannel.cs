using TimeCycleHook;
using UnityEngine;

namespace EventSystem
{
    [CreateAssetMenu(menuName = "Events/PhaseEventChannel")]
    public class PhaseEventChannel : EventChannel<TimePhase> { }
}