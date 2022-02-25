using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Combat
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] float deathTime = 1.2f;
        [SerializeField] float healthPoints = 10f;
        bool isDead = false;
        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);

            if(healthPoints == 0)
            {
                Die();
            }
        }
        public float GetEnemyHealthPoints()
        {
            return healthPoints;
        }
        private void Die() 
        {
            if (isDead) return;
            isDead = true;
            StartCoroutine(removeEnemy());
            Debug.Log("The Enemy is dead!");
        }
        IEnumerator removeEnemy()
        {
            yield return new WaitForSecondsRealtime(deathTime);
            Destroy(gameObject);
        }
    }
    
}
