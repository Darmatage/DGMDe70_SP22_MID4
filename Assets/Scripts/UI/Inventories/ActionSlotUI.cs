using System.Collections;
using System.Collections.Generic;
using Game.Core.UI.Dragging;
using Game.Inventories;
using UnityEngine;

namespace Game.UI.Inventories
{
    public class ActionSlotUI : MonoBehaviour, IItemHolder, IDragContainer<SO_InventoryItem>
    {
        [SerializeField] InventoryItemIcon icon = null;
        [SerializeField] int index = 0;
        ActionStore store;

        private void Awake()
        {
            store = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG).GetComponent<ActionStore>();
            store.storeUpdated += UpdateIcon;
        }


        public void AddItems(SO_InventoryItem item, int number)
        {
            store.AddAction(item, index, number);
        }

        public SO_InventoryItem GetItem()
        {
            return store.GetAction(index);
        }

        public int GetNumber()
        {
            return store.GetNumber(index);
        }

        public int MaxAcceptable(SO_InventoryItem item)
        {
            return store.MaxAcceptable(item, index);
        }

        public void RemoveItems(int number)
        {
            store.RemoveItems(index, number);
        }

        public bool IsDroppable()
        {
            return store.IsDroppable(index);
        }

        void UpdateIcon()
        {
            icon.SetItem(GetItem(), GetNumber());
        }
    }
}
