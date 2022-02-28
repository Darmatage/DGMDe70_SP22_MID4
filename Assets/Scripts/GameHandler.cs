using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    private PlayerManager player;

    // UI Elements
    public GameObject speechBubble;

    void Awake() {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerManager>();
        speechBubble.SetActive(false);
    }

     public bool isMonster() {
        return player.hasIsMonster();
        // return player.GetComponent<PlayerManager>().hasIsMonster();
    }
}
