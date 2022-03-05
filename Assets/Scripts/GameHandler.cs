using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour {
    //private PlayerManager player;

    // UI Elements

    // void Awake() {
    //     if (GameObject.FindWithTag("Player") != null) {
    //         player = GameObject.FindWithTag("Player").GetComponent<PlayerManager>();
    //     }
    // }

    //  public bool isMonster() {
    //     if (player != null) {
    //         return player.hasIsMonster();
    //     }
    //     return false;
    // }

    public void StartGame() {
        Debug.Log("Start Game");
        SceneManager.LoadScene("Scene_Main");
    }

    public void OpenCredits() {
        SceneManager.LoadScene("Scene_Credits");
    }

    public void QuitGame(){
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    public void ChooseHero() {
        SceneManager.LoadScene("Scene_ChooseHero");
    }
    public void ChooseCurse() {
        Debug.Log("Clicked Curse");
        SceneManager.LoadScene("Scene_ChooseCurse");
    }
}
