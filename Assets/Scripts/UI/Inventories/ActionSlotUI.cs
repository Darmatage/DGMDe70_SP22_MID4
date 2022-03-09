using System.Collections;
using System.Collections.Generic;
using Game.Core.UI.Dragging;
using Game.Inventories;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Inventories
{
    public class ActionSlotUI : MonoBehaviour, IItemHolder, IDragContainer<SO_InventoryItem>
    {
        [SerializeField] InventoryItemIcon icon = null;
        [SerializeField] int index = 0;
        [SerializeField] Image cooldownOverlay = null;

        ActionStore store;
        CooldownStore cooldownStore;

        private void Awake()
        {
            GameObject player = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG);
            store = player.GetComponent<ActionStore>();
            cooldownStore = player.GetComponent<CooldownStore>();
            store.storeUpdated += UpdateIcon;
        }

        private void Update()
        {
            cooldownOverlay.fillAmount = cooldownStore.GetFractionRemaining(GetItem());
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
