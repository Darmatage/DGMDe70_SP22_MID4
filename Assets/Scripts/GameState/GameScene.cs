using System.Collections;
using System.Collections.Generic;
using Game.Enums;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScene : MonoBehaviour
{
    private static GameScene _instance;
    public static GameScene Instance {
        get { return _instance; }
    }
    public GameScenes currentScene = GameScenes.S01;
    public GameStages currentStage = GameStages.S01;

    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void ChangeScene(GameScenes scene, GameStages stage = GameStages.S01) 
    {
        SavingWrapperControl wrapper = FindObjectOfType<SavingWrapperControl>(); //<- Still working on this

        currentScene = scene;
        currentStage = stage;

        switch (scene) {
            case GameScenes.Dialogue:
                wrapper.Save();
                SceneManager.LoadSceneAsync((int)SceneName.Scene_Dialogue);
                wrapper.Load();
                break;
            
            case GameScenes.Start:
                wrapper.Save();
                SceneManager.LoadSceneAsync("Start");
                wrapper.Load();
                break;

            // Game Scenes

            case GameScenes.S01:
                wrapper.Save();
                SceneManager.LoadSceneAsync((int)SceneName.Scene_Main);
                wrapper.Load();
                break;
        }

    }
}


