using System;
using System.Collections.Generic;
using Game.Enums;
using UnityEngine;

namespace Game.Curses.Effects
{
    [CreateAssetMenu(fileName = "Armor Restrict Effect", menuName = "Game/Player/Curses/Effects/Armor Restrict")]
    public class SO_ArmorRestrictEffect : SO_EffectStrategy, ICurseProvider
    {
        [SerializeField] string curseEffectName;
        [Tooltip("Curse Effect Modifier.")]
        [SerializeField] RestrictedArmorMaterial[] restrictedArmorMaterialList;
        private CurseEffectTypes curseEffectType = CurseEffectTypes.ArmorRestrictMaterial;


        [System.Serializable]
        struct RestrictedArmorMaterial
        {
            public EquipmentMaterial cantEquipArmor;
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
        public override string GetCurseEffectName()
        {
            return curseEffectName;
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


    }
}