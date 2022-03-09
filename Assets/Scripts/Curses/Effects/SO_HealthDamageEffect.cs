using System;
using System.Collections.Generic;
using Game.Enums;
using Game.PlayerClass;
using UnityEngine;

namespace Game.Curses.Effects
{
    [CreateAssetMenu(fileName = "Health Damage Effect", menuName = "Game/Player/Curses/Effects/Damage Health")]
    public class SO_HealthDamageEffect : SO_EffectStrategy, ICurseProvider
    {
        [Tooltip("Curse Effect Modifier.")]
        [SerializeField] Modifier[] curseEffectMod;
        private CurseEffectTypes curseEffectType = CurseEffectTypes.DamageHealth;

        public override CurseEffectTypes GetCurseEffectType()
        {
            return curseEffectType;
        }
        public override bool EnableCurseEffect(CurseEffectTypes effectType)
        {
            if(effectType == curseEffectType)
            {
                Debug.Log("It hurts!");
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