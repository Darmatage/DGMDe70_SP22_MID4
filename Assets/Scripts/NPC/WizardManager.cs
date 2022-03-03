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
        public DialogueScene01 dialogueScene01 = new DialogueScene01();
        private bool canInteract = false;
        
        private void OnEnable()
        {
            EventHandler.InteractActionEvent += InteractActionActivateWizard;
        }
        private void OnDisable()
        {
            EventHandler.InteractActionEvent -= InteractActionActivateWizard;
        }

        public bool HandleRaycast(PlayerInputControl callingController)
        {
            if(canInteract)
            {
                Debug.Log("Activate Wizard");
                //GameScene.Instance.ChangeScene(GameScenes.Dialogue);
                GetComponent<NPCPortal>().GoToCutScene();
                
            }
            EventHandler.CallInteractActionEvent(false);
            return true;
        }

        private void InteractActionActivateWizard(bool isKeyPressed)
        {
            canInteract = isKeyPressed;
        }
    }
    
}

