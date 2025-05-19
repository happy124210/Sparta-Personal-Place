using System;
using System.Collections.Generic;
using Entity.Player;
using UnityEditor;
using UnityEngine;

namespace Objects
{
    public class CampFire : MonoBehaviour
    {
        [SerializeField] private int damage;
        [SerializeField] private float damageRate;
        private List<IDamageable> things = new List<IDamageable>();


        void Start()
        {
            InvokeRepeating("DealDamage", 0f, damageRate);
        }

        // Update is called once per frame
        private void DealDamage()
        {
            for (int i = 0; i < things.Count; i++)
            {
                things[i].TakeHealthDamage(damage);
            }
        }
        
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageable damageable))
            {
                things.Add(damageable);
            }
        }
        
        
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out IDamageable damageable))
            {
                things.Remove(damageable);
            }
        }
    }
}
