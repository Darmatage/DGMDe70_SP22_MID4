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
        [SerializeField] GameObject DialogueChoices;
        [SerializeField] GameObject DialogueChoiceButton1;
        [SerializeField] GameObject DialogueChoiceButton2;
        [SerializeField] GameObject DialogueChoiceButton3;
        [SerializeField] GameObject DialogueChoiceButton4;
        [SerializeField] GameObject DialogueChoiceText1;
        [SerializeField] GameObject DialogueChoiceText2;
        [SerializeField] GameObject DialogueChoiceText3;
        [SerializeField] GameObject DialogueChoiceText4;
        [SerializeField] GameObject DialogueText;
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
            Image image = npmImage.GetComponent<Image>();
            Sprite sprite;
            String spritePath;

            spritePath = "human_thumbnail";

            switch (cutSceneDestinationIdentifier) {
                case CutSceneDestinationIdentifier.Wizard:
                    spritePath = "wizard_thumbnail"; break;
            }

            sprite = UnityEngine.Resources.Load<Sprite>(spritePath);

            image.sprite = sprite;
        }

        public void OpenScreen(CutSceneDestinationIdentifier cutSceneDestinationIdentifier) 
        {
            LoadNPCSprite(cutSceneDestinationIdentifier);

            DialogueVariant variant = DialogueVariant.DV_01;
            // Debug.Log("This is the: " + cutSceneDestinationIdentifier.ToString());
            
            bool isMonster = GameObject.FindWithTag(Tags.PLAYER_TAG).GetComponent<PlayerTransformControl>().IsMonster;

            ShowText(cutSceneDestinationIdentifier, isMonster, variant);

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

           ShowText(cutSceneDestinationIdentifier, isMonster, variant);

            Button closeDialogueButton = dialogueBox.FindComponentInChildrenWithTag<Button>(Tags.UI_BUTTON_DIALOGUE_CLOSE_TAG);
            closeDialogueButton.onClick.AddListener(() => 
            {
                EventHandler.CallCloseAllUIActionEvent();
            });   
        }

        private void ShowText(CutSceneDestinationIdentifier cutSceneDestinationIdentifier, bool isMonster, DialogueVariant variant) {
            String[] dialogue = gameDialogue.getDialogue(GameScene.Instance.currentScene, GameScene.Instance.currentStage, cutSceneDestinationIdentifier, isMonster, variant);

            // Normal Dialogue Text
            if (dialogue.Length == 1) {
                DialogueChoices.SetActive(false);
                DialogueText.SetActive(true);

                TMP_Text textBox = DialogueText.GetComponent<TMP_Text>();
                textBox.text = dialogue[0];
            }

            // Choices
            else {
                DialogueChoices.SetActive(true);
                DialogueText.SetActive(false);

                System.Action[] actions = gameDialogue.getActions(GameScene.Instance.currentScene, GameScene.Instance.currentStage, cutSceneDestinationIdentifier, isMonster, variant);

                /*for(int i = 0; i < dialogue.Length; i++) {
                    Debug.Log("Dialogue:" + dialogue[i]);
                }*/

                TMP_Text choice1 =  DialogueChoiceText1.GetComponent<TMP_Text>();
                choice1.text = dialogue[0];
                Button button1 = DialogueChoiceButton1.GetComponent<Button>();
                button1.onClick.AddListener(() => {
                    actions[0]();
                });

                TMP_Text choice2 =  DialogueChoiceText2.GetComponent<TMP_Text>();
                choice2.text = dialogue[1];

                TMP_Text choice3 =  DialogueChoiceText3.GetComponent<TMP_Text>();
                choice3.text = dialogue[2];

                TMP_Text choice4 =  DialogueChoiceText4.GetComponent<TMP_Text>();
                choice4.text = dialogue[3];
            }
            
        }
    }
}
