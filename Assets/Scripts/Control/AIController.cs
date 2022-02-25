using System.Collections;
using System.Collections.Generic;
using Game.Combat;
using Game.Core;
using Game.Movement;
using UnityEngine;

namespace Game.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float chaseDistance = 10f;
        [SerializeField] float suspicionTime = 2f;
        EnemyCombat combat;
        EnemyHealth health;
        AIMovement movement;
        GameObject player;
        Vector2 guardPosition;
        float timeSinceLastSawlayer = Mathf.Infinity;
        
        private void Awake() 
        {
            combat = GetComponent<EnemyCombat>();
            health = GetComponent<EnemyHealth>();
            movement = GetComponent<AIMovement>();
            player = GameObject.FindWithTag(Tags.PlayerTag);

            guardPosition = transform.position;
        }
        void Update()
        {
            if (health.IsDead()) return;
            if (InAttackRangeOfPlayer() && combat.CanAttack(player))
            {
                timeSinceLastSawlayer = 0;
                AttackBehaviour();
            }
            else if (timeSinceLastSawlayer < suspicionTime)
            {
                //Suspicion State
                SuspicionBehaviour();
            }
            else
            {
                //Guard State
                GuardBehavior();
            }

            timeSinceLastSawlayer += Time.deltaTime;
        }
        private void AttackBehaviour()
        {
            combat.Attack(player);
        }
        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
        private void GuardBehavior()
        {
            movement.StartMoveAction(guardPosition);
        }

        private bool InAttackRangeOfPlayer()
        {  
            float distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance;
        }

        //Called by Unity to visulize the guard area
        private void OnDrawGizmos() {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
    
}

