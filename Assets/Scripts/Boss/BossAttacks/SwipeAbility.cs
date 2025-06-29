using DG.Tweening;
using UnityEngine;

namespace Boss.BossAttacks
{
    public class SwipeAbility : BossAbility
    {
        [SerializeField] private GameObject swipeObject;
        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform endPoint;
        
        public override void Activate()
        {
            base.Activate();
            swipeObject.transform.position = startPoint.position;
            swipeObject.SetActive(true);
            swipeObject.transform.DOJump(endPoint.position, 1, 1, abilityData.Duration);
        }
    }
}