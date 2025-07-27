using UnityEngine;

namespace Enemies.Detection {
    public class PlayerDetector : MonoBehaviour {
        [Header("Detection")]
        [SerializeField] private float detectionAngle = 60f;        // Cone in front of enemy
        [SerializeField] private float detectionRadius = 10f;       // Large circle around enemy
        [SerializeField] private float innerDetectionRadius = 5f;   // Small circle around enemy
        [SerializeField] private float detectionCooldown = 1f;      // Time between detections
        
        [Header("Ranges")]
        [SerializeField] private float attackRange = 2f;            // Distance from enemy to player to attack
        
        [Header("References")]
        [SerializeField] public PlayerTransform player;
        
        // Detection strategy
        private IDetectionStrategy _detectionStrategy;
        
        private void Start() {
            _detectionStrategy = new ConeDetectionStrategy(detectionAngle, detectionRadius, innerDetectionRadius);
        }
        
        public bool CanDetectPlayer() {
            return _detectionStrategy.Execute(player.Transform, transform);
        }
        
        public bool CanAttackPlayer() {
            var directionToPlayer = player.Position - transform.position;
            return directionToPlayer.magnitude <= attackRange;
        }
        
        private void OnDrawGizmos() {
            Gizmos.color = Color.red;

            // Draw a spheres for the radii
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
            Gizmos.DrawWireSphere(transform.position, innerDetectionRadius);

            // Calculate our cone directions
            Vector3 forwardConeDirection = Quaternion.Euler(0, detectionAngle / 2, 0) * transform.forward * detectionRadius;
            Vector3 backwardConeDirection = Quaternion.Euler(0, -detectionAngle / 2, 0) * transform.forward * detectionRadius;

            // Draw lines to represent the cone
            Gizmos.DrawLine(transform.position, transform.position + forwardConeDirection);
            Gizmos.DrawLine(transform.position, transform.position + backwardConeDirection);
        }
    }
}