using UnityEngine;
using UnityEngine.UI;
using Game.Crafting;
using Game.Inventories;

namespace Game.UI.Crafting
{
    public class CraftingUI : MonoBehaviour
    {
        [SerializeField] GameObject recipePrefab = null;
        [SerializeField] CraftingSlotUI craftingSlotPrefab = null;
        [SerializeField] GameObject recipeArrow = null;
        [SerializeField] Button craftButton = null;

        SO_CraftingRecipe craftingRecipe;
        Inventory inventory;

        private void Awake()
        {
            inventory = Inventory.GetPlayerInventory();
        }

        public void SetupRecipes(SO_CraftingRecipe recipe)
        {
            craftingRecipe = recipe;

            Redraw();
        }

        private void Redraw()
        {
            DestroyChild(transform);
        
            for (int i = 0; i < craftingRecipe.GetCraftingRecipes().Length; i++)
            {

                var recipeHolder = Instantiate(recipePrefab, transform);
                DestroyChild(recipeHolder.transform);
                var recipe = craftingRecipe.GetCraftingRecipes()[i];
                CreateRecipeIngredients(recipe, recipeHolder.transform);
                CreateRecipeObjects(craftingRecipe.GetCraftingRecipes()[i].craftedItem, recipeHolder.transform, recipe);
            }
        }

        private void CreateRecipeIngredients(SO_CraftingRecipe.Recipes recipe, Transform recipeHolder)
        {
            int ingredientsSize = recipe.ingredients.Length;
            for (int ingredient = 0; ingredient < ingredientsSize; ingredient++)
            {
                var ingredientItem = Instantiate(craftingSlotPrefab, recipeHolder);
                ingredientItem.Setup(recipe.ingredients[ingredient].item, recipe.ingredients[ingredient].number);
            }
        }

        private void CreateRecipeObjects(SO_InventoryItem inventoryItem, Transform recipeHolder, SO_CraftingRecipe.Recipes recipe)
        {
            var arrow = Instantiate(recipeArrow, recipeHolder);
            var item = Instantiate(craftingSlotPrefab, recipeHolder);
            item.Setup(inventoryItem, 1);
            var button = Instantiate(craftButton, recipeHolder);
            var craft = button.GetComponent<Craft>();

            button.onClick.AddListener(() => craft.CraftItem(inventory, inventoryItem, recipe));
        }

        private void DestroyChild(Transform transform)
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}