using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Game.Inventories;
using Game.Core.UI.Dragging;

namespace Game.UI.Inventories
{
    public class InventorySlotUI : MonoBehaviour, IItemHolder, IDragContainer<SO_InventoryItem>
    {
        // CONFIG DATA
        [SerializeField] InventoryItemIcon icon = null;

        // STATE
        int index;
        SO_InventoryItem item;
        Inventory inventory;

        // PUBLIC

        public void Setup(Inventory inventory, int index)
        {
            this.inventory = inventory;
            this.index = index;
            icon.SetItem(inventory.GetItemInSlot(index), inventory.GetNumberInSlot(index));
        }

        public int MaxAcceptable(SO_InventoryItem item)
        {
            if (inventory.HasSpaceFor(item))
            {
                return int.MaxValue;
            }
            return 0;
        }

        public void AddItems(SO_InventoryItem item, int number)
        {
            inventory.AddItemToSlot(index, item, number);
        }

        public bool IsDroppable()
        {
            return inventory.IsItemDroppable(index);
        }

        public SO_InventoryItem GetItem()
        {
            return inventory.GetItemInSlot(index);
        }

        public int GetNumber()
        {
            return inventory.GetNumberInSlot(index);
        }

        public void RemoveItems(int number)
        {
            inventory.RemoveFromSlot(index, number);
        }
    }
}