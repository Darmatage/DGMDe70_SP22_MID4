using System.Collections;
using System.Collections.Generic;
using Game.Core;
using UnityEngine;
using Game.ClassTypes;

namespace Game.Movement
{
    public class AIMovement : MonoBehaviour, IAction
    {
        private float moveSpeed = 0;
        private float moveTowardsSpeed = 0;
        private float moveAwaySpeed = 0;
        private Vector2 lookDirection = new Vector2(0,0);
        private Rigidbody2D aiRigidbody;
        private Animator animator;
        private Vector2 posLastFrame;
        private Vector2 posThisFrame;
        private bool canMove = true;
        private float randomNum;
        private Vector2 movement;

        private void Awake() 
        {
            aiRigidbody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();

            moveSpeed = GetComponent<IClassSetup>().GetMovementSpeed();
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
            moveTowardsSpeed = moveSpeed * 1.25f;
            float step = moveTowardsSpeed * Time.deltaTime;

            transform.position = Vector2.MoveTowards(transform.position, destination, step);
            lookDirection.Set(posThisFrame.x - posLastFrame.x, posThisFrame.y - posLastFrame.y);
            lookDirection.Normalize();
        }
        public void MoveAway(Vector2 awayFrom, float speedMultiplier)
        {
            moveAwaySpeed = moveSpeed * speedMultiplier;
            float step = moveAwaySpeed * Time.fixedDeltaTime;

            lookDirection.Set((posThisFrame.x - awayFrom.x), (posThisFrame.y - awayFrom.y));
            lookDirection.Normalize();

            aiRigidbody.MovePosition(aiRigidbody.position + lookDirection * step);

            Debug.DrawRay(transform.position, lookDirection, Color.green); //Visilize the Raycast for interaction
        }

        public void Cancel()
        {
        }

        public void MoveAround() {
            if (!canMove) { return; }
            aiRigidbody.MovePosition(aiRigidbody.position + movement * moveSpeed * Time.fixedDeltaTime);
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

