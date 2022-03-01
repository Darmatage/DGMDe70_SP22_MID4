using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Story;

public class DialogueManager : MonoBehaviour
{
    public GameObject speechBubble;
    public GameObject textField;
    public DialogueScene01 dialogueScene01 = new DialogueScene01();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        Debug.Log("On Scene: " + scene.name);


    }

    void OnDialogueScene() {
        speechBubble.SetActive(true);
        // Text textBox = textField.GetComponent<Text>();
        Text textBox = GameObject.FindWithTag("SpeechText").GetComponent<Text>();
        textBox.text = dialogueScene01.hello();
    }
}
