using System.Collections;
using System.Collections.Generic;
using Game.Core;
using UnityEngine;

namespace Game.Movement
{
    public class AIMovement : MonoBehaviour, IAction
    {
        [SerializeField] Transform target;
        [SerializeField] float enemySpeed = 1f;
        private Vector2 lookDirection = new Vector2(0,0);
        private Rigidbody2D enemyRigidbody;
        private Animator animator;
        private Vector2 posLastFrame;
        private Vector2 posThisFrame;

        private void Awake() 
        {
            enemyRigidbody = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        void Update()
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

        private void UpdateAnimator()
        {
        }
    }
    
}

