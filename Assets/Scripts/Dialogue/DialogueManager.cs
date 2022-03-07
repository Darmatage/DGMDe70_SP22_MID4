using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Story
{
    public class DialogueManager : MonoBehaviour
    {
        public GameObject dialogueScreen;
        public Text textField;
        public DialogueScene01 dialogueScene01 = new DialogueScene01();

        void Awake() {
            if (GameObject.FindWithTag("DialogueScreen") != null) {
                dialogueScreen = GameObject.FindWithTag("DialogueScreen");
                textField = dialogueScreen.GetComponent<Text>();
            }
        }

        void Update() {
            if (Input.GetMouseButtonDown(0)) {   
                CloseScreen();
            }
        }

        public void CloseScreen() {
            dialogueScreen.SetActive(false);
        }

        public void OpenScreen() {
            dialogueScreen.SetActive(true);

            textField.text = dialogueScene01.hello();
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
