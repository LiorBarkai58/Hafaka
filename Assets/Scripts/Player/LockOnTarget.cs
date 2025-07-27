using System;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class LockOnTarget : MonoBehaviour
    {
        public List<Transform> targets;


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy"))
            {
                targets.Add(other.transform);
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Enemy") && targets.Contains(other.transform))
            {
                targets.Remove(other.transform);
            }
        }
    }
}