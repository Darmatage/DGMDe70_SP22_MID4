using System.Collections;
using System.Collections.Generic;
using Game.Enums;
using Game.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.UI
{
    public class ShowHideUI : MonoBehaviour
    {
        [SerializeField] GameObject uiInventroyContainer = null;
        [SerializeField] GameObject uiCraftingContainer = null;
        [SerializeField] GameObject uiPauseContainer = null;

        LazyValue<GameObject> uiDialogueContainer;
        LazyValue<SavingWrapperControl> savingWrapper;
        private bool isGamePaused = false;

        private void Awake() 
        {
            uiDialogueContainer = new LazyValue<GameObject>(GetDialogueContainer);
            savingWrapper = new LazyValue<SavingWrapperControl>(GetSavingWrapper);
        }

        private SavingWrapperControl GetSavingWrapper()
        {
            return FindObjectOfType<SavingWrapperControl>();
        }
        private GameObject GetDialogueContainer()
        {
            return GameObject.FindWithTag(Tags.UI_DIALOGUE_CONTAINER_TAG);
        }

        private void OnEnable()
        {
            EventHandler.InventoryActionEvent += InventoryToggle;
            EventHandler.EscapeActionEvent += EscapeToggle;
            EventHandler.CraftingActionEvent += CraftingToggle;
            EventHandler.DialogueActionEvent += DialogueToggle;
            EventHandler.CloseAllUIActionEvent += CloseAllUI;
            EventHandler.ActiveGameUI += GamePausedToggle;
        }

        private void OnDisable()
        {
            EventHandler.InventoryActionEvent -= InventoryToggle;
            EventHandler.EscapeActionEvent -= EscapeToggle;
            EventHandler.CraftingActionEvent -= CraftingToggle;
            EventHandler.DialogueActionEvent -= DialogueToggle;
            EventHandler.CloseAllUIActionEvent -= CloseAllUI;
            EventHandler.ActiveGameUI -= GamePausedToggle;
        }
        private void Start()
        {
            uiDialogueContainer.ForceInit();
            uiInventroyContainer.SetActive(false);
            uiCraftingContainer.SetActive(false);
            uiPauseContainer.SetActive(false);
            uiDialogueContainer.value.SetActive(false);
        }
        private void Update()
        {
            if(isGamePaused)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }

        public void OpenInventoryHUDButton()
        {
            MenuToggle(uiInventroyContainer);
        }

        public void ClosePauseUI()
        {
            MenuToggle(uiPauseContainer);
        }

        public void ExitGameUI()
        {
            isGamePaused = false;
            savingWrapper.value.ExitGame(0, 0.5f);
            Debug.Log("Exit Game");
        }

        private void InventoryToggle()
        {
            MenuToggle(uiInventroyContainer);
            Debug.Log("Inventory toggle");
        }


        private void CraftingToggle()
        {
            MenuToggle(uiCraftingContainer);
            Debug.Log("Crafting toggle");
        }

        private void DialogueToggle(CutSceneDestinationIdentifier cutSceneDestinationIdentifier) //<- Variables only included here because the event passes it through
        {
            MenuToggle(uiDialogueContainer.value);
            Debug.Log("Dialogue toggle");
        }

        private void EscapeToggle()
        {
            if(isGamePaused)
            {
                uiInventroyContainer.SetActive(false);
                uiCraftingContainer.SetActive(false);
                uiPauseContainer.SetActive(false);
                uiDialogueContainer.value.SetActive(false);
                isGamePaused = false;
            }
            else 
            {
                Debug.Log("Open Pause UI");
                uiPauseContainer.SetActive(true);
                isGamePaused = true;
            }
            EventHandler.CallActiveGameUI(isGamePaused);
            Debug.Log("Escape toggle");
        }

        private void CloseAllUI()
        {
            uiInventroyContainer.SetActive(false);
            uiCraftingContainer.SetActive(false);
            uiPauseContainer.SetActive(false);
            uiDialogueContainer.value.SetActive(false);
            isGamePaused = false;
            EventHandler.CallActiveGameUI(isGamePaused);
        }

        private void MenuToggle(GameObject uiContainer)
        {
            uiContainer.SetActive(!uiContainer.activeInHierarchy);
            if(isGamePaused && uiCraftingContainer.activeSelf)
            {
                uiCraftingContainer.SetActive(false);
                isGamePaused = true;
            }
            else if(isGamePaused && uiDialogueContainer.value.activeSelf)
            {
                uiDialogueContainer.value.SetActive(false);
                isGamePaused = true;
            }
            else if (isGamePaused && !uiCraftingContainer.activeSelf)
            {
                isGamePaused = false;
            }
            else
            {
                isGamePaused = true;
            }
            EventHandler.CallActiveGameUI(isGamePaused);
        }

        private void GamePausedToggle(bool toggleTo)
        {
            isGamePaused = toggleTo;
            Debug.Log("IsGamePause: " + isGamePaused);
        }

    }
}