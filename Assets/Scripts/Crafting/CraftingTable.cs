using System.Collections;
using System.Collections.Generic;
using Game.Control;
using Game.UI.Crafting;
using UnityEngine;

namespace Game.Crafting
{
    public class CraftingTable : MonoBehaviour, IRaycastable
    {
        [SerializeField] SO_CraftingRecipe craftingRecipe = null;
        [SerializeField] CraftingUI craftingItems = null;
        [SerializeField] GameObject uiCraftingContainer = null;
        private bool isActive = false;
        private bool isGamePaused = false;
        private void OnEnable()
        {
            EventHandler.InteractActionEvent += InteractActionActivate;
        }
        private void OnDisable()
        {
            EventHandler.InteractActionEvent -= InteractActionActivate;
        }
        private void Start()
        {
            uiCraftingContainer.SetActive(false);
        }
        public bool HandleRaycast(PlayerInputControl callingController)
        {
            if(isActive)
            {
                Debug.Log("This is a crafting table");
                craftingItems.SetupRecipes(craftingRecipe);
                MenuToggle();
                
            }

            EventHandler.CallInteractActionEvent(false);
            return true;
        }

        private void MenuToggle()
        {
            uiCraftingContainer.SetActive(!uiCraftingContainer.activeSelf);
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

        private void InteractActionActivate(bool isKeyPressed)
        {
            isActive = isKeyPressed;
        }
    }
}
