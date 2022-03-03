using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.UI
{
    public class ShowHideUI : MonoBehaviour
    {
        [SerializeField] GameObject uiInventroyContainer = null;
        [SerializeField] GameObject uiCraftingContainer = null;
        private bool isGamePaused = false;
        private void Start()
        {
            uiInventroyContainer.SetActive(false);
            uiCraftingContainer.SetActive(false);
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

        public void Inventory(InputAction.CallbackContext value)
        {
            MenuToggle();
        }

        public void CloseUI()
        {
            MenuToggle();
        }

        private void MenuToggle()
        {
            uiInventroyContainer.SetActive(!uiInventroyContainer.activeInHierarchy);
            if(isGamePaused)
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