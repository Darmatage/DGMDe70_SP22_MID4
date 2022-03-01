using UnityEngine;
using Game.Inventories;

namespace Game.Crafting
{
    public class Craft : MonoBehaviour
    {
        public void CraftItem(Inventory inventory, SO_InventoryItem inventoryItem, SO_CraftingRecipe.Recipes recipe)
        {
            if(HasIngredients(inventory, recipe))
            {
                RemoveItems(inventory, recipe);
                inventory.AddToFirstEmptySlot(inventoryItem, 1);
            }
        }

        private void RemoveItems(Inventory inventory, SO_CraftingRecipe.Recipes recipe)
        {
            foreach (SO_CraftingRecipe.Ingredients ingredient in recipe.ingredients)
            {
                if (ingredient.item.IsStackable())
                {
                    int itemSlot = inventory.GetItemSlot(ingredient.item, ingredient.number);
                    inventory.RemoveFromSlot(itemSlot, ingredient.number);
                }
                else
                {
                    for (int i = 0; i < ingredient.number; i++)
                    {
                        int itemSlot = inventory.GetItemSlot(ingredient.item, 1);
                        inventory.RemoveFromSlot(itemSlot, 1);
                    }
                }
            }
        }

        private bool HasIngredients(Inventory inventory, SO_CraftingRecipe.Recipes recipe)
        {
            bool hasItem = false;
            foreach(SO_CraftingRecipe.Ingredients ingredient in recipe.ingredients)
            {
                if(ingredient.item.IsStackable())
                {
                    hasItem = inventory.GetItemSlot(ingredient.item, ingredient.number) >= 0;
                }
                else
                {
                    hasItem = inventory.HasItem(ingredient.item);
                }
                if(!hasItem) return false;
            }
            return true;
        }
    }
}