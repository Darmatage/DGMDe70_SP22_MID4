using System.Collections;
using System.Collections.Generic;
using Game.Enums;
using Game.SceneManagement;
using Game.Utils;
using UnityEngine;

namespace Game.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] float fadeTime = 0.5f;
        [SerializeField] float buttonLoadWaitTime = 5f;
        [SerializeField] int firstSceneBuildIndex = 1;
        [SerializeField] GameObject loadScreenCanvas = null;
        [SerializeField] GameObject startGameButton = null;
        LazyValue<SavingWrapperControl> savingWrapper;
        LazyValue<SavedFileSingleton> savedFileSingleton;

        private void Awake() {
            savedFileSingleton = new LazyValue<SavedFileSingleton>(GetSavedFileSingleton);
            savingWrapper = new LazyValue<SavingWrapperControl>(GetSavingWrapper);
        }
        private SavedFileSingleton GetSavedFileSingleton()
        {
            return FindObjectOfType<SavedFileSingleton>();
        }
        private SavingWrapperControl GetSavingWrapper()
        {
            return FindObjectOfType<SavingWrapperControl>();
        }

        public void ChooseCurse(int curseTypeEnum)
        {
            CurseTypes chosenCurse = (CurseTypes)curseTypeEnum;
            savedFileSingleton.value.SetCurseType(chosenCurse);
            Debug.Log(chosenCurse);

            StartCoroutine(FadeToLoadScreen());
            StartCoroutine(StartButtonWait());
        }

        public void StartNewGame()
        {
            Debug.Log("Starting the game!");
            savingWrapper.value.NewGame(firstSceneBuildIndex, fadeTime);
        }

        private IEnumerator FadeToLoadScreen() 
        {
            CanvasFader fader = FindObjectOfType<CanvasFader>();
            yield return fader.FadeOut(fadeTime);
            if (loadScreenCanvas != null) loadScreenCanvas.SetActive(true);
            if (startGameButton != null) startGameButton.SetActive(false);
            yield return fader.FadeIn(fadeTime);
            
        }

        private IEnumerator StartButtonWait()
        {
            yield return new WaitForSecondsRealtime(buttonLoadWaitTime);
            if (startGameButton != null) startGameButton.SetActive(true);
            Debug.Log("Show Start Button");
        }

    }
}