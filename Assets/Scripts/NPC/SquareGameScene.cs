using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Story;
using Game.Control;
using Game.Enums;
using Game.SceneManagement;

namespace Game.NPC
{
    public class SquareGameScene : MonoBehaviour, IRaycastable
    {
        [SerializeField] GameObject interactionIndicatorUI = null;

        DialogueManager dialogue;
        private bool isKeyActive = false;
        private bool isRaycastOn = false;
        
        private void OnEnable()
        {
            EventHandler.InteractActionKeyEvent += InteractActionActivateScene;
        }
        private void OnDisable()
        {
            EventHandler.InteractActionKeyEvent -= InteractActionActivateScene;
        }

        private void Update() 
        {
            InteractUIDisplay();
            isRaycastOn = false;
        }
        public void InteractUIDisplay()
        {
            if(!interactionIndicatorUI) return;
            interactionIndicatorUI.SetActive(isRaycastOn);
        }

        public bool HandleRaycast(PlayerInputControl callingController)
        {
            isRaycastOn = true;

            if(isKeyActive)
            {
                Debug.Log("Scene Pressed");
                GameScene.Instance.AdvanceScene();
            }
            EventHandler.CallInteractActionKeyEvent(false);
            return true;
        }

        private void InteractActionActivateScene(bool isKeyPressed)
        {
            isKeyActive = isKeyPressed;
        }

    }
    
}
