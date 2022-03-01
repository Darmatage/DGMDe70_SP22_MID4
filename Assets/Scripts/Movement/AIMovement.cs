using System.Collections;
using System.Collections.Generic;
using Game.Core;
using Game.EnemyClass;
using UnityEngine;

namespace Game.Movement
{
    public class AIMovement : MonoBehaviour, IAction
    {
        private float enemySpeed;
        private Vector2 lookDirection = new Vector2(0,0);
        private Rigidbody2D enemyRigidbody;
        private Animator animator;
        private Vector2 posLastFrame;
        private Vector2 posThisFrame;
        private bool canMove = true;
        private float randomNum;
        private Vector2 movement;

        private void Awake() 
        {
            enemyRigidbody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            enemySpeed = GetComponent<EnemyClassSetup>().GetMovementSpeed();
        }
        private void Start() 
        {
            StartCoroutine(ChangeDirectionRoutine());
        }

        private void Update()
        {
            posLastFrame = posThisFrame;
            posThisFrame = transform.position;

            UpdateAnimator();
        }
        
        public void StartMoveAction(Vector2 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination);
        }

        public void MoveTo(Vector2 destination)
        {
            float step = enemySpeed * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, destination, step);
            lookDirection.Set(posThisFrame.x - posLastFrame.x, posThisFrame.y - posLastFrame.y);
            lookDirection.Normalize();
            
            Vector2 v2 = new Vector2(0, 0);
            v2 = transform.position;
            Debug.DrawRay(v2 + Vector2.up * 0.2f, lookDirection, Color.red); //Visilize the Raycast

        }

        public void Cancel()
        {
        }

        public void MoveAround() {
            if (!canMove) { return; }
            enemyRigidbody.MovePosition(enemyRigidbody.position + movement * enemySpeed * Time.fixedDeltaTime);
        }

        // random movement around map
        private IEnumerator ChangeDirectionRoutine() {
            while (true) {
                randomNum = Random.Range(-5, 5);
                movement.x = Random.Range(-1, 2);
                movement.y = Random.Range(-1, 2);
                yield return new WaitForSeconds(randomNum);
            }
        }

        private void UpdateAnimator()
        {
        }
    }
    
}
