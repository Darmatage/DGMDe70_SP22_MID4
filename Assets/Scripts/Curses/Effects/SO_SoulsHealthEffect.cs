using System;
using System.Collections.Generic;
using Game.Enums;
using Game.PlayerClass;
using UnityEngine;

namespace Game.Curses.Effects
{
    [CreateAssetMenu(fileName = "Souls Health Effect", menuName = "Game/Player/Curses/Effects/Souls Health", order = 0)]
    public class SO_SoulsHealthEffect : SO_EffectStrategy, ICurseProvider
    {
        [SerializeField] CurseEffectTypes curseEffectType = CurseEffectTypes.None;
        [Tooltip("Curse Effect Modifier.")]
        [SerializeField] Modifier[] curseEffectMod;
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