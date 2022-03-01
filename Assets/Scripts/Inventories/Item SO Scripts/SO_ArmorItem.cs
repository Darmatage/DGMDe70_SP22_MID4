using System.Collections;
using System.Collections.Generic;
using Game.Enums;
using Game.PlayerClass;
using UnityEngine;

namespace Game.Inventories
{
    [CreateAssetMenu(fileName = "Armor", menuName = ("Game/Inventory/New Armor Item"))]
    public class SO_ArmorItem : SO_EquipableItem, IModifierProvider
    {
        [Header("Armor Stat Modifiers")]
        [Tooltip("Modify base stats.")]
        [SerializeField] Modifier[] additiveModifiers;
        [Tooltip("Modify base stats by percentage.")]
        [SerializeField] Modifier[] percentageModifiers;

        [System.Serializable]
        struct Modifier
        {
            public PlayerStats stat;
            public float value;
        }   

        public IEnumerable<float> GetAdditiveModifiers(PlayerStats stat)
        {
            foreach (var modifier in additiveModifiers)
            {
                if (modifier.stat == stat)
                {
                    yield return modifier.value;
                }
            }
        }

        public IEnumerable<float> GetPercentageModifiers(PlayerStats stat)
        {
            foreach (var modifier in percentageModifiers)
            {
                if (modifier.stat == stat)
                {
                    yield return modifier.value;
                }
            }
        }
    }
}
