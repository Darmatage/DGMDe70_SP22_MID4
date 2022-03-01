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
        public DialogueScene02 dialogueScene02 = new DialogueScene02();
        private bool canInteract = false;

        // void OnCollisionExit2D(Collision2D player) {
        //     if (player.gameObject.tag == "Player") {
        //         canInteract = false;
        //     }
        // }

        // void OnCollisionStay2D(Collision2D player) {
        //     if (!canInteract && player.gameObject.tag == "Player") {
        //         canInteract = true;
        //     }
        // }

        // void Update() {
        //     if (canInteract) {
        //         if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
        //             // @TODO transition to EvilWizard dialogue scene
        //         }
        //     }
        // }

        private void OnEnable()
        {
            EventHandler.InteractActionEvent += InteractActionActivateEvilWizard;
        }
        private void OnDisable()
        {
            EventHandler.InteractActionEvent -= InteractActionActivateEvilWizard;
        }

        public bool HandleRaycast(PlayerInputControl callingController)
        {
            if(canInteract)
            {
                Debug.Log("Activate Evil Wizard");
                // @TODO transition to EvilWizard dialogue scene
                
            }
            EventHandler.CallInteractActionEvent(false);
            return true;
        }

        private void InteractActionActivateEvilWizard(bool isKeyPressed)
        {
            canInteract = isKeyPressed;
        }

    }
    
}

