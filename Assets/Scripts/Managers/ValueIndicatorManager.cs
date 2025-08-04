using System;
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
        private Camera _camera;
        [Header("Events")]
        [SerializeField] private DamageArgsEventListener damageArgsEventListener;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void OnEnable() {
            damageArgsEventListener.OnEvent += OnEnemyHit;
        }

        private void OnDisable() {
            damageArgsEventListener.OnEvent -= OnEnemyHit;
        }

        private void OnEnemyHit(DamageDealtArgs damageDealtArgs) {
            var pos = damageDealtArgs.attackedEntity.transform.position + Vector3.up * 4 + Vector3.right * UnityEngine.Random.Range(-2f, 2f);
            var dmg = damageDealtArgs.damage;
            
            var floatingPopup = Instantiate(floatingPopupPrefab, 
                pos, 
                quaternion.identity);
            
            floatingPopup.transform.LookAt(_camera.transform);
            floatingPopup.transform.Rotate(0, 180f, 0);
            floatingPopup.SetFloatingText(dmg);
            floatingPopup.transform.DOMoveY(pos.y + 3, 0.7f).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                 Destroy(floatingPopup.gameObject);
            });
            
        }
    }
}