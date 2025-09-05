using System;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class LockOnTarget : MonoBehaviour
    {
        public Transform target;
        [SerializeField] private float closeEnoughDistance = 3f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                target = other.transform;
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Enemy") && target == other.transform)
            {
                target = null;
            }
        }

        public bool CloseToTarget()
        {
            return target && Vector3.Distance(transform.position, target.position) < closeEnoughDistance;
        }

        public void CheckClearTarget()
        {
            if (target && target.gameObject && target.gameObject.activeInHierarchy) return;
            target = null;
        }
    }
}