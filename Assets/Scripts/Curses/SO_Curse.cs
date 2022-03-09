using System.Collections;
using System.Collections.Generic;
using Game.Enums;
using Game.Inventories;
using Game.PlayerClass;
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

        [Header("Curse Stat Modifiers")]
        [Tooltip("Modify base stats.")]
        [SerializeField] Modifier[] additiveModifiers;
        [Tooltip("Modify base stats by percentage.")]
        [SerializeField] Modifier[] percentageModifiers;
        [Tooltip("Does the Curse have it's own attack?")]
        [SerializeField] SO_WeaponItem curseWeapon = null;
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

        public int GetCurseEffectModifier(CurseEffectTypes curseEffectType)
        {
            int total = 0;
            foreach (ICurseProvider curseProvider in effectStrategies)
            {
                foreach (int modifier in curseProvider.GetCurseModifiers(curseEffectType))
                {
                    total += modifier;
                }
            }
            return total;
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
