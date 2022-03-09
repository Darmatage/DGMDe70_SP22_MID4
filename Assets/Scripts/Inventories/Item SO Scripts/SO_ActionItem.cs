using System;
using UnityEngine;

namespace Game.Inventories
{
    public abstract class SO_ActionItem : SO_InventoryItem
    {
        [Tooltip("Does one of these item get consumed every time it's used.")]
        [SerializeField] bool consumable = false;

        [Tooltip("If true, multiple items of this type can be stacked in the same inventory slot.")]
        [SerializeField] bool stackable = false;
        public virtual bool Use(GameObject user)
        {
            return false;
        }
        public bool isConsumable()
        {
            return consumable;
        }
        public override bool IsStackable()
        {
            return stackable;
        }
    }
}