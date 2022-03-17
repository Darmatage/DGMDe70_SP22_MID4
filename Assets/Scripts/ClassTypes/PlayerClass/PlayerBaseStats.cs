using System;
using System.Collections;
using System.Collections.Generic;
using Game.Enums;
using Game.Utils;
using UnityEngine;

namespace Game.ClassTypes.Player
{
    public class PlayerBaseStats : MonoBehaviour
    {
        [Range(1, 99)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClasses characterClass;
        [SerializeField] SO_PlayerProgression progression = null;
        public event Action onLevelUp;
        LazyValue<int> currentLevel;

        PlayerExperience experience;

        private void Awake() {
            experience = GetComponent<PlayerExperience>();
            currentLevel = new LazyValue<int>(CalculateLevel);
        }

        private void Start() 
        {
            currentLevel.ForceInit();
        }

        private void OnEnable() {
            if (experience != null)
            {
                experience.onExperienceGained += UpdateLevel;
            }
        }

        private void OnDisable() {
            if (experience != null)
            {
                experience.onExperienceGained -= UpdateLevel;
            }
        }

        private void UpdateLevel() 
        {
            int newLevel = CalculateLevel();
            if (newLevel > currentLevel.value)
            {
                currentLevel.value = newLevel;
                onLevelUp();
            }
        }

        public float GetStat(PlayerStats stat)
        {
            return (GetBaseStat(stat) + GetAdditiveModifier(stat)) * (1 + GetPercentageModifier(stat)/100);
        }

        private float GetBaseStat(PlayerStats stat)
        {
            return progression.GetStat(stat, characterClass, GetLevel());
        }

        public int GetLevel()
        {
            return currentLevel.value;
        }

        public CharacterClasses GetCharacterClass()
        {
            return characterClass;
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
            PlayerExperience experience = GetComponent<PlayerExperience>();
            if (experience == null) return startingLevel;

            float currentXP = experience.GetPoints();
            int penultimateLevel = progression.GetLevels(PlayerStats.ExperienceToLevelUp, characterClass);
            for (int level = 1; level <= penultimateLevel; level++)
            {
                float XPToLevelUp = progression.GetStat(PlayerStats.ExperienceToLevelUp, characterClass, level);
                if (XPToLevelUp > currentXP)
                {
                    return level;
                }
            }

            return penultimateLevel + 1;
        }
    }
}