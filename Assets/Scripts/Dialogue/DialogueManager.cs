using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Game.Enums;
using System;

namespace Game.Story
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] GameObject dialogueScreen;
        [SerializeField] GameObject dialogueBox;
        DialogueScene01 dialogueScene01 = new DialogueScene01();

        /*void Awake() {
            dialogueScreen = GameObject.Find("DialogueScreen");
            textField = GameObject.Find("TextField");
        }*/

        private void OnEnable() 
        {
            EventHandler.DialogueActionEvent += OpenScreen; 
        }

        private void OnDisable() 
        {
            EventHandler.DialogueActionEvent -= OpenScreen;
        }

        void Update() {
            // if (Input.GetMouseButtonDown(0)) {   
            //     CloseScreen();
            // }
        }

        public void CloseScreen() {
            // dialogueScreen = GameObject.Find("DialogueScreen");
            //dialogueScreen.SetActive(false);
        }

        public void OpenScreen(CutSceneDestinationIdentifier cutSceneDestinationIdentifier) 
        {
            // dialogueScreen = GameObject.Find("DialogueScreen");
            // dialogueScreen.SetActive(true);

            // textField = GameObject.Find("TextField");
            TMP_Text textBox = dialogueBox.FindComponentInChildrenWithTag<TMP_Text>(Tags.UI_DIALOGUE_SPEECHTEXT_TAG); //<- TMP_Text instead of TextMeshPro
            // textBox.text = dialogueScene01.hello();

            textBox.text = String.Format("Sup from the {0}!", cutSceneDestinationIdentifier);


            Button closeDialogueButton = dialogueBox.FindComponentInChildrenWithTag<Button>(Tags.UI_BUTTON_DIALOGUE_CLOSE_TAG);
            closeDialogueButton.onClick.AddListener(() => 
            {
                EventHandler.CallCloseAllUIActionEvent();
            });

            //Debug.Log("This is the: " + cutSceneDestinationIdentifier);
        }

        /*private void OnEnable() 
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
            OnDialogueScene();
        }

        void OnDialogueScene() {
            if (dialogueScreen == null) {
                return;
            }

            dialogueScreen.SetActive(true);
            // Text textBox = GameObject.FindWithTag("SpeechText").GetComponent<Text>();
           //  textBox.text = dialogueScene01.hello();
            textField.text = dialogueScene01.hello();
            Debug.Log(dialogueScene01.hello());
        }*/
    }
}
