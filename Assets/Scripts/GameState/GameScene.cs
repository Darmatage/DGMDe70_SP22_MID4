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

    public void AdvanceScene() {
        switch (currentScene) {
            case GameScenes.Scene_00: currentScene = GameScenes.Scene_01; break;
            case GameScenes.Scene_01: currentScene = GameScenes.Scene_02; break;
            case GameScenes.Scene_02: currentScene = GameScenes.Scene_03; break;
            case GameScenes.Scene_03: currentScene = GameScenes.Scene_04; break;
            case GameScenes.Scene_04: currentScene = GameScenes.Scene_05; break;
            case GameScenes.Scene_05: currentScene = GameScenes.Scene_06; break;
            case GameScenes.Scene_06: currentScene = GameScenes.Scene_07; break;
            case GameScenes.Scene_07: currentScene = GameScenes.Scene_08; break;
            case GameScenes.Scene_08: currentScene = GameScenes.Scene_09; break;
        }

        currentStage = GameStages.Stage_01;
    }

    public void AdvanceStage() {
        switch (currentStage) {
            case GameStages.Stage_01: currentStage = GameStages.Stage_02; break;
            case GameStages.Stage_02: currentStage = GameStages.Stage_03; break;
            case GameStages.Stage_03: currentStage = GameStages.Stage_04; break;
            case GameStages.Stage_04: currentStage = GameStages.Stage_05; break;
            case GameStages.Stage_05: currentStage = GameStages.Stage_06; break;
            case GameStages.Stage_06: currentStage = GameStages.Stage_07; break;
            case GameStages.Stage_07: currentStage = GameStages.Stage_08; break;
            case GameStages.Stage_08: currentStage = GameStages.Stage_09; break;
            case GameStages.Stage_09: currentStage = GameStages.Stage_10; break;
            case GameStages.Stage_10: currentStage = GameStages.Stage_11; break;
        }
    }

    public void ResetStory() {
        currentScene = GameScenes.Scene_01;
        currentStage = GameStages.Stage_01;
    }
}
