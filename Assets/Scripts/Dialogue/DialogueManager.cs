using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

namespace Game.Story
{
    public class DialogueManager : MonoBehaviour
    {
        public GameObject dialogueScreen;
        public GameObject textField;
        DialogueScene01 dialogueScene01 = new DialogueScene01();

        /*void Awake() {
            dialogueScreen = GameObject.Find("DialogueScreen");
            textField = GameObject.Find("TextField");
        }*/

        void Update() {
            if (Input.GetMouseButtonDown(0)) {   
                CloseScreen();
            }
        }

        public void CloseScreen() {
            // dialogueScreen = GameObject.Find("DialogueScreen");
            dialogueScreen.SetActive(false);
        }

        public void OpenScreen() {
            // dialogueScreen = GameObject.Find("DialogueScreen");
            dialogueScreen.SetActive(true);

            // textField = GameObject.Find("TextField");
            TextMeshPro textBox = textField.GetComponent<TextMeshPro>();
            // textBox.text = dialogueScene01.hello();
            textBox.text = "Sup";
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
