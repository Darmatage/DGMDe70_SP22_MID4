using System.Collections;
using System.Collections.Generic;
using Game.Combat;
using Game.Enums;
using UnityEngine;

namespace Game.ClassTypes.Enemy
{
    public class EnemyClassSetup : MonoBehaviour
    {
        [Range(1,5)]
        [SerializeField] int difficultyLevel = 1;
        [SerializeField] EnemyType enemyType;
        [SerializeField] WeaponAttackType enemyAttackType;
        [SerializeField] float movementSpeed = 1f;
        [SerializeField] SO_EnemyClassStats enemyClassStats = null;
        public float GetStat(AIBaseStat stat)
        {
            return (GetBaseStat(stat));
        }
        public WeaponAttackType GetEnemyAttackType()
        {
            return enemyAttackType;
        }
        public float GetAttackDamage()
        {
            float addedAttackDamage;
            if(enemyAttackType == WeaponAttackType.Melee)
            {
                addedAttackDamage = enemyClassStats.GetEnemyMeleeAttackDetails(enemyType).attackDamage;
            }
            else if (enemyAttackType == WeaponAttackType.Range)
            {
                addedAttackDamage = enemyClassStats.GetEnemyRangeAttackDetails(enemyType).attackDamage;
            }
            else
            {
                addedAttackDamage = 0;
            }
            return (GetBaseStat(AIBaseStat.BaseDamage) + addedAttackDamage);
        }
        
        public float GetAttackRange()
        {
            float attackRange = 0f;
            if(enemyAttackType == WeaponAttackType.Melee)
            {
                attackRange = enemyClassStats.GetEnemyMeleeAttackDetails(enemyType).attackRange;
            }
            else if (enemyAttackType == WeaponAttackType.Range)
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
        private float GetBaseStat(AIBaseStat stat)
        {
            return enemyClassStats.GetStat(enemyType, stat, difficultyLevel);
        }
    }
    
}
