using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Inventories
{
    public class CooldownStore : MonoBehaviour
    {
        Dictionary<SO_InventoryItem, float> cooldownTimers = new Dictionary<SO_InventoryItem, float>();
        Dictionary<SO_InventoryItem, float> initialCooldownTimes = new Dictionary<SO_InventoryItem, float>();

        void Update() 
        {
            var keys = new List<SO_InventoryItem>(cooldownTimers.Keys);
            foreach (SO_InventoryItem item in keys)
            {
                cooldownTimers[item] -= Time.deltaTime;
                if (cooldownTimers[item] < 0)
                {
                    cooldownTimers.Remove(item);
                    initialCooldownTimes.Remove(item);
                }
            }
        }

        public void StartCooldown(SO_InventoryItem item, float cooldownTime)
        {
            cooldownTimers[item] = cooldownTime;
            initialCooldownTimes[item] = cooldownTime;
        }

        public float GetTimeRemaining(SO_InventoryItem item)
        {
            if (!cooldownTimers.ContainsKey(item))
            {
                return 0;
            }

            return cooldownTimers[item];
        }

        public float GetFractionRemaining(SO_InventoryItem item)
        {
            if (item == null)
            {
                return 0;
            }

            if (!cooldownTimers.ContainsKey(item))
            {
                return 0;
            }

            return cooldownTimers[item] / initialCooldownTimes[item];
        }
    }
}