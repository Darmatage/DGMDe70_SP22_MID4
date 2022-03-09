using System.Collections.Generic;
using Game.Control;
using Game.Enums;
using Game.PlayerClass;

namespace Game.Inventories
{
    /// <summary>
    /// Placed on the player and will calculate the weapon and armor stat modifiers,
    /// and then apply them to the players base stats.
    /// </summary>
    public class EquipmentModifier : Equipment, IModifierProvider
    {
        IEnumerable<float> IModifierProvider.GetAdditiveModifiers(PlayerStats stat)
        {
            foreach (var slot in GetAllPopulatedSlots())
            {
                var item = GetItemInSlot(slot) as IModifierProvider;
                if (item == null) continue;

                foreach (float modifier in item.GetAdditiveModifiers(stat))
                {
                    yield return modifier;
                    // if (GetComponent<PlayerTransformControl>().IsMonster) yield return 0f;
                    // if (!GetComponent<PlayerTransformControl>().IsMonster) yield return modifier;
                }
            }
        }

        IEnumerable<float> IModifierProvider.GetPercentageModifiers(PlayerStats stat)
        {
            foreach (var slot in GetAllPopulatedSlots())
            {
                var item = GetItemInSlot(slot) as IModifierProvider;
                if (item == null) continue;

                foreach (float modifier in item.GetPercentageModifiers(stat))
                {
                    yield return modifier;
                    // if (GetComponent<PlayerTransformControl>().IsMonster) yield return 0f;
                    // if (!GetComponent<PlayerTransformControl>().IsMonster) yield return modifier;
                }
            }
        }

    }
}
