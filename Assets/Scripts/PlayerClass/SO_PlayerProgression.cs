using UnityEngine;
using System.Collections.Generic;
using System;
using Game.Enums;

namespace Game.PlayerClass
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Game/Player/New Player Progression")]
    public class SO_PlayerProgression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClasses = null;

        Dictionary<CharacterClasses, Dictionary<PlayerStats, float[]>> lookupTable = null;

        public float GetStat(PlayerStats stat, CharacterClasses characterClass, int level)
        {
            BuildLookup();

            if (!lookupTable[characterClass].ContainsKey(stat))
            {
                return 0;
            }

            float[] levels = lookupTable[characterClass][stat];

            if (levels.Length == 0)
            {
                return 0;
            }

            if (levels.Length < level)
            {
                return levels[levels.Length - 1];
            }

            return levels[level - 1];
        }

        public int GetLevels(PlayerStats stat, CharacterClasses characterClass)
        {
            BuildLookup();

            float[] levels = lookupTable[characterClass][stat];
            return levels.Length;
        }

        private void BuildLookup()
        {
            if (lookupTable != null) return;

            lookupTable = new Dictionary<CharacterClasses, Dictionary<PlayerStats, float[]>>();

            foreach (ProgressionCharacterClass progressionClass in characterClasses)
            {
                var statLookupTable = new Dictionary<PlayerStats, float[]>();

                foreach (ProgressionStat progressionStat in progressionClass.stats)
                {
                    statLookupTable[progressionStat.stat] = progressionStat.levels;
                }

                lookupTable[progressionClass.characterClass] = statLookupTable;
            }
        }

        [System.Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClasses characterClass;
            public ProgressionStat[] stats;
        }

        [System.Serializable]
        class ProgressionStat
        {
            public PlayerStats stat;
            public float[] levels;
        }
    }
}