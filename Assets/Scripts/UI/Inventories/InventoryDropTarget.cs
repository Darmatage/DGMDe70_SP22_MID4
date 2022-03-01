using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.Core.UI.Dragging;
using Game.Inventories;

namespace Game.UI.Inventories
{
    /// <summary>
    /// Handles spawning pickups when item dropped into the world.
    /// 
    /// Must be placed on the root canvas where items can be dragged. Will be
    /// called if dropped over empty space. 
    /// </summary>
    public class InventoryDropTarget : MonoBehaviour, IDragDestination<SO_InventoryItem>
    {
        public void AddItems(SO_InventoryItem item, int number)
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<ItemDropper>().DropItem(item, number);
        }

        public int MaxAcceptable(SO_InventoryItem item)
        {
            return int.MaxValue;
        }
    }
}