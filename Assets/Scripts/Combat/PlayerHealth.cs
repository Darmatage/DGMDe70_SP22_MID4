using System.Collections;
using System.Collections.Generic;
using Game.Enums;
using Game.Inventories;
using Game.ClassTypes.Player;
using Game.Saving;
using Game.Utils;
using UnityEngine;

namespace Game.Combat
{
    public class PlayerHealth : MonoBehaviour, ISaveable
    {
        [SerializeField] float regenerationPercentage = 5;
        LazyValue<float> healthPoints;

        private void Awake() 
        {
            healthPoints = new LazyValue<float>(GetInitialHealth);
            GetComponent<Equipment>().equipmentUpdated += UpdateHealth;
        }
        private float GetInitialHealth()
        {
            return GetComponent<PlayerBaseStats>().GetStat(PlayerStats.Health);
        }
        private void Start()
        {
            healthPoints.ForceInit();
        }

        private void OnEnable() {
            GetComponent<PlayerBaseStats>().onLevelUp += RegenerateHealth;
        }

        private void OnDisable() {
            GetComponent<PlayerBaseStats>().onLevelUp -= RegenerateHealth;
        }
        public bool IsDead()
        {
            return healthPoints.value <= 0;
        }

        public void TakeDamage(float damage)
        {
            healthPoints.value = Mathf.Max(healthPoints.value - damage, 0);            
            if(IsDead())
            {
                Debug.Log("Player is dead!");
                FindObjectOfType<SavingWrapperControl>().Load();
            } 
        }

        public void Heal(float healthToRestore)
        {
            healthPoints.value = Mathf.Min(healthPoints.value + healthToRestore, GetMaxHealthPoints());
        }

        public float GetHealthPoints()
        {
            return healthPoints.value;
        }

        public float GetMaxHealthPoints()
        {
            return GetComponent<PlayerBaseStats>().GetStat(PlayerStats.Health);
        }

        public float GetPercentage()
        {
            return 100 * GetFraction();
        }

        public float GetFraction()
        {
            return healthPoints.value / GetComponent<PlayerBaseStats>().GetStat(PlayerStats.Health);
        }

        private void UpdateHealth()
        {
            healthPoints.value = Mathf.Min(healthPoints.value, GetComponent<PlayerBaseStats>().GetStat(PlayerStats.Health));
        }

        private void RegenerateHealth()
        {
            float regenHealthPoints = GetComponent<PlayerBaseStats>().GetStat(PlayerStats.Health) * (regenerationPercentage / 100);
            healthPoints.value = Mathf.Max(healthPoints.value, regenHealthPoints);
        }

        object ISaveable.CaptureState()
        {
            return healthPoints.value;
        }

        void ISaveable.RestoreState(object state)
        {
            healthPoints.value = (float) state;
        }
    }   
}
