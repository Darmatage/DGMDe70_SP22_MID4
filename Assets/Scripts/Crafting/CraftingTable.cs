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
            EventHandler.InteractActionEvent += InteractActionActivateCraft;
        }
        private void OnDisable()
        {
            EventHandler.InteractActionEvent -= InteractActionActivateCraft;
        }
        private void Awake() 
        {
            uiCraftingContainer = GameObject.FindWithTag(Tags.UI_CraftingContainerTag);
            craftingItems = GameObject.FindWithTag(Tags.UI_CraftingRecipesTag).GetComponent<CraftingUI>();

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

        private void InteractActionActivateCraft(bool isKeyPressed)
        {
            isActive = isKeyPressed;
        }
    }
}
