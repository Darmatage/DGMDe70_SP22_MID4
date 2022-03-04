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
        [SerializeField] GameObject interactionIndicatorUI = null;
        private bool isKeyActive = false;
        private bool isRaycastOn = false;
        private void OnEnable()
        {
            EventHandler.InteractActionKeyEvent += InteractActionActivateCraft;
        }
        private void OnDisable()
        {
            EventHandler.InteractActionKeyEvent -= InteractActionActivateCraft;
        }
        private void Awake() 
        {
            craftingItems = GameObject.FindWithTag(Tags.UI_CRAFTING_RECIPES_TAG).GetComponent<CraftingUI>();
        }
        private void Update() 
        {
            InteractUIDisplay();
            isRaycastOn = false;
        }
        public bool HandleRaycast(PlayerInputControl callingController)
        {
            isRaycastOn = true;
            
            if(isKeyActive)
            {
                craftingItems.SetupRecipes(craftingRecipe);
                EventHandler.CallCraftingActionEvent();
            }
            
            EventHandler.CallInteractActionKeyEvent(false);
            return true;
        }

        private void InteractActionActivateCraft(bool isKeyPressed)
        {
            isKeyActive = isKeyPressed;
        }

        public void InteractUIDisplay()
        {
            if(!interactionIndicatorUI) return;
            interactionIndicatorUI.SetActive(isRaycastOn);
        }
    }
}
