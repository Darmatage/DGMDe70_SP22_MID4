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
    public GameScenes currentScene = GameScenes.Scene_Main;
    public GameStages currentStage = GameStages.Stage_01;
    public GameScenes previousScene = GameScenes.Scene_Main;
    public GameStages previousStage = GameStages.Stage_01;

    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);

        /* Will remove once we resolve the (int) casting (cabrams)
        Debug.Log("InitialScene");
        Debug.Log("Previous Scene: " + (int)previousScene);
        Debug.Log("Current Scene: " + (int)currentScene);*/
    }

    public void ChangeScene(GameScenes scene, GameStages stage = GameStages.Stage_01) 
    {
       // SavingWrapperControl wrapper = FindObjectOfType<SavingWrapperControl>(); //<- Still working on this

        previousScene = currentScene;
        previousStage = currentStage;
        currentScene = scene;
        currentStage = stage;

        switch (scene) {
            case GameScenes.Scene_Dialogue:
                //wrapper.Save();
                SceneManager.LoadSceneAsync((int)GameScenes.Scene_Dialogue);
                //wrapper.Load();
                break;

            // Game Scenes

            case GameScenes.Scene_Main:
                //wrapper.Save();
                SceneManager.LoadSceneAsync((int)GameScenes.Scene_Main);
                //wrapper.Load();
                break;

        }

        /* Will remove once we resolve the (int) casting (cabrams)
        Debug.Log("ChangeScene");
        Debug.Log("Previous Scene: " + (int)previousScene);
        Debug.Log("Current Scene: " + (int)currentScene);
        */

    }

    public void PreviousScene() {
        GameScenes nextScreen = previousScene;
        GameStages nextStage = previousStage;
        previousScene = currentScene;
        previousStage = currentStage;
        currentScene = nextScreen;
        currentStage = nextStage;

        /* Will remove once we resolve the (int) casting (cabrams)
        Debug.Log("PreviousScene");
        Debug.Log("Previous Scene: " + (int)previousScene);
        Debug.Log("Current Scene: " + (int)currentScene);
        */

        SceneManager.LoadSceneAsync((int)currentScene);
    }
}
