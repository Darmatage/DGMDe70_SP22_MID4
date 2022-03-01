using System.Collections;
using System.Collections.Generic;
using Game.EnemyClass;
using Game.Enums;
using Game.PlayerClass;
using Game.Utils;
using UnityEngine;

namespace Game.Combat
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] float deathTime = 1.2f;
        LazyValue<float> healthPoints;

        private void Awake() 
        {
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
            return healthPoints.value <= 0;
        }
        public void TakeDamage(GameObject instigator, float damage)
        {
            healthPoints.value = Mathf.Max(healthPoints.value - damage, 0);
            
            if(IsDead())
            {
                AwardExperience(instigator);
                Die();
            } 
        }
        public float GetEnemyHealthPoints()
        {
            return healthPoints.value;
        }

        public float GetEnemyMaxHealthPoints()
        {
            return GetComponent<EnemyClassSetup>().GetStat(EnemyBaseStat.Health);
        }

        public float GetPercentage()
        {
            return 100 * GetFraction();
        }

        public float GetFraction()
        {
            return healthPoints.value / GetComponent<EnemyClassSetup>().GetStat(EnemyBaseStat.Health);
        }

        private void Die() 
        {
            StartCoroutine(removeEnemy());
            Debug.Log("The Enemy is dead!");
        }
        private void AwardExperience(GameObject instigator)
        {
            PlayerExperience experience = instigator.GetComponent<PlayerExperience>();
            if (experience == null) return;

            experience.GainExperience(GetComponent<EnemyClassSetup>().GetStat(EnemyBaseStat.ExperienceReward));
        }
        IEnumerator removeEnemy()
        {
            yield return new WaitForSecondsRealtime(deathTime);
            Destroy(gameObject);
        }
    }
    
}
