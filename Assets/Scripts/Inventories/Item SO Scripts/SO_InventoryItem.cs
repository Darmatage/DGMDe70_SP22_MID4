using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Inventories
{
    /// <summary>
    /// A ScriptableObject that represents any item that can be put in an inventory.
    /// </summary>
    public abstract class SO_InventoryItem : ScriptableObject, ISerializationCallbackReceiver
    {

        [Tooltip("Auto-generated UUID for saving/loading. Clear this field if you want to generate a new one.")]
        [SerializeField] string itemID = null;
        [Tooltip("Item name to be displayed in UI.")]
        [SerializeField] string displayName = null;
        [Tooltip("Item description to be displayed in UI.")]
        [SerializeField][TextArea] string description = null;
        [Tooltip("The UI icon to represent this item in the inventory.")]
        [SerializeField] Sprite icon = null;
        [Tooltip("The prefab that should be spawned when this item is dropped.")]
        [SerializeField] Pickup pickupPrefab = null;
        static Dictionary<string, SO_InventoryItem> itemLookupCache;
        public static SO_InventoryItem GetFromID(string itemID)
        {
            if (itemLookupCache == null)
            {
                itemLookupCache = new Dictionary<string, SO_InventoryItem>();
                var itemList = Resources.LoadAll<SO_InventoryItem>("");
                foreach (var item in itemList)
                {
                    if (itemLookupCache.ContainsKey(item.itemID))
                    {
                        Debug.LogError(string.Format("Looks like there's a duplicate Invetory ID for objects: {0} and {1}", itemLookupCache[item.itemID], item));
                        continue;
                    }

                    itemLookupCache[item.itemID] = item;
                }
            }

            if (itemID == null || !itemLookupCache.ContainsKey(itemID)) return null;
            return itemLookupCache[itemID];
        }
        
        public Pickup SpawnPickup(Vector3 position, int number)
        {
            var pickup = Instantiate(this.pickupPrefab);
            pickup.transform.position = position;
            pickup.Setup(this, number);
            return pickup;
        }

        public Sprite GetIcon()
        {
            return icon;
        }

        public string GetItemID()
        {
            return itemID;
        }

        public virtual bool IsStackable()
        {
            return false;
        }
        
        public string GetDisplayName()
        {
            return displayName;
        }

        public string GetDescription()
        {
            return description;
        }

        
        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            // Generate and save a new UUID if this is blank.
            if (string.IsNullOrWhiteSpace(itemID))
            {
                itemID = System.Guid.NewGuid().ToString();
            }
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            // Require by the ISerializationCallbackReceiver but we don't need
            // to do anything with it.
        }
    }
}
