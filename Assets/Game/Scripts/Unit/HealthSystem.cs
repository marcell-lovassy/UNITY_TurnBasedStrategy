using System;
using UnityEngine;
using UnityEngine.Events;

namespace Game.UnitSystem 
{
    public class HealthSystem : MonoBehaviour
    {
        public event UnityAction OnDead;

        [SerializeField]
        private int health = 100;

        public void TakeDamage(int damageAmount)
        {
            health -= damageAmount;

            if(health <= 0) 
            {
                health = 0;
                Die();
            }
        }

        private void Die()
        {
            OnDead?.Invoke();
        }
    }
}