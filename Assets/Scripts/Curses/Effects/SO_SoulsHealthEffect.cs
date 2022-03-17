using System;
using System.Collections.Generic;
using Game.Enums;
using Game.ClassTypes.Player;
using UnityEngine;

namespace Game.Curses.Effects
{
    [CreateAssetMenu(fileName = "Souls Health Effect", menuName = "Game/Player/Curses/Effects/Souls Health")]
    public class SO_SoulsHealthEffect : SO_EffectStrategy, ICurseProvider
    {
        [SerializeField] string curseEffectName;
        [Tooltip("Curse Effect Modifier.")]
        [SerializeField] Modifier[] curseEffectMod;
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
        [System.Serializable]
        struct Modifier
        {
            public CurseEffectTypes stat;
            public int value;
        }  

        public IEnumerable<int> GetCurseModifiers(CurseEffectTypes stat)
        {
            foreach (var modifier in curseEffectMod)
            {
                if (modifier.stat == stat)
                {
                    yield return modifier.value;
                }
            }
        }
    }
}