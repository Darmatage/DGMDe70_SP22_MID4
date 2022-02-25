using System.Collections;
using System.Collections.Generic;
using Game.Movement;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Control
{
   public class PlayerInputControl : MonoBehaviour
    {
        // The hitboxes of our 4 different directions
        [SerializeField] private GameObject hitBox_Up;
        [SerializeField] private GameObject hitBox_Bottom;
        [SerializeField] private GameObject hitBox_Left;
        [SerializeField] private GameObject hitBox_Right;
        [SerializeField] float timeBetweenActions = 0.75f; //move this to settings

        public bool canAttack = true;
        float timeSinceLastAction = Mathf.Infinity;
        Vector2 playerDirection = new Vector2(0,0);
        
        
        //Animation verables
        private float xInput;
        private float yInput;
        private bool isIdle;
        private bool isRunning;
        private bool isWalking;
        private bool isAttackingUp;
        private bool isAttackingDown;
        private bool isAttackingLeft;
        private bool isAttackingRight;



        private void Awake() 
        {
            playerDirection = GetComponent<PlayerMovement>().GetPlayerVector2();
        }
        private void Start()
        {
            ResetAnimationTriggers();
        }
        private void Update() 
        {    
            timeSinceLastAction += Time.deltaTime;
            playerDirection = GetComponent<PlayerMovement>().GetPlayerVector2();
        }

        public void PrimaryAction(InputAction.CallbackContext value)
        {
            if (value.started)
            {
                DoPrimaryAction();
            }
        }

        public void SecondaryAction(InputAction.CallbackContext value)
        {
            if (value.started)
            {
                //DoSecondaryAction();
            }
        }

        //Using the animation triggers to activate the attack areas -- Animation need to be setup with triggers
        //These shgould be moved to the weapon game object so that it can be activated by the animation or intergrate with event system 
        public void ActivateCollider() 
        {
            if (playerDirection == Vector2.up) 
            {
                hitBox_Up.SetActive(true);
            }
            if (playerDirection == Vector2.right) 
            {
                hitBox_Right.SetActive(true);
            }
            if (playerDirection == Vector2.left) 
            {
                hitBox_Left.SetActive(true);
            }
            if (playerDirection == Vector2.down) 
            {
                hitBox_Bottom.SetActive(true);
            }
        }

        public void canMoveFunction()
        {
            hitBox_Left.SetActive(false);
            hitBox_Right.SetActive(false);
            hitBox_Bottom.SetActive(false);
            hitBox_Up.SetActive(false);
        }

        //PRIVATE Functions
        private void DoPrimaryAction()
        {
            if(canAttack && timeSinceLastAction > timeBetweenActions)
            {
                ResetAnimationTriggers();
                MakeAttackAction();
                Debug.Log("Do primary action!");
                CallAnimationEvent();
            }
            else
            {
                ResetAnimationTriggers();
            }

        }
        private void MakeAttackAction()
        {
            if (playerDirection == Vector2.up)
            {
                isAttackingUp = true;
            }
            else if (playerDirection == Vector2.right)
            {
                isAttackingRight = true;
            }
            else if (playerDirection == Vector2.left)
            {
                isAttackingLeft = true;
            }
            else if (playerDirection == Vector2.down)
            {
                isAttackingDown = true;
            }
            else
            {
                ResetAnimationTriggers();
            }
                timeSinceLastAction = 0;
        }

        private void CallAnimationEvent()
        {
            // Send event to any listeners for player movement input
            EventHandler.CallMovementEvent(0f, 0f, false, false, false,
                isAttackingRight, isAttackingLeft, isAttackingUp, isAttackingDown,
                false, false, false, false);
        }

        private void ResetAnimationTriggers()
        {
            isAttackingRight = false;
            isAttackingLeft = false;
            isAttackingUp = false;
            isAttackingDown = false;
        }
    } 
}

