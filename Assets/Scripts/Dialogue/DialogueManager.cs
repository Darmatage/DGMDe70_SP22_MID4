using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Game.Enums;
using System;
using Game.Control;

namespace Game.Story
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] GameObject dialogueScreen;
        [SerializeField] GameObject dialogueBox;
        GameDialogue gameDialogue = new GameDialogue();

        private void OnEnable() 
        {
            EventHandler.DialogueActionEvent += OpenScreen; 
        }

        private void OnDisable() 
        {
            EventHandler.DialogueActionEvent -= OpenScreen;
        }

        public void OpenScreen(CutSceneDestinationIdentifier cutSceneDestinationIdentifier) 
        {
            DialogueVariant variant = DialogueVariant.DV_01;
            Debug.Log("This is the: " + cutSceneDestinationIdentifier.ToString());
            
            bool isMonster = GameObject.FindWithTag(Tags.PLAYER_TAG).GetComponent<PlayerTransformControl>().IsMonster;

            TMP_Text textBox = dialogueBox.FindComponentInChildrenWithTag<TMP_Text>(Tags.UI_DIALOGUE_SPEECHTEXT_TAG); //<- TMP_Text instead of TextMeshPro
            textBox.text = gameDialogue.getDialogue(GameScene.Instance.currentScene, GameScene.Instance.currentStage, cutSceneDestinationIdentifier, isMonster, variant);

            Button closeDialogueButton = dialogueBox.FindComponentInChildrenWithTag<Button>(Tags.UI_BUTTON_DIALOGUE_CLOSE_TAG);
            closeDialogueButton.onClick.AddListener(() => 
            {
                EventHandler.CallCloseAllUIActionEvent();
            });   
        }
        public void OpenScreen(CutSceneDestinationIdentifier cutSceneDestinationIdentifier, DialogueVariant variant = DialogueVariant.DV_01) 
        {
            Debug.Log("This is the: " + cutSceneDestinationIdentifier.ToString());

            bool isMonster = GameObject.FindWithTag(Tags.PLAYER_TAG).GetComponent<PlayerTransformControl>().IsMonster;

            TMP_Text textBox = dialogueBox.FindComponentInChildrenWithTag<TMP_Text>(Tags.UI_DIALOGUE_SPEECHTEXT_TAG); //<- TMP_Text instead of TextMeshPro
            textBox.text = gameDialogue.getDialogue(GameScene.Instance.currentScene, GameScene.Instance.currentStage, cutSceneDestinationIdentifier, isMonster, variant);

            Button closeDialogueButton = dialogueBox.FindComponentInChildrenWithTag<Button>(Tags.UI_BUTTON_DIALOGUE_CLOSE_TAG);
            closeDialogueButton.onClick.AddListener(() => 
            {
                EventHandler.CallCloseAllUIActionEvent();
            });   
        }
    }
}
