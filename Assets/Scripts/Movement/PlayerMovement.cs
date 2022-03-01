using System.Collections;
using System.Collections.Generic;
using Game.Saving;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Movement
{
    public class PlayerMovement : MonoBehaviour//, ISaveable <- Need to work on recovering the players placement between scenes.
    {
        [SerializeField] float runningSpeed = 15f;
        [SerializeField] float walkingSpeed = 10f;
        private float movementSpeed;
        private bool runningButtonHeld = false;

        // Movement Parameters
        private float xInput;
        private float yInput;
        private bool isIdle;
        private bool isRunning;
        private bool isWalking;
        private bool isAttackingUp;
        private bool isAttackingDown;
        private bool isAttackingLeft;
        private bool isAttackingRight;

        private Rigidbody2D playerRigidbody;
        public Rigidbody2D GetPlayerRigidbody() { return playerRigidbody; }

        private Vector2 moveInput;
        private Vector2 lookDirection = new Vector2(1,0);
        public Vector2 GetPlayerVector2() { return lookDirection; }
        private bool PlayerInputIsDisabled = false;
        //public bool PlayerInputIsDisabled { get => _playerInputIsDisabled; set => _playerInputIsDisabled = value; }

        private void Awake() 
        {
            playerRigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            EventHandler.ActiveGameUI += SetPlayerInput;
        }

        private void OnDisable()
        {
            EventHandler.ActiveGameUI -= SetPlayerInput;
        }
        private void FixedUpdate()
        {
           
            if (!PlayerInputIsDisabled)
            {
                PlayerMove(moveInput);
            }
        }

        private void Update()
        {
            #region Player Input

                if (!PlayerInputIsDisabled)
                {
                ResetAnimationTriggers();
                PlayerMovementInput();
                
                PlayerRunInput();

                // Send event to any listeners for player movement input
                EventHandler.CallPlayerInputEvent(xInput, yInput, isWalking, isRunning, isIdle, false,
                    isAttackingRight, isAttackingLeft, isAttackingUp, isAttackingDown,
                    false, false, false, false);
                }

            #endregion Player Input
        }
        
        public void Move(InputAction.CallbackContext value)
        {
            moveInput = value.ReadValue<Vector2>();
        }

        public void PlayerMove(Vector2 inputMovement)
        {
            Vector2 playerVelocity = new Vector2 (inputMovement.x * movementSpeed * Time.deltaTime, inputMovement.y * movementSpeed * Time.deltaTime);
            playerRigidbody.MovePosition(playerRigidbody.position + playerVelocity);

            if(!Mathf.Approximately(inputMovement.x, 0.0f) || !Mathf.Approximately(inputMovement.y, 0.0f))
            {
                lookDirection.Set(inputMovement.x, inputMovement.y);
                lookDirection.Normalize();
            }

        }

        private void PlayerMovementInput()
        {
            xInput = moveInput.x;
            yInput = moveInput.y;

            //Debug.Log("PlayerMovementInput: " + xInput + ", " + yInput);

            if (yInput != 0 && xInput != 0)
            {
                xInput = xInput * 0.71f;
                yInput = yInput * 0.71f;
            }

            if (xInput != 0 || yInput != 0)
            {
                
                isWalking = true;
                isRunning = false;
                isIdle = false;
                movementSpeed = walkingSpeed;
            }
            else if (xInput == 0 && yInput == 0)
            {
                isRunning = false;
                isWalking = false;
                isIdle = true;
            }
        }

        public void PlayerRunInput()
        {
            if (runningButtonHeld)
            {
                isWalking = false;
                isRunning = true;
                isIdle = false;
                movementSpeed = runningSpeed;
            }
            else
            {
                isWalking = true;
                isRunning = false;
                isIdle = false;
                movementSpeed = walkingSpeed;
            }
        }

        private void ResetMovement()
        {
            // Reset movement
            xInput = 0f;
            yInput = 0f;
            isRunning = false;
            isWalking = false;
            isIdle = true;
        }

        public void DisablePlayerInputAndResetMovement()
        {
            DisablePlayerInput();
            ResetMovement();

            // Send event to any listeners for player movement input
                EventHandler.CallPlayerInputEvent(xInput, yInput, isWalking, isRunning, isIdle, false,
                    isAttackingRight, isAttackingLeft, isAttackingUp, isAttackingDown,
                    false, false, false, false);
        }

        private void DisablePlayerInput()
        {
            PlayerInputIsDisabled = true;
        }

        private void SetPlayerInput(bool setGamePaused)
        {
            PlayerInputIsDisabled = setGamePaused;
        }

        private void ResetAnimationTriggers()
        {
            isAttackingRight = false;
            isAttackingLeft = false;
            isAttackingUp = false;
            isAttackingDown = false;
        }

        // public object CaptureState()
        // {
        //     return new SerializableVector2(transform.position);
        // }

        // public void RestoreState(object state)
        // {
        //     SerializableVector2 position = (SerializableVector2)state;
        //     transform.position = position.ToVector();
        // }
    }
}

