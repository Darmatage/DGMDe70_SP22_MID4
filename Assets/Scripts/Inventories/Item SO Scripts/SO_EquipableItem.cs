using Game.Enums;
using UnityEngine;

namespace Game.Inventories
{
    /// <summary>
    /// An abstract inventory item - Weapons and Armor are built ontop of this class. 
    /// </summary>
    public abstract class SO_EquipableItem : SO_InventoryItem
    {
        [Tooltip("Where are we allowed to put this item.")]
        [SerializeField] EquipLocation allowedEquipLocation = EquipLocation.None;
        public virtual EquipLocation GetAllowedEquipLocation()
        {
            return allowedEquipLocation;
        }
        public abstract EquipmentMaterial GetEquipmentMaterial();
    }
}