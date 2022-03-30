using UnityEngine;
using Game.Saving;
using System.Collections;
using Game.SceneManagement;
using UnityEngine.SceneManagement;
using Game.Enums;

public class SavingWrapperControl : MonoBehaviour
{
    // private void Update() 
    // {
    //     Debug.Log(Time.deltaTime);
    // }
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
        CanvasFader fader = GameObject.FindWithTag(Tags.CANVAS_FADER_TAG).GetComponent<CanvasFader>();
        yield return fader.FadeOut(fadeTime);
        yield return SceneManager.LoadSceneAsync(buildIndex);
        
        yield return new WaitForSeconds(0.5f);
        EventHandler.CallDialogueActionEvent(CutSceneDestinationIdentifier.Narrator);
        yield return null;
        
        EventHandler.CallLoadFirstSceneEvent();
        EventHandler.CallActiveGameUI(false);

        yield return new WaitForSeconds(1f);
        yield return fader.FadeIn(fadeTime);

        Save();
        EventHandler.CallActiveGameUI(true);
    }


    private IEnumerator LoadMainMenuScene(int buildIndex, float fadeTime) 
    {
        CanvasFader fader = GameObject.FindWithTag(Tags.CANVAS_FADER_TAG).GetComponent<CanvasFader>();
        yield return fader.FadeOut(fadeTime);
        yield return SceneManager.LoadSceneAsync(buildIndex);
        yield return fader.FadeIn(fadeTime);
    }

}
