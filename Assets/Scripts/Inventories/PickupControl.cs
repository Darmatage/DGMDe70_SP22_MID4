using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Inventories
{
    [RequireComponent(typeof(Pickup))]
    public class PickupControl : MonoBehaviour
    {
        Pickup pickup;

        private void Awake()
        {
            pickup = GetComponent<Pickup>();
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (pickup.InventoryHasSpace() && pickup.IsPlayerPickup(other.gameObject))
            {
                pickup.PickupItem();
            }
        }
    }
}