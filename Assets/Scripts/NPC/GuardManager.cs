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
    public class GuardManager : MonoBehaviour, IRaycastable
    {
        [SerializeField] GameObject interactionIndicatorUI = null;

        DialogueManager dialogue;
        private bool isKeyActive = false;
        private bool isRaycastOn = false;
        
        private void OnEnable()
        {
            EventHandler.InteractActionKeyEvent += InteractActionActivateGuard;
        }
        private void OnDisable()
        {
            EventHandler.InteractActionKeyEvent -= InteractActionActivateGuard;
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
                Debug.Log("Activate Guard");

                EventHandler.CallDialogueActionEvent(CutSceneDestinationIdentifier.Guard);
            }

            EventHandler.CallInteractActionKeyEvent(false);

            return true;
        }

        private void InteractActionActivateGuard(bool isKeyPressed)
        {
            isKeyActive = isKeyPressed;
        }

    }
    
}
