using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDialogue : MonoBehaviour
{
    public GameObject dialogueBox;

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) {   
            GameScene.Instance.ChangeScene(GameScenes.S01);
        }
    }
}
