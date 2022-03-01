using UnityEngine;
using Game.UI.Inventories;
using Game.Inventories;

namespace Game.UI.Crafting
{
    public class CraftingSlotUI : MonoBehaviour, IItemHolder
    {
        [SerializeField] InventoryItemIcon icon = null;
 
        SO_InventoryItem item;

        public void Setup(SO_InventoryItem item, int number)
        {
            this.item = item;
            icon.SetItem(item, number);
        }

        public SO_InventoryItem GetItem()
        {
            return item;
        }
    }
}