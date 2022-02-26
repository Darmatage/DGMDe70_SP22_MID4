using System.Collections;
using System.Collections.Generic;
using Game.EnemyClass;
using Game.Enums;
using Game.Utils;
using UnityEngine;

namespace Game.Combat
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] float deathTime = 1.2f;
        LazyValue<float> healthPoints;
        bool isDead = false;
        private void Awake() {
            healthPoints = new LazyValue<float>(GetInitialHealth);
        }
        private float GetInitialHealth()
        {
            return GetComponent<EnemyClassSetup>().GetStat(EnemyBaseStat.Health);
        }
        private void Start()
        {
            healthPoints.ForceInit();
        }
        public bool IsDead()
        {
            return isDead;
        }
        public void TakeDamage(float damage)
        {
            healthPoints.value = Mathf.Max(healthPoints.value - damage, 0);

            if(healthPoints.value == 0)
            {
                Die();
            }
        }
        public float GetEnemyHealthPoints()
        {
            return healthPoints.value;
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
