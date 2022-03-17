using System;
using System.Collections.Generic;
using Game.Enums;
using UnityEngine;

namespace Game.Curses.Effects
{
    /// <summary>
    /// This has no effect, its just used to add extra info into the 
    /// Boon (Advantages) and Bust (Disadvantages) List.
    /// </summary>

    [CreateAssetMenu(fileName = "Effect_Details_", menuName = "Game/Player/Curses/Effects/Details")]
    public class SO_DetailEffect : SO_EffectStrategy
    {
        [SerializeField] string curseEffectName;
        [SerializeField] CurseEffectConditionType curseEffectConditionType = CurseEffectConditionType.None;
        [Tooltip("Curse effect description.")]
        [SerializeField][TextArea] string description = null;
        private CurseEffectTypes curseEffectType = CurseEffectTypes.DetailEffect;

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
        public override bool EnableCurseEffect(CurseEffectTypes curseEffectStats)
        {
            return false;
        }

        public override CurseEffectConditionType GetCurseEffectConditionType()
        {
            return curseEffectConditionType;
        }

    }
}

