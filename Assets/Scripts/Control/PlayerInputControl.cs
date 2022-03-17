using System;
using System.Collections;
using System.Collections.Generic;
using Game.Enums;
using Game.Inventories;
using Game.Movement;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Control
{
   public class PlayerInputControl : MonoBehaviour
    {
        private bool canAttack = true;
        private bool isGamePaused = false;
        
        private Vector2 playerDirection = new Vector2(0,0);
        private Rigidbody2D playerRigidbody2D;
        private ActionStore actionStore;
        
        
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
            playerRigidbody2D = GetComponent<Rigidbody2D>();
            actionStore = GetComponent<ActionStore>();
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
            playerDirection = GetComponent<PlayerMovement>().GetPlayerVector2();
            
            Debug.DrawRay(playerRigidbody2D.position, playerDirection, Color.green); //Visilize the Raycast for interaction

            if (InteractWithComponent()) return;
        }

        public void PrimaryAction(InputAction.CallbackContext value) //<- Left mouse button
        {
            if (value.started && !isGamePaused)
            {
                DoAction(ActionTypes.Attack);
            }
        }

        public void SecondaryAction(InputAction.CallbackContext value)
        {
            if (value.started && !isGamePaused)
            {
                //DoSecondaryAction();
            }
        }
        public void InteractAction(InputAction.CallbackContext value) //<- E Key
        {
            if (value.started)
            {
                EventHandler.CallInteractActionKeyEvent(true);
                Debug.Log("Started interaction");
            }
            if (value.canceled)
            {
                EventHandler.CallInteractActionKeyEvent(false);
            }
        }

        public void EscapeAction(InputAction.CallbackContext value) //<- Esc Key
        {
            
            if (value.started)
            {
                EventHandler.CallEscapeActionEvent();
            }
            if (value.canceled)
            {
            }
        }

        public void InventoryAction(InputAction.CallbackContext value) //<- I Key
        {
            
            if (value.started)
            {
                EventHandler.CallInventoryActionEvent();
            }
            if (value.canceled)
            {
            }
        }

        public void TransformAction(InputAction.CallbackContext value) //<- M key
        {
            if (value.started)
            {
                EventHandler.CallTransformEvent();
            }
        }

        public void UseAction(InputAction.CallbackContext value) //Keys: 1, 2, 3, 4, 5
        {
            if (value.started)
            {
                string keyPress = value.control.path.ToString();
                int keyPressIndex = Int32.Parse(keyPress.Substring(keyPress.Length - 1)) - 1;
                actionStore.Use(keyPressIndex, gameObject);
            }
        }


        //Private Functions
        private void DoAction(ActionTypes actionTypes)
        {
            if(actionTypes == ActionTypes.Attack && canAttack)
            {
                ResetAnimationTriggers();
                MakeAttackAction();
                CallAnimationEvent();
            }
            else
            {
                //EventHandler.CallInteractActionKeyEvent(false);
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
        }

        private bool InteractWithComponent()
        {
            RaycastHit2D[] hits = RaycastAllSorted();
            foreach (RaycastHit2D hit in hits)
            {
                IRaycastable[] raycastables = hit.transform.GetComponents<IRaycastable>();
                foreach (IRaycastable raycastable in raycastables)
                {
                    if (raycastable.HandleRaycast(this))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        RaycastHit2D[] RaycastAllSorted()
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(playerRigidbody2D.position, playerDirection, 1f);
            float[] distances = new float[hits.Length];
            for (int i = 0; i < hits.Length; i++)
            {
                distances[i] = hits[i].distance;
            }
            Array.Sort(distances, hits);
            return hits;
        }

        private void SetIsActiveGameUI(bool setGamePaused)
        {
            isGamePaused = setGamePaused;
        }

        private void CallAnimationEvent()
        {
            // Send event to any listeners for player movement input
            EventHandler.CallPlayerInputEvent(0f, 0f, false, false, false, isMakingAttack,
                isAttackingUp, isAttackingRight, isAttackingDown, isAttackingLeft,
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

