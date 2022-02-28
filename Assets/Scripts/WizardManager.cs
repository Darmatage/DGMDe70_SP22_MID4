using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardManager : MonoBehaviour
{
    public GameObject interactNotification;
    public GameObject speechBubble;

    void OnCollisionEnter2D(Collision2D player) {
        if (player.gameObject.tag == "Player") {
            interactNotification.SetActive(true);
        }
    }

    void OnCollisionExit2D(Collision2D player) {
        if (player.gameObject.tag == "Player") {
            interactNotification.SetActive(false);
        }
    }

    void OnCollisionStay2D(Collision2D player) {
        if (player.gameObject.tag == "Player") {
            if (Input.GetKeyDown("e")) {
                speechBubble.SetActive(true);
            }
        }
    }
}
