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

        [SerializeField] DialogueManager dialogueUIManager = null;

        DialogueManager dialogue;
        public DialogueScene01 dialogueScene01 = new DialogueScene01();
        private bool isKeyActive = false;
        private bool isRaycastOn = false;

        private void Awake() {
            // This is creating a new DialogueManager GameObject with the script on it at run time,
            // which is why the object can't be found.
            // GameObject gameObject = new GameObject("DialogueManager"); 
            // dialogue = gameObject.AddComponent<DialogueManager>();

            dialogueUIManager = GameObject.FindWithTag(Tags.UI_DIALOGUE_CONTAINER_TAG).GetComponent<DialogueManager>();



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
                // GameScene.Instance.ChangeScene(GameScenes.Dialogue);
                // GetComponent<NPCPortal>().GoToCutScene();

                EventHandler.CallDialogueActionEvent(CutSceneDestinationIdentifier.Wizard); //<- Changed it to use the event system, Decoupling its reliance on the dialogue manager and followed a similar patten to the other interactable objects.

                //dialogueUIManager.OpenScreen();

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
