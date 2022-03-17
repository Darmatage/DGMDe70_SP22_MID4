using System.Collections;
using System.Collections.Generic;
using Game.Enums;
using Game.ClassTypes.Player;
using UnityEngine;

namespace Game.Inventories
{
    /// <summary>
    /// A ScriptableObject that is used for player armor.
    /// </summary>

    [CreateAssetMenu(fileName = "Armor", menuName = ("Game/Inventory/New Armor Item"))]
    public class SO_ArmorItem : SO_EquipableItem, IModifierProvider
    {
        [Tooltip("What is the armor made of?")]
        [SerializeField] EquipmentMaterial armorMaterial = EquipmentMaterial.None;

        [Header("Armor Stat Modifiers")]
        [Tooltip("Modify base stats.")]
        [SerializeField] Modifier[] additiveModifiers;
        [Tooltip("Modify base stats by percentage.")]
        [SerializeField] Modifier[] percentageModifiers;

        public override EquipmentMaterial GetEquipmentMaterial()
        {
            return armorMaterial;
        }

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
