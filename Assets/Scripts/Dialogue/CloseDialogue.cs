using System.Collections;
using System.Collections.Generic;
using Game.Enums;
using UnityEngine;

namespace Game.Story
{
    public class CloseDialogue : MonoBehaviour
    {
        public GameObject dialogueBox;

        // Update is called once per frame
        void Update() {
            if (Input.GetMouseButtonDown(0)) {   
                GameScene.Instance.ChangeScene(GameScenes.Scene_Main);
                // GameScene.Instance.PreviousScene();
            }
        }
    }
    
}

