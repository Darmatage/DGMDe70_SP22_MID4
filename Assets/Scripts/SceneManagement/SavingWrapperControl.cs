using UnityEngine;
using Game.Saving;
using System.Collections;
using Game.SceneManagement;
using UnityEngine.SceneManagement;

public class SavingWrapperControl : MonoBehaviour
{
    public void NewGame(int buildIndex, float fadeTime)
    {
        StartCoroutine(LoadFirstScene(buildIndex, fadeTime));
    }

    public void ExitGame(int buildIndex, float fadeTime)
    {
        StartCoroutine(LoadMainMenuScene(buildIndex, fadeTime));
    }

    public void Save()
    {
        GetComponent<SavingSystem>().Save();
    }

    public void Load()
    {
        GetComponent<SavingSystem>().Load();
    }

    private IEnumerator LoadFirstScene(int buildIndex, float fadeTime) 
    {
        CanvasFader fader = FindObjectOfType<CanvasFader>();
        yield return fader.FadeOut(fadeTime);
        yield return SceneManager.LoadSceneAsync(buildIndex);
        EventHandler.CallLoadFirstSceneEvent();
        yield return fader.FadeIn(fadeTime);
        Save();
    }

    private IEnumerator LoadMainMenuScene(int buildIndex, float fadeTime) 
    {
        CanvasFader fader = FindObjectOfType<CanvasFader>();
        yield return fader.FadeOut(fadeTime);
        yield return SceneManager.LoadSceneAsync(buildIndex);
        yield return fader.FadeIn(fadeTime);
    }

}
