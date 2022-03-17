using System;
using System.Collections.Generic;
using Game.Enums;
using UnityEngine;

namespace Game.Curses.Effects
{
    [CreateAssetMenu(fileName = "Effect_SoulsHealth_", menuName = "Game/Player/Curses/Effects/Souls Health")]
    public class SO_SoulsHealthEffect : SO_EffectStrategy, ICurseProvider
    {
        [SerializeField] string curseEffectName;
        [SerializeField] CurseEffectConditionType curseEffectConditionType = CurseEffectConditionType.None;
        [Tooltip("Curse effect description.")]
        [SerializeField][TextArea] string description = null;
        [Tooltip("Curse Effect Modifier.")]
        [SerializeField] float healthHealValue = 0f;
        private CurseEffectTypes curseEffectType = CurseEffectTypes.SoulHealBonus;

        public override string GetCurseEffectName()
        {
            return curseEffectName;
        }
        public override string GetDescription()
        {
            return description;
        }
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
        public IEnumerable<float> GetCurseModifiers(CurseEffectTypes effectType)
        {
            if (effectType == curseEffectType)
            {
                yield return healthHealValue;
            }
        }

        public override CurseEffectConditionType GetCurseEffectConditionType()
        {
            return curseEffectConditionType;
        }
    }
}