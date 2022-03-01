using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScene : MonoBehaviour
{
    private static GameScene _instance;
    public static GameScene Instance {
        get { return _instance; }
    }
    public GameScenes currentScene = GameScenes.S01;

    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeScene(GameScenes scene) {

        switch (scene) {
            case GameScenes.Start:
                SceneManager.LoadScene("Start");
                break;

            case GameScenes.Dialogue:
                SceneManager.LoadScene("Dialogue");
                break;

            case GameScenes.S01:
                SceneManager.LoadScene("main");
                break;
        }

    }
}

public enum GameScenes
{
    Start,
    Dialogue,
    S01,
    S02,
    S03
}
