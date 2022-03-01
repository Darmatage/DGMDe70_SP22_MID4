using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour {
    public PlayerManager player;

    // UI Elements

    void Awake() {
        if (GameObject.FindWithTag("Player") != null) {
            player = GameObject.FindWithTag("Player").GetComponent<PlayerManager>();
        }
    }

     public bool isMonster() {
        if (player != null) {
            return player.hasIsMonster();
        }
        return false;
    }
}
