using EventSystem;
using UI;
using Unity.Mathematics;
using UnityEngine;

namespace Managers {
    public class ValueIndicatorManager : MonoBehaviour {
        [Header("Floating Popup")]
        [SerializeField] private FloatingPopup floatingPopupPrefab;
        [SerializeField] private float popupLifeDuration = 3f;
        
        [Header("Events")]
        [SerializeField] private DamageArgsEventListener damageArgsEventListener;

        private void OnEnable() {
            damageArgsEventListener.OnEvent += OnEnemyHit;
        }

        private void OnDisable() {
            damageArgsEventListener.OnEvent -= OnEnemyHit;
        }

        private void OnEnemyHit(DamageDealtArgs damageDealtArgs) {
            var pos = damageDealtArgs.attackingEntity.transform.position + Vector3.up;
            var dmg = damageDealtArgs.damage;
            
            var floatingPopup = Instantiate(floatingPopupPrefab, 
                pos, 
                quaternion.identity);
            
            floatingPopup.SetFloatingText(dmg);
            
            Destroy(floatingPopup, popupLifeDuration);
        }
    }
}