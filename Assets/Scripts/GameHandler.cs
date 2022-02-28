using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    private GameObject player;

    // UI Elements
    public GameObject interactNotification;
    public GameObject speechBubble;

    void Awake() {
        player = GameObject.FindWithTag("Player");
        interactNotification.SetActive(false);
        speechBubble.SetActive(false);
    }

     public bool isMonster() {
        return player.GetComponent<PlayerManager>().hasIsMonster();
    }
}
