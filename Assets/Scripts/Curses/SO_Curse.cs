using System.Collections;
using System.Collections.Generic;
using Game.Enums;
using Game.Inventories;
using Game.PlayerClass;
using UnityEngine;

namespace Game.Curses
{
    [CreateAssetMenu(fileName = "Curse", menuName = "Game/Player/New Curse")]
    public class SO_Curse : SO_EquipableItem, IModifierProvider
    {
        [Header("Curse Stat Modifiers")]
        [Tooltip("Modify base stats.")]
        [SerializeField] Modifier[] additiveModifiers;
        [Tooltip("Modify base stats by percentage.")]
        [SerializeField] Modifier[] percentageModifiers;
        [Tooltip("Does the Curse have it's own attack?")]
        [SerializeField] SO_WeaponItem curseWeapon = null;

        [System.Serializable]
        struct Modifier
        {
            public PlayerStats stat;
            public float value;
        }   
        public override EquipLocation GetAllowedEquipLocation()
        {
            return EquipLocation.Curse;
        }

        public SO_WeaponItem GetCurseWeapon()
        {
            if (curseWeapon != null)
            {
                return curseWeapon;
            }
            return null;
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
