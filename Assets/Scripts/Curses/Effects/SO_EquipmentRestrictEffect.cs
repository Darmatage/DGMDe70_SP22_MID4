using System;
using System.Collections.Generic;
using Game.Enums;
using UnityEngine;

namespace Game.Curses.Effects
{
    [CreateAssetMenu(fileName = "Effect_EquipmentRestrict_", menuName = "Game/Player/Curses/Effects/Equipment Material Restrict")]
    public class SO_EquipmentRestrictEffect : SO_EffectStrategy, ICurseProvider
    {
        [SerializeField] string curseEffectName;
        [SerializeField] CurseEffectConditionType curseEffectConditionType = CurseEffectConditionType.None;
        [Tooltip("Curse effect description.")]
        [SerializeField][TextArea] string description = null;
        [Tooltip("List restricted materials.")]
        [SerializeField] RestrictedArmorMaterial[] restrictedArmorMaterialList;
        private CurseEffectTypes curseEffectType = CurseEffectTypes.EquipmentRestrictMaterial;


        [System.Serializable]
        struct RestrictedArmorMaterial
        {
            public EquipmentMaterial cantEquipArmor;
        } 
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
                yield return 0f;
            }
        }

        public IEnumerable<EquipmentMaterial> GetRestictedArmorMaterial()
        {
            foreach (var materialItem in restrictedArmorMaterialList)
            {
                yield return materialItem.cantEquipArmor;
            }
        }

        public override CurseEffectConditionType GetCurseEffectConditionType()
        {
            return curseEffectConditionType;
        }

    }
}