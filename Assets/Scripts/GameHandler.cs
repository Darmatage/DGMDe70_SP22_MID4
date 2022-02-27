using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        bool isMonster = player.GetComponent<PlayerManager>().hasIsMonster();

        string playerState = "";
        if (isMonster) {
            playerState = "monster";
        } else {
            playerState = "human";
        }

        Debug.Log("Player is " + playerState); 
    }
}
