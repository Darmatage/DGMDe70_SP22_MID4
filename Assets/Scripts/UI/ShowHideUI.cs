using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.UI
{
    public class ShowHideUI : MonoBehaviour
    {
        [SerializeField] GameObject uiContainer = null;
        bool isGamePaused = false;
        private void Start()
        {
            uiContainer.SetActive(false);
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

        public void MenuToggle()
        {
            uiContainer.SetActive(!uiContainer.activeInHierarchy);
            if(isGamePaused)
            {
                isGamePaused = false;
            }
            else
            {
                isGamePaused = true;
            }
        }
    }
}