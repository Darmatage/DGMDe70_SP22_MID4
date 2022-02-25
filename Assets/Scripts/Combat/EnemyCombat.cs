using System.Collections;
using System.Collections.Generic;
using Game.Core;
using Game.Movement;
using UnityEngine;

namespace Game.Combat
{
    public class EnemyCombat : MonoBehaviour, IAction
    {
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float attackDamage = 5f;
        [SerializeField] float attackRange = 1f;
        PlayerHealth target;
        float timeSinceLastAttack = Mathf.Infinity;


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

            target.TakeDamage(attackDamage);

        }

        private bool GetIsInRange()
        {
            return Vector2.Distance(transform.position, target.transform.position) < attackRange;
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
