using System.Collections;
using System.Collections.Generic;
using Game.ClassTypes.Friendly;
using Game.ClassTypes.Player;
using Game.Enums;
using Game.Inventories;
using Game.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Combat
{
    public class FriendlyHealth : MonoBehaviour, IHealth
    {
        [SerializeField] float deathTime = 1.2f;
        LazyValue<float> healthPoints;

        private void Awake() 
        {
            healthPoints = new LazyValue<float>(GetInitialHealth);
        }
        private float GetInitialHealth()
        {
            return GetComponent<FriendlyClassSetup>().GetStat(AIBaseStat.Health);
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
                
                Die();
                AwardExperience(instigator);
                if(!GetComponent<LootDropper>()) return;
                GetComponent<LootDropper>().RandomDrop();
                GetComponent<LootDropper>().GemDrop(instigator);

            } 
        }
        public float GetHealthPoints()
        {
            return healthPoints.value;
        }

        public float GetMaxHealthPoints()
        {
            return GetComponent<FriendlyClassSetup>().GetStat(AIBaseStat.Health);
        }

        public float GetPercentage()
        {
            return 100 * GetFraction();
        }

        public float GetFraction()
        {
            return healthPoints.value / GetComponent<FriendlyClassSetup>().GetStat(AIBaseStat.Health);
        }

        private void Die() 
        {
            StartCoroutine(removeEnemy());
            Debug.Log("The Friendly is dead!");
        }
        private void AwardExperience(GameObject instigator)
        {
            PlayerExperience experience = instigator.GetComponent<PlayerExperience>();
            if (experience == null) return;

            experience.GainExperience(GetComponent<FriendlyClassSetup>().GetStat(AIBaseStat.ExperienceReward));
        }
        IEnumerator removeEnemy()
        {
            yield return new WaitForSecondsRealtime(deathTime);
            Destroy(gameObject);
        }
    }
    
}
