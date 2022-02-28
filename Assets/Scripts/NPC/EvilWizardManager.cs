using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Story;

public class EvilWizardManager : MonoBehaviour
{    
    public GameObject speechBubble;
    public GameObject textField;
    public DialogueScene02 dialogueScene02 = new DialogueScene02();

    private bool canInteract;

    void OnCollisionExit2D(Collision2D player) {
        if (player.gameObject.tag == "Player") {
            speechBubble.SetActive(false);
            canInteract = false;
        }
    }

    void OnCollisionStay2D(Collision2D player) {
        if (!canInteract && player.gameObject.tag == "Player") {
            canInteract = true;
        }
    }

    void Update() {
        if (canInteract) {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) {
                speechBubble.SetActive(true);
                Text textBox = textField.GetComponent<Text>();
                textBox.text = dialogueScene02.hello();
            }
        }
    }

}
