using System;
using System.Collections.Generic;
using UnityEngine;

namespace Combat
{
    public class WeaponsManager : MonoBehaviour
    { 
        [SerializeField] protected List<Weapon> weapons;

        private void OnEnable()
        {
            DisableColliders();
        }

        public void EnableColliders()
        {
            foreach (Weapon weapon in weapons)
            {
                weapon.EnableColliders();
            }
        }

        public void DisableColliders()
        {
            foreach (Weapon weapon in weapons)
            {
                weapon.DisableColliders();
            }
        }
    }
}