using UnityEngine;

namespace Enemies.Detection {
    public class ConeDetectionStrategy : IDetectionStrategy {
        
        private float _detectionAngle;
        private readonly float _detectionRadius;
        private readonly float _innerDetectionRadius;
        
        public ConeDetectionStrategy(float detectionAngle, float detectionRadius, float innerDetectionRadius) {
            _detectionAngle = detectionAngle;
            _detectionRadius = detectionRadius;
            _innerDetectionRadius = innerDetectionRadius;
        }
        
        public bool Execute(Transform player, Transform detector) {
            var directionToPlayer = player.position - detector.position;
            var angleToPlayer = Vector3.Angle(directionToPlayer, detector.forward);
            
            return (angleToPlayer < _detectionAngle / 2f && directionToPlayer.magnitude < _detectionRadius)
                   || directionToPlayer.magnitude < _innerDetectionRadius;
        }
    }
}