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
        [SerializeField] GameObject npmImage;
        GameDialogue gameDialogue = new GameDialogue();

        private void OnEnable() 
        {
            EventHandler.DialogueActionEvent += OpenScreen;
        }

        private void OnDisable() 
        {
            EventHandler.DialogueActionEvent -= OpenScreen;
        }

        private void LoadNPCSprite(CutSceneDestinationIdentifier cutSceneDestinationIdentifier) {
            return;
            Image image = npmImage.GetComponent<Image>();
            Sprite sprite;
            String spritePath;

            spritePath = "human_thumbnail";

            sprite = Resources.Load<Sprite>(spritePath);

            /*switch (cutSceneDestinationIdentifier) {

            }*/

            image.sprite = sprite;
        }

        public void OpenScreen(CutSceneDestinationIdentifier cutSceneDestinationIdentifier) 
        {
            LoadNPCSprite(cutSceneDestinationIdentifier);

            DialogueVariant variant = DialogueVariant.DV_01;
            // Debug.Log("This is the: " + cutSceneDestinationIdentifier.ToString());
            
            bool isMonster = GameObject.FindWithTag(Tags.PLAYER_TAG).GetComponent<PlayerTransformControl>().IsMonster;

            TMP_Text textBox = dialogueBox.FindComponentInChildrenWithTag<TMP_Text>(Tags.UI_DIALOGUE_SPEECHTEXT_TAG);
            textBox.text = gameDialogue.getDialogue(GameScene.Instance.currentScene, GameScene.Instance.currentStage, cutSceneDestinationIdentifier, isMonster, variant);

            Button closeDialogueButton = dialogueBox.FindComponentInChildrenWithTag<Button>(Tags.UI_BUTTON_DIALOGUE_CLOSE_TAG);
            closeDialogueButton.onClick.AddListener(() => 
            {
                EventHandler.CallCloseAllUIActionEvent();
            });   
        }
        public void OpenScreen(CutSceneDestinationIdentifier cutSceneDestinationIdentifier, DialogueVariant variant = DialogueVariant.DV_01) 
        {
            LoadNPCSprite(cutSceneDestinationIdentifier);
            
            // Debug.Log("This is the: " + cutSceneDestinationIdentifier.ToString());

            bool isMonster = GameObject.FindWithTag(Tags.PLAYER_TAG).GetComponent<PlayerTransformControl>().IsMonster;

            TMP_Text textBox = dialogueBox.FindComponentInChildrenWithTag<TMP_Text>(Tags.UI_DIALOGUE_SPEECHTEXT_TAG);
            textBox.text = gameDialogue.getDialogue(GameScene.Instance.currentScene, GameScene.Instance.currentStage, cutSceneDestinationIdentifier, isMonster, variant);

            Button closeDialogueButton = dialogueBox.FindComponentInChildrenWithTag<Button>(Tags.UI_BUTTON_DIALOGUE_CLOSE_TAG);
            closeDialogueButton.onClick.AddListener(() => 
            {
                EventHandler.CallCloseAllUIActionEvent();
            });   
        }
    }
}
