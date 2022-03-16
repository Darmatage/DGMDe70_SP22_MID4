using System.Collections;
using System.Collections.Generic;
using Game.Enums;
using Game.Inventories;
using Game.ClassTypes.Player;
using UnityEngine;

namespace Game.Curses
{
    [CreateAssetMenu(fileName = "Curse", menuName = "Game/Player/Curses/New Curse")]
    public class SO_Curse : SO_EquipableItem, IModifierProvider
    {
        [Header("Curse Info")]
        [Tooltip("Which curse is this?")]
        [SerializeField] CurseTypes curseType;
        [Tooltip("What state is this?")]
        [SerializeField] PlayerTransformState cursePairState;
        [SerializeField] float cooldownTime = 0;
        [Tooltip("Does the Curse have it's own attack?")]
        [SerializeField] SO_WeaponItem curseWeapon = null;

        [Header("Curse Stat Modifiers")]
        [Tooltip("Modify base stats.")]
        [SerializeField] Modifier[] additiveModifiers;
        [Tooltip("Modify base stats by percentage.")]
        [SerializeField] Modifier[] percentageModifiers;
        [Tooltip("What else does the curse do?")]
        [SerializeField] SO_EffectStrategy[] effectStrategies;

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

        public bool ActivateCurse(GameObject player)
        {
            CooldownStore cooldownStore = player.GetComponent<CooldownStore>();
            if (cooldownStore.GetTimeRemaining(this) > 0)
            {
                return false;
            }
            StartCooldownTimer(cooldownStore);
            return true;
        }

        private void StartCooldownTimer(CooldownStore cooldownStore)
        {
            cooldownStore.StartCooldown(this, cooldownTime);
        }

        public IEnumerable<string> GetCurseEffectNames()
        {
            foreach (var effect in effectStrategies)
            {
                yield return effect.GetCurseEffectName();
            }
        }

        public bool HasCurseEffects(CurseEffectTypes curseEffectType)
        {
            bool hasCurseEffects = false;
            foreach (var effect in effectStrategies)
            {
                if(curseEffectType == effect.GetCurseEffectType())
                {
                    hasCurseEffects = effect.EnableCurseEffect(curseEffectType);
                }
            }
            return hasCurseEffects;
        }

        public SO_EffectStrategy GetSpecificCurseEffectStrategy(CurseEffectTypes curseEffectType)
        {
            foreach (var effect in effectStrategies)
            {
                if(curseEffectType == effect.GetCurseEffectType())
                {
                    return effect;
                }
            }
            return null;
        }

        public IEnumerable<float> GetCurseEffectModifier(CurseEffectTypes curseEffectType)
        {
            foreach (ICurseProvider curseProvider in effectStrategies)
            {
                foreach (float modifier in curseProvider.GetCurseModifiers(curseEffectType))
                {
                    yield return modifier;
                }
            }
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

        public override EquipmentMaterial GetEquipmentMaterial()
        {
            return EquipmentMaterial.None;
        }
    }
}
