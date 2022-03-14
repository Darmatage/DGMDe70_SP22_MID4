using System.Collections;
using System.Collections.Generic;
using Game.Enums;
using UnityEngine;

namespace Game.ClassTypes.Friendly
{
    [CreateAssetMenu(fileName = "FriendlyClassStats", menuName = "Game/Friendly/New Friendly Class Stats", order = 1)]
    public class SO_FriendlyClassStats : ScriptableObject
    {
        [SerializeField] FriendlyClassTypeCollection[] friendlyClassTypeCollectionList = null;
        Dictionary<FriendlyType, Dictionary<AIBaseStat, float[]>> lookupTable = null;

        public float GetStat(FriendlyType friendlyType, AIBaseStat stat,  int difficultyLevel)
        {
            BuildLookup();

            float[] levels = lookupTable[friendlyType][stat];

            if(levels.Length < difficultyLevel)
            {
                return 0f;
            }

            return levels[difficultyLevel - 1];
        }

        private void BuildLookup()
        {
            if(lookupTable != null) return;

            lookupTable = new Dictionary<FriendlyType, Dictionary<AIBaseStat, float[]>>();

            foreach(FriendlyClassTypeCollection friendlyClassTypeCollectionItem in friendlyClassTypeCollectionList)
            {
                var stateLookupTable = new Dictionary<AIBaseStat, float[]>();

                foreach (FriendlyStatCollections friendlyStatItem in friendlyClassTypeCollectionItem.stats)
                {
                    stateLookupTable[friendlyStatItem.friendlyBaseStat] = friendlyStatItem.levels;
                }

                lookupTable[friendlyClassTypeCollectionItem.friendlyType] = stateLookupTable;
            }
        }

        [System.Serializable]
        class FriendlyClassTypeCollection
        {
            public FriendlyType friendlyType;
            public FriendlyStatCollections[] stats;
        }

        [System.Serializable]
        class FriendlyStatCollections
        {
            public AIBaseStat friendlyBaseStat;
            public float[] levels;
        }

    }
    
}

