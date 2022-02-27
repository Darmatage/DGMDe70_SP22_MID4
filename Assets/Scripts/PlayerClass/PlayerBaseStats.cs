using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Utils;
using Game.Enums;

namespace Game.PlayerClass
{
    public class PlayerBaseStats : MonoBehaviour
    {
        [Range(1,10)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClasses characterClasses;
        [SerializeField] Progression progression = null;
        [SerializeField] bool shouldUseModifiers = false;
        LazyValue<int> currentLevel;
        Experience experience;
        public event Action onLevelUp;

        private void Awake() 
        {
            experience = GetComponent<Experience>();
            currentLevel = new LazyValue<int>(CalculateLevel);
        }
        private void Start() 
        {
            currentLevel.ForceInit();
        }
        private void OnEnable() 
        {
            if (experience != null)
            {
                experience.onExperienceGained += UpdateLevel;
            }
        }
        private void OnDisable() 
        {
            if (experience != null)
            {
                experience.onExperienceGained -= UpdateLevel;
            }
        }
        private void UpdateLevel()  //Recalculates Level on gain level not on update
        {
            int newLevel = CalculateLevel();
            if (newLevel > currentLevel.value)
            {
                currentLevel.value = newLevel;
                print("Levelled Up!");
                onLevelUp();
            }
        }
        public float GetStat(PlayerStats stat)
        {
            return (GetBaseStat(stat) + GetAdditiveModifier(stat)) * (1 + GetPercentageModifier(stat)/100);
        }

        private float GetBaseStat(PlayerStats stat)
        {
            return progression.GetStat(stat, characterClasses, GetLevel());
        }

        public int GetLevel()
        {
            return currentLevel.value;
        }

        private float GetAdditiveModifier(PlayerStats stat)
        {
            float total = 0;
            foreach (IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach (float modifier in provider.GetAdditiveModifiers(stat))
                {
                    total += modifier;
                }
            }
            return total;
        }

        private float GetPercentageModifier(PlayerStats stat)
        {
            if (!shouldUseModifiers) return 0;

            float total = 0;
            foreach (IModifierProvider provider in GetComponents<IModifierProvider>())
            {
                foreach (float modifier in provider.GetPercentageModifiers(stat))
                {
                    total += modifier;
                }
            }
            return total;
        }

        private int CalculateLevel()
        {
            Experience experience = GetComponent<Experience>();
            if (experience == null) return startingLevel;

            float currentXP = experience.GetExperiencePoints();
            int penultimateLevel = progression.GetLevels(Stat.ExperienceToLevelUp, characterClasses);

            for (int level = 1; level <= penultimateLevel; level++)
            {
                float XPToLevelUp = progression.GetStat(Stat.ExperienceToLevelUp, characterClasses, level);
                if(XPToLevelUp > currentXP)
                {
                    return level;
                }
            }
            return penultimateLevel + 1;
        }

    }
}
