using System;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class LockOnTarget : MonoBehaviour
    {
        public Transform target;


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
    }
}