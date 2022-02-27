using System;
using UnityEngine;

namespace Game.Inventories
{
    [CreateAssetMenu(fileName = "Collectable", menuName = "Game/Inventory/New Collectable Item")]
    public class SO_CollectableItem : SO_InventoryItem
    {
        [Tooltip("If true, multiple items of this type can be stacked in the same inventory slot.")]
        [SerializeField] bool stackable = false;
        public override bool IsStackable()
        {
            return stackable;
        }
    }
}