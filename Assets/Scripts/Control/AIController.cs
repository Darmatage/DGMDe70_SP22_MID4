using System;
using System.Collections;
using System.Collections.Generic;
using Game.Combat;
using Game.Core;
using Game.Enums;
using Game.Movement;
using UnityEngine;

namespace Game.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float interactDistance = 4f;
        [SerializeField] float suspicionTime = 2f;
        [SerializeField] bool guardThisPosition = false;
        private EnemyCombat combat;
        private AIMovement movement;
        private GameObject player;
        private Vector2 guardPosition;

        private AIMotiveState aiMotive;
        
        float timeSinceLastSawPlayer = Mathf.Infinity;
        
        private void Awake() 
        {
            player = GameObject.FindWithTag(Tags.PLAYER_TAG);

            if(this.CompareTag(Tags.ENEMY_TAG)) EnemyAISetup();
            if(this.CompareTag(Tags.FRIENDLY_TAG)) FriendlyAISetup();
        }
        void Update()
        {
            if (aiMotive == AIMotiveState.Enemy) EnemyAIUpdates();
            if (aiMotive == AIMotiveState.Friendly) FriendlyAIUpdates();
        }

        private void EnemyAISetup()
        {
            aiMotive = AIMotiveState.Enemy;
            movement = GetComponent<AIMovement>();
            combat = GetComponent<EnemyCombat>();
            guardPosition = transform.position;
        }

        private void EnemyAIUpdates()
        {
            if (InAttackRangeOfPlayer() && combat.CanAttack(player) && !player.GetComponent<PlayerTransformControl>().IsMonster)
            {
                timeSinceLastSawPlayer = 0;
                AttackBehaviour();
            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                SuspicionBehaviour();
            }
            else if (guardThisPosition)
            {
                GuardBehavior();
            }
            else
            {
                WonderBehaviour();
            }

            timeSinceLastSawPlayer += Time.deltaTime;
        }

        private void FriendlyAISetup()
        {
            aiMotive = AIMotiveState.Friendly;
            movement = GetComponent<AIMovement>();
        }

        private void FriendlyAIUpdates()
        {
            if (InAttackRangeOfPlayer() && player.GetComponent<PlayerTransformControl>().IsMonster)
            {
                timeSinceLastSawPlayer = 0;
                FleeBehaviour(1.5f);
            }
            else if (IsBeingAttacked() && InAttackRangeOfPlayer())
            {
                timeSinceLastSawPlayer = 0;
                FleeBehaviour(1.75f);
            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                SuspicionBehaviour();
                GetComponent<FriendlyHealth>().SetBeingAttacker(false);
            }
            else
            {
                WonderBehaviour();
            }
            timeSinceLastSawPlayer += Time.deltaTime;
 
        }

        private bool IsBeingAttacked()
        {
            return GetComponent<FriendlyHealth>().GetBeingAttacker();
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
        private void FleeBehaviour(float speedMultiplier)
        {
            movement.MoveAway(player.transform.position, speedMultiplier);
        }
        private void WonderBehaviour()
        {
            movement.MoveAround();
        }

        private bool InAttackRangeOfPlayer()
        {  
            float distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);
            return distanceToPlayer < interactDistance;
        }

        //Called by Unity to visulize the guard area
        private void OnDrawGizmos() {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, interactDistance);
        }
    }
    
}

