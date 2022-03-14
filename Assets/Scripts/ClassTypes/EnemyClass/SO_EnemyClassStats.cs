using System.Collections;
using System.Collections.Generic;
using Game.Combat;
using Game.Enums;
using UnityEngine;

namespace Game.ClassTypes.Enemy
{
    [CreateAssetMenu(fileName = "EnemyClassStats", menuName = "Game/Enemy/New Enemy Class Stats", order = 1)]
    public class SO_EnemyClassStats : ScriptableObject
    {
        [SerializeField] EnemyClassTypeCollection[] enemyClassTypeCollections = null;
        Dictionary<EnemyType, Dictionary<AIBaseStat, float[]>> lookupTable = null;

        public float GetStat(EnemyType enemyType, AIBaseStat stat,  int difficultyLevel)
        {
            BuildLookup();

            float[] levels = lookupTable[enemyType][stat];

            if(levels.Length < difficultyLevel)
            {
                return 0f;
            }

            return levels[difficultyLevel - 1];
        }

        public EnemyMeleeAttackDetails GetEnemyMeleeAttackDetails(EnemyType enemyType)
        {
            foreach(EnemyClassTypeCollection enemyClassTypeCollection in enemyClassTypeCollections)
            {
                if(enemyType == enemyClassTypeCollection.enemyType)
                {
                    return enemyClassTypeCollection.meleeAttackDetails;
                }
            }

            return null;
        }

        public EnemyRangeAttackDetails GetEnemyRangeAttackDetails(EnemyType enemyType)
        {
            foreach(EnemyClassTypeCollection enemyClassTypeCollection in enemyClassTypeCollections)
            {
                if(enemyType == enemyClassTypeCollection.enemyType)
                {
                    return enemyClassTypeCollection.rangeAttackDetails;
                }
            }
            
            return null;
        }
        public EnemyProjectile GetEnemyProjectile(EnemyType enemyType)
        {
            foreach(EnemyClassTypeCollection enemyClassTypeCollection in enemyClassTypeCollections)
            {
                if(enemyType == enemyClassTypeCollection.enemyType)
                {
                    return enemyClassTypeCollection.rangeAttackDetails.projectile;
                }
            }
            
            return null;
        }


        private void BuildLookup()
        {
            if(lookupTable != null) return;

            lookupTable = new Dictionary<EnemyType, Dictionary<AIBaseStat, float[]>>();

            foreach(EnemyClassTypeCollection enemyClassTypeCollection in enemyClassTypeCollections)
            {
                var stateLookupTable = new Dictionary<AIBaseStat, float[]>();

                foreach (EnemyStatCollections enemyStatItem in enemyClassTypeCollection.stats)
                {
                    stateLookupTable[enemyStatItem.enemyBaseStat] = enemyStatItem.levels;
                }

                lookupTable[enemyClassTypeCollection.enemyType] = stateLookupTable;
            }
        }

        [System.Serializable]
        class EnemyClassTypeCollection
        {
            public EnemyType enemyType;
            public EnemyStatCollections[] stats;
            public EnemyMeleeAttackDetails meleeAttackDetails;
            public EnemyRangeAttackDetails rangeAttackDetails;
        }

        [System.Serializable]
        class EnemyStatCollections
        {
            public AIBaseStat enemyBaseStat;
            public float[] levels;
        }

        [System.Serializable]
        public class EnemyMeleeAttackDetails
        {
            public float attackDamage = 0f;
            public float attackRange = 2f;
        }

        [System.Serializable]
        public class EnemyRangeAttackDetails
        {
            public float attackDamage = 0f;
            public float attackRange = 4f;
            public EnemyProjectile projectile = null;
            
        }

    }
    
}

