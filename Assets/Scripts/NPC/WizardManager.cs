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
    public class WizardManager : MonoBehaviour, IRaycastable
    {
        [SerializeField] GameObject interactionIndicatorUI = null;

        // [SerializeField] DialogueManager dialogueUIManager = null;

        DialogueManager dialogue;
        private bool isKeyActive = false;
        private bool isRaycastOn = false;

        private void Awake() {
            // dialogueUIManager = GameObject.FindWithTag(Tags.UI_DIALOGUE_CONTAINER_TAG).GetComponent<DialogueManager>();
        }
        
        private void OnEnable()
        {
            EventHandler.InteractActionKeyEvent += InteractActionActivateWizard;
        }
        private void OnDisable()
        {
            EventHandler.InteractActionKeyEvent -= InteractActionActivateWizard;
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
                Debug.Log("Activate Wizard");

                EventHandler.CallDialogueActionEvent(CutSceneDestinationIdentifier.Wizard);
            }
            EventHandler.CallInteractActionKeyEvent(false);
            return true;
        }

        private void InteractActionActivateWizard(bool isKeyPressed)
        {
            isKeyActive = isKeyPressed;
        }

    }
    
}
