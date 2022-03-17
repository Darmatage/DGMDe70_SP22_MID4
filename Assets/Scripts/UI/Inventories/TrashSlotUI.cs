using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Game.Inventories;
using Game.Core.UI.Dragging;

namespace Game.UI.Inventories
{
    public class TrashSlotUI : MonoBehaviour, IItemHolder, IDragContainer<SO_InventoryItem>
    {
        [SerializeField] AudioSource trashSFX;
        public int MaxAcceptable(SO_InventoryItem item)
        {
            return 1;
        }

        public void AddItems(SO_InventoryItem item, int number)
        {
            trashSFX.Play();
            Debug.Log("Item has been trashed!");
        }

        public bool IsDroppable()
        {
            return false;
        }

        public bool IsSwappable()
        {
            return false;
        }

        public SO_InventoryItem GetItem()
        {
            return null;
        }

        public int GetNumber()
        {
            return 0;
        }

        public void RemoveItems(int number)
        {
            return;
        }
    }
}