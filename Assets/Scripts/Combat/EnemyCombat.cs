using System.Collections;
using System.Collections.Generic;
using Game.Core;
using Game.EnemyClass;
using Game.Enums;
using Game.Movement;
using Game.Utils;
using UnityEngine;

namespace Game.Combat
{
    public class EnemyCombat : MonoBehaviour, IAction
    {
        [SerializeField] Transform rangeAttackLaunchPosition = null;
        [SerializeField] float timeBetweenAttacks = 1f;
        private EnemyClassSetup enemyClass;
        private LazyValue<WeaponAttackType> enemyAttackType;
        private LazyValue<float> attackDamage;
        private LazyValue<float> attackRange;
        private PlayerHealth target;
        private EnemyProjectile projectile = null;
        private float timeSinceLastAttack = Mathf.Infinity;

        //LazyValue is used to initialize the values before they are needed as to reduce the posibility of getting null values.
        private void Awake() {
            attackDamage = new LazyValue<float>(GetInitialAttackDamage);
            enemyAttackType = new LazyValue<WeaponAttackType>(GetInitialAttackType);
            attackRange = new LazyValue<float>(GetInitialAttackRange);
        }
        private float GetInitialAttackDamage()
        {
            return enemyClass.GetAttackDamage();
        }
        private WeaponAttackType GetInitialAttackType()
        {
            return enemyClass.GetEnemyAttackType();
        }
        private float GetInitialAttackRange()
        {
            return enemyClass.GetAttackRange();
        }
        private void Start() 
        {
            enemyClass = GetComponent<EnemyClassSetup>();
            attackDamage.ForceInit();
            enemyAttackType.ForceInit();
            attackRange.ForceInit(); 
            SetupProjectile();
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;
            if (target.IsDead()) return;

            if (!GetIsInRange())
            {
                GetComponent<AIMovement>().MoveTo(target.transform.position);
            }
            else
            {
                GetComponent<AIMovement>().Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                TriggerAttack();
                timeSinceLastAttack = 0;
            }
        }

        private void TriggerAttack()
        {
            Hit();
        }

        void Hit()
        {
            if(target == null) { return; }

            if(enemyAttackType.value == WeaponAttackType.Range && HasProjectile())
            {
                LaunchProjectile();
                print("AI Shoot Projectile");
            }
            else
            {
                target.TakeDamage(attackDamage.value);
                print("AI Did Damage");
            }

        }

        private void SetupProjectile()
        {
            if(HasProjectile())
            {
                projectile = enemyClass.GetEnemyProjectile();
            }
        }
        private void LaunchProjectile()
        {
            EnemyProjectile projectInstance = Instantiate(projectile, rangeAttackLaunchPosition);
            projectInstance.SetTarget(target, attackDamage.value);
        }

        private bool HasProjectile()
        {
            return enemyClass.GetEnemyProjectile() != null;
        }

        private bool GetIsInRange()
        {
            return Vector2.Distance(transform.position, target.transform.position) < attackRange.value;
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) { return false; }
            PlayerHealth targetToTest = combatTarget.GetComponent<PlayerHealth>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<PlayerHealth>();
        }

        public void Cancel()
        {
            StopAttack();
            target = null;
        }

        private void StopAttack()
        {
            //Stop the attack animations 
        }
    }
    
}
