using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Boss.BossAttacks
{
    public class Gilgamesh : BossAbility
    {
        [SerializeField] private int numberOfWeapons = 3;

        [SerializeField] private GameObject weaponPrefab;

        [SerializeField] private PlayerTransform playerTransform;

        [SerializeField] private AnimationCurve weaponCurve;

        [SerializeField] private float tweenDuration = 1.5f; 
        public override void Activate()
        {
            base.Activate();
            StartCoroutine(AbilitySequence());
        }

        private IEnumerator AbilitySequence()
        {
            List<GameObject> weapons =  new List<GameObject>();
            for (int i = 0; i < numberOfWeapons; i++)
            {
                GameObject current = Instantiate(weaponPrefab, transform.position + Vector3.up * (i+6), Quaternion.identity);
                yield return new WaitForSeconds(0.3f);
                weapons.Add(current);
                
            }

            foreach (GameObject weapon in weapons)
            {
                weapon.transform.DOMove(weapon.transform.position + (playerTransform.Transform.position - weapon.transform.position).normalized * -2, 0.3f)
                    .SetEase(Ease.OutSine).OnComplete(() =>
                    {
                        weapon.transform.DOMove(playerTransform.Transform.position, tweenDuration).SetEase(Ease.InSine);
                    });
                
                yield return new WaitForSeconds(0.325f);
            }
            
            
        }
    }
}