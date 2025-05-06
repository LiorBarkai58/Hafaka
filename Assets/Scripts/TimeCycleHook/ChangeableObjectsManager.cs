using System.Collections.Generic;
using UnityEngine;

namespace TimeCycleHook {
    public class ChangeableObjectsManager : MonoBehaviour {
        [SerializeField] private TimePhase timePhase;
        [SerializeField] private List<GameObject> gameObjects = new ();

        public void OnTimePhaseChange(TimePhase newTimePhase) {
            if (timePhase != newTimePhase)
            {
                // ReSharper disable once InconsistentNaming
                foreach (var _gameObject in gameObjects) {
                    _gameObject.SetActive(false);
                }
            }
            else
            {
                // ReSharper disable once InconsistentNaming
                foreach (var _gameObject in gameObjects) {
                    _gameObject.SetActive(true);
                }
            }
        }
    }
}