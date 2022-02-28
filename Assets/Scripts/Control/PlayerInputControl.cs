using System.Collections;
using System.Collections.Generic;
using Game.Movement;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Control
{
   public class PlayerInputControl : MonoBehaviour
    {
        [SerializeField] float timeBetweenActions = 0.75f; //move this to settings

        private bool canAttack = true;
        private bool isGamePaused = false;
        private float timeSinceLastAction = Mathf.Infinity;
        private Vector2 playerDirection = new Vector2(0,0);
        
        
        //Animation verables
        private float xInput;
        private float yInput;
        private bool isIdle;
        private bool isRunning;
        private bool isWalking;
        private bool isMakingAttack;
        private bool isAttackingUp;
        private bool isAttackingDown;
        private bool isAttackingLeft;
        private bool isAttackingRight;



        private void Awake() 
        {
            playerDirection = GetComponent<PlayerMovement>().GetPlayerVector2();
        }
        private void OnEnable()
        {
            EventHandler.ActiveGameUI += SetIsActiveGameUI;
        }

        private void OnDisable()
        {
            EventHandler.ActiveGameUI -= SetIsActiveGameUI;
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
            if (value.started && !isGamePaused)
            {
                DoPrimaryAction();
            }
        }

        public void SecondaryAction(InputAction.CallbackContext value)
        {
            if (value.started && !isGamePaused)
            {
                //DoSecondaryAction();
            }
        }


        //PRIVATE Functions
        private void DoPrimaryAction()
        {
            if(canAttack && timeSinceLastAction > timeBetweenActions)
            {
                ResetAnimationTriggers();
                MakeAttackAction();
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

            isMakingAttack = true;
            timeSinceLastAction = 0;
        }

        private void SetIsActiveGameUI(bool setGamePaused)
        {
            isGamePaused = setGamePaused;
        }

        private void CallAnimationEvent()
        {
            // Send event to any listeners for player movement input
            EventHandler.CallPlayerInputEvent(0f, 0f, false, false, false, isMakingAttack,
                isAttackingRight, isAttackingLeft, isAttackingUp, isAttackingDown,
                false, false, false, false);
        }

        private void ResetAnimationTriggers()
        {
            isMakingAttack = false;
            isAttackingRight = false;
            isAttackingLeft = false;
            isAttackingUp = false;
            isAttackingDown = false;
        }
    } 
}

