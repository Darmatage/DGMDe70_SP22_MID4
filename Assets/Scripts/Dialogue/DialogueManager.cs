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
        public int dialogueChoice = 0;
        [SerializeField] GameObject dialogueScreen;
        [SerializeField] GameObject dialogueBox;
        [SerializeField] GameObject DialogueChoices;
        [SerializeField] GameObject DialogueChoiceButtonNext;
        [SerializeField] GameObject DialogueChoiceTitle;
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

        private void RefreshDialogue(CutSceneDestinationIdentifier cutSceneDestinationIdentifier, bool isMonster, DialogueVariant variant) {
            ShowText(cutSceneDestinationIdentifier, isMonster, variant);
        }

        private void ShowText(CutSceneDestinationIdentifier cutSceneDestinationIdentifier, bool isMonster, DialogueVariant variant) {
            dialogueChoice = 0;
            String[] dialogue = gameDialogue.getDialogue(GameScene.Instance.currentScene, GameScene.Instance.currentStage, cutSceneDestinationIdentifier, isMonster, variant);
            System.Action[] actions = gameDialogue.getActions(GameScene.Instance.currentScene, GameScene.Instance.currentStage, cutSceneDestinationIdentifier, isMonster, variant);

            // Normal Dialogue Text
            if (dialogue != null && dialogue.Length == 1) {
                DialogueChoices.SetActive(false);
                DialogueText.SetActive(true);

                TMP_Text textBox = DialogueText.GetComponent<TMP_Text>();
                textBox.text = dialogue[0];

                // If there should be a next button
                if (actions != null && actions.Length > 0) {
                    DialogueChoiceButtonNext.SetActive(true);

                    Button next = DialogueChoiceButtonNext.GetComponent<Button>();
                    next.onClick.AddListener(() => {
                        GameScene.Instance.AdvanceStage();
                        RefreshDialogue(cutSceneDestinationIdentifier, isMonster, variant);
                    });
                }
                else {
                    DialogueChoiceButtonNext.SetActive(false);
                }
            }

            // Choices
            else {
                DialogueChoices.SetActive(true);
                DialogueText.SetActive(false);
                DialogueChoiceButtonNext.SetActive(false);

                /*for(int i = 0; i < dialogue.Length; i++) {
                    Debug.Log("Dialogue:" + dialogue[i]);
                }*/

                TMP_Text title =  DialogueChoiceTitle.GetComponent<TMP_Text>();
                title.text = dialogue[0];

                if (dialogue.Length > 1) {
                    DialogueChoiceButton1.SetActive(true);
                    TMP_Text choice1 =  DialogueChoiceText1.GetComponent<TMP_Text>();
                    choice1.text = dialogue[1];
                    Button button1 = DialogueChoiceButton1.GetComponent<Button>();
                    button1.onClick.AddListener(() => {
                        actions[0]();
                        dialogueChoice = 1;
                        GameScene.Instance.AdvanceStage();
                        RefreshDialogue(cutSceneDestinationIdentifier, isMonster, variant);
                    });
                }
                else {
                    DialogueChoiceButton1.SetActive(false);
                }

                if (dialogue.Length > 2) {
                    DialogueChoiceButton2.SetActive(true);
                    TMP_Text choice2 =  DialogueChoiceText2.GetComponent<TMP_Text>();
                    choice2.text = dialogue[2];
                    Button button2 = DialogueChoiceButton2.GetComponent<Button>();
                    button2.onClick.AddListener(() => {
                        actions[1]();
                        dialogueChoice = 2;
                        GameScene.Instance.AdvanceStage();
                        RefreshDialogue(cutSceneDestinationIdentifier, isMonster, variant);
                    });
                }
                else {
                    DialogueChoiceButton2.SetActive(false);
                }

                if (dialogue.Length > 3) {
                    DialogueChoiceButton3.SetActive(true);
                    TMP_Text choice3 =  DialogueChoiceText3.GetComponent<TMP_Text>();
                    choice3.text = dialogue[3];
                    Button button3 = DialogueChoiceButton3.GetComponent<Button>();
                    button3.onClick.AddListener(() => {
                        actions[2]();
                        dialogueChoice = 3;
                        GameScene.Instance.AdvanceStage();
                        RefreshDialogue(cutSceneDestinationIdentifier, isMonster, variant);
                    });
                }
                else {
                    DialogueChoiceButton3.SetActive(false);
                }

                if (dialogue.Length > 4) {
                    DialogueChoiceButton4.SetActive(true);
                    TMP_Text choice4 =  DialogueChoiceText4.GetComponent<TMP_Text>();
                    choice4.text = dialogue[4];
                    Button button4 = DialogueChoiceButton4.GetComponent<Button>();
                    button4.onClick.AddListener(() => {
                        actions[3]();
                        dialogueChoice = 4;
                        GameScene.Instance.AdvanceStage();
                        RefreshDialogue(cutSceneDestinationIdentifier, isMonster, variant);
                    });
                }
                else {
                    DialogueChoiceButton4.SetActive(false);
                }
            }
        }
    }
}
