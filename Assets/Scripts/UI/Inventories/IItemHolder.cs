using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Inventories;

namespace Game.UI.Inventories
{
    /// <summary>
    /// Allows the `ItemTooltipSpawner` to display the right information.
    /// </summary>
    public interface IItemHolder
    {
        SO_InventoryItem GetItem();
    }
}