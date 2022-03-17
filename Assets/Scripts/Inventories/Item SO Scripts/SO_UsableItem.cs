using System;
using Game.Combat;
using UnityEngine;

namespace Game.Inventories
{
    [CreateAssetMenu(fileName = "Health Item", menuName = "Game/Inventory/New Health Item")]
    public class SO_UsableItem : SO_ActionItem
    {
        [SerializeField] float cooldownTime = 0f;
        [SerializeField] float healthChange = 0f;
        public override bool Use(GameObject user)
        {
            CooldownStore cooldownStore = user.GetComponent<CooldownStore>();
            if (cooldownStore.GetTimeRemaining(this) > 0)
            {
                return false;
            }

            UseHealing(user);
            return true;
        }

        private void UseHealing(GameObject user)
        {
            CooldownStore cooldownStore = user.GetComponent<CooldownStore>();
            cooldownStore.StartCooldown(this, cooldownTime);

            var health = user.GetComponent<PlayerHealth>();
            if (health)
            {
                health.Heal(healthChange);
            }
        }


    }
}