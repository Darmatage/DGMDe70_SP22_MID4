using System.Collections;
using System.Collections.Generic;
using Game.Enums;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.UI
{
    public class ShowHideUI : MonoBehaviour
    {
        [SerializeField] GameObject uiInventroyContainer = null;
        [SerializeField] GameObject uiCraftingContainer = null;
        [SerializeField] GameObject uiDialogueContainer = null;
        [SerializeField] GameObject uiPauseContainer = null;
        private bool isGamePaused = false;

        private void OnEnable()
        {
            EventHandler.InventoryActionEvent += InventoryToggle;
            EventHandler.EscapeActionEvent += EscapeToggle;
            EventHandler.CraftingActionEvent += CraftingToggle;
            EventHandler.DialogueActionEvent += DialogueToggle;
            EventHandler.CloseAllUIActionEvent += CloseAllUI;
        }

        private void OnDisable()
        {
            EventHandler.InventoryActionEvent -= InventoryToggle;
            EventHandler.EscapeActionEvent -= EscapeToggle;
            EventHandler.CraftingActionEvent -= CraftingToggle;
            EventHandler.DialogueActionEvent -= DialogueToggle;
            EventHandler.CloseAllUIActionEvent -= CloseAllUI;
        }
        private void Start()
        {
            uiInventroyContainer.SetActive(false);
            uiCraftingContainer.SetActive(false);
            uiPauseContainer.SetActive(false);
            uiDialogueContainer.SetActive(false);
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

        public void ClosePauseUI()
        {
            MenuToggle(uiPauseContainer);
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
            MenuToggle(uiDialogueContainer);
            Debug.Log("Dialogue toggle");
        }

        private void EscapeToggle()
        {
            if(isGamePaused)
            {
                uiInventroyContainer.SetActive(false);
                uiCraftingContainer.SetActive(false);
                uiPauseContainer.SetActive(false);
                uiDialogueContainer.SetActive(false);
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
            uiDialogueContainer.SetActive(false);
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
            else if(isGamePaused && uiDialogueContainer.activeSelf)
            {
                uiDialogueContainer.SetActive(false);
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

    }
}