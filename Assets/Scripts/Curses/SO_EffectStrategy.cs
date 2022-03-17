using System;
using System.Collections.Generic;
using Game.Enums;
using UnityEngine;

namespace Game.Curses
{
    public abstract class SO_EffectStrategy : ScriptableObject
    {
        public abstract string GetCurseEffectName();
        public abstract string GetDescription();
        public abstract CurseEffectTypes GetCurseEffectType();
        public abstract CurseEffectConditionType GetCurseEffectConditionType();
        public abstract bool EnableCurseEffect(CurseEffectTypes curseEffectStats);
    }

}