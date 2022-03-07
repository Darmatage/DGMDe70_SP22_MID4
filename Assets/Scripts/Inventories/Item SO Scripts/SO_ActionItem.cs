using System;
using Game.Combat;
using UnityEngine;

namespace Game.Inventories
{
    [CreateAssetMenu(fileName = "Health Potion", menuName = "Game/Inventory/New Health Potion")]
    public class SO_ActionItem : SO_InventoryItem
    {
        [Tooltip("Does one of these item get consumed every time it's used.")]
        [SerializeField] bool consumable = false;
        [SerializeField] float healthChange = 0f;
        public bool Use(GameObject user)
        {
            //Debug.Log("Using action: " + this);
            UseHealing(user);
            return false;
        }

        public bool isConsumable()
        {
            return consumable;
        }

        private void UseHealing(GameObject user)
        {
            var health = user.GetComponent<PlayerHealth>();
            if (health)
            {
                health.Heal(healthChange);
            }
        }


    }
}