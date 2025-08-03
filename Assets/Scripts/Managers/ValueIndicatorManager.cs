using DG.Tweening;
using EventSystem;
using UI;
using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;

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
            var pos = damageDealtArgs.attackedEntity.transform.position + Vector3.up + Vector3.right * UnityEngine.Random.Range(-0.5f, 0.5f);
            var dmg = damageDealtArgs.damage;
            
            var floatingPopup = Instantiate(floatingPopupPrefab, 
                pos, 
                quaternion.identity);
            
            floatingPopup.SetFloatingText(dmg);
            floatingPopup.transform.DOMoveY(3, 0.7f).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                 Destroy(floatingPopup.gameObject);
            });
            
        }
    }
}