using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Story;
using Game.Control;
using Game.Enums;

namespace Game.NPC
{
    public class EvilWizardManager : MonoBehaviour, IRaycastable
    {    
        [SerializeField] GameObject interactionIndicatorUI = null;
        public DialogueScene02 dialogueScene02 = new DialogueScene02();
        private bool isKeyActive = false;
        private bool isRaycastOn = false;

        private void OnEnable()
        {
            EventHandler.InteractActionKeyEvent += InteractActionActivateEvilWizard;
        }
        private void OnDisable()
        {
            EventHandler.InteractActionKeyEvent -= InteractActionActivateEvilWizard;
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
                Debug.Log("Activate Evil Wizard");
                // @TODO transition to EvilWizard dialogue scene
                
            }
            EventHandler.CallInteractActionKeyEvent(false);
            return true;
        }

        private void InteractActionActivateEvilWizard(bool isKeyPressed)
        {
            isKeyActive = isKeyPressed;
        }


    }
    
}

