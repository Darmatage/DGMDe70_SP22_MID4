using System;
using System.Collections.Generic;
using Game.Enums;
using UnityEngine;

namespace Game.Curses.Effects
{
    [CreateAssetMenu(fileName = "Souls Health Effect", menuName = "Game/Player/Curses/Effects/Souls Health")]
    public class SO_SoulsHealthEffect : SO_EffectStrategy, ICurseProvider
    {
        [SerializeField] string curseEffectName;
        [Tooltip("Curse Effect Modifier.")]
        [SerializeField] float healthHealValue = 0f;
        private CurseEffectTypes curseEffectType = CurseEffectTypes.SoulHealBonus;

        public override CurseEffectTypes GetCurseEffectType()
        {
            return curseEffectType;
        }
        public override bool EnableCurseEffect(CurseEffectTypes effectType)
        {
            if(effectType == curseEffectType)
            {
                return true;
            }
            return false;
        }
        public override string GetCurseEffectName()
        {
            return curseEffectName;
        }
        public IEnumerable<float> GetCurseModifiers(CurseEffectTypes effectType)
        {
            if (effectType == curseEffectType)
            {
                yield return healthHealValue;
            }
        }
    }
}