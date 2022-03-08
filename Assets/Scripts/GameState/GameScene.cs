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
    public GameScenes currentScene = GameScenes.Scene_01;
    public GameStages currentStage = GameStages.Stage_01;
    public GameScenes previousScene = GameScenes.Scene_01;
    public GameStages previousStage = GameStages.Stage_01;

    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void ChangeScene(GameScenes scene, GameStages stage = GameStages.Stage_01) 
    {
        SavingWrapperControl wrapper = FindObjectOfType<SavingWrapperControl>(); //<- Still working on this

        switch (scene) {
            case GameScenes.Scene_Dialogue:
                wrapper.Save();
                SceneManager.LoadSceneAsync((int)SceneName.Scene_Dialogue);
                wrapper.Load();
                break;
            
            case GameScenes.Scene_Credits:
                wrapper.Save();
                SceneManager.LoadSceneAsync("Start");
                wrapper.Load();
                break;

            // Game Scenes

            case GameScenes.Scene_Main:
                wrapper.Save();
                SceneManager.LoadSceneAsync((int)SceneName.Scene_Main);
                wrapper.Load();
                break;

        }
    }

    public void PreviousScene() {
        SceneManager.LoadSceneAsync((int)currentScene);
    }
}
