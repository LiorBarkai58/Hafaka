using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace Boss.BossAttacks
{
    public class SwipeAbility : BossAbility
    {
        [SerializeField] private Transform visuals;
        [SerializeField] private GameObject swipeObject;
        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform endPoint;
        [SerializeField] private AnimationCurve curve;
        
        public override void Activate()
        {
            base.Activate();
            swipeObject.transform.position = startPoint.position;
            visuals.rotation = Quaternion.Euler(0, 0, 90);
            swipeObject.SetActive(true);
            Vector3 controlPoint = (endPoint.position + startPoint.position) / 2 + transform.forward * 4;
            swipeObject.transform.DOPath(new Vector3[]{startPoint.position, controlPoint, endPoint.position }, abilityData.Duration, PathType.CatmullRom, gizmoColor: Color.red).SetEase(curve);
            visuals.DORotate(new Vector3(0, 180, visuals.transform.rotation.eulerAngles.z), abilityData.Duration).SetEase(curve);
        }
    }
}