using System;
using UnityEngine;

namespace Game.Inventories
{
    /// <summary>
    /// A ScriptableObject that is used for items that are collectable and stackable.
    /// but have no uses other then in crafting.
    /// </summary>

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