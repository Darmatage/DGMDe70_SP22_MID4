using System.Collections.Generic;
using Game.Enums;
using Game.PlayerClass;

namespace Game.Inventories
{
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
                }
            }
        }

    }
}
