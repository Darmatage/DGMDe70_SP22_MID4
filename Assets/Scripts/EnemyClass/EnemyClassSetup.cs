using System.Collections;
using System.Collections.Generic;
using Game.Combat;
using Game.Enums;
using UnityEngine;

namespace Game.EnemyClass
{
    public class EnemyClassSetup : MonoBehaviour
    {
        [Range(1,5)]
        [SerializeField] int difficultyLevel = 1;
        [SerializeField] EnemyType enemyType;
        [SerializeField] EnemyAttackType enemyAttackType;
        [SerializeField] float movementSpeed = 1f;
        [SerializeField] SO_EnemyClassStats enemyClassStats = null;
        public float GetStat(EnemyBaseStat stat)
        {
            return (GetBaseStat(stat));
        }
        public EnemyAttackType GetEnemyAttackType()
        {
            return enemyAttackType;
        }
        public float GetAttackDamage()
        {
            float addedAttackDamage;
            if(enemyAttackType == EnemyAttackType.Melee)
            {
                addedAttackDamage = enemyClassStats.GetEnemyMeleeAttackDetails(enemyType).attackDamage;
            }
            else if (enemyAttackType == EnemyAttackType.Range)
            {
                addedAttackDamage = enemyClassStats.GetEnemyRangeAttackDetails(enemyType).attackDamage;
            }
            else
            {
                addedAttackDamage = 0;
            }
            return (GetBaseStat(EnemyBaseStat.BaseDamage) + addedAttackDamage);
        }
        
        public float GetAttackRange()
        {
            float attackRange = 0f;
            if(enemyAttackType == EnemyAttackType.Melee)
            {
                attackRange = enemyClassStats.GetEnemyMeleeAttackDetails(enemyType).attackRange;
            }
            else if (enemyAttackType == EnemyAttackType.Range)
            {
                attackRange = enemyClassStats.GetEnemyRangeAttackDetails(enemyType).attackRange;
            }
            else
            {
                return attackRange;
            }
            return attackRange;
        }

        public EnemyProjectile GetEnemyProjectile()
        {
            return enemyClassStats.GetEnemyProjectile(enemyType);
        }

        public int GetDifficultyLevel()
        {
            return difficultyLevel;
        }
        public float GetMovementSpeed()
        {
            return movementSpeed;
        }
        private float GetBaseStat(EnemyBaseStat stat)
        {
            return enemyClassStats.GetStat(enemyType, stat, difficultyLevel);
        }
    }
    
}
