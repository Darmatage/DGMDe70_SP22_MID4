using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Story;

public class DialogueManager : MonoBehaviour
{
    public GameObject speechBubble;
    public Text textField;
    public DialogueScene01 dialogueScene01 = new DialogueScene01();

    void Awake() {
        if (GameObject.FindWithTag("SpeechBubble") != null) {
            speechBubble = GameObject.FindWithTag("SpeechBubble");
        }
    }

    void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        OnDialogueScene();
    }

    void OnDialogueScene() {
        if (speechBubble == null) {
            return;
        }

        speechBubble.SetActive(true);
        Text textBox = GameObject.FindWithTag("SpeechText").GetComponent<Text>();
        textBox.text = dialogueScene01.hello();
        Debug.Log(dialogueScene01.hello());
    }
}
