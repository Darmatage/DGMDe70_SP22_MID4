using UnityEngine;
using Game.Inventories;

namespace Game.Crafting
{
    /// <summary>
    /// Create item recipe list to be attached to the crafting location.</br>
    /// Use different list of different crafting options.
    /// </summary>
    [CreateAssetMenu(fileName = "NewCraftingRecipe",menuName = "Game/Crafting/Crafting Recipe")]
    public class SO_CraftingRecipe : ScriptableObject
    {
		[SerializeField] Recipes[] recipes;

		[System.Serializable]
		public class Recipes
		{
			[Tooltip("What items is to be crafted?")]
            public SO_InventoryItem craftedItem;
			public Ingredients[] ingredients;
		}
 
		[System.Serializable]
		public class Ingredients
		{
			[Tooltip("What items are needed to craft the item?")]
            public SO_InventoryItem item;
			[Tooltip("How many of this item are needed?")]
            public int number;
		}

		public Recipes[] GetCraftingRecipes()
		{
			return recipes;
		}
    }
}