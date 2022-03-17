using System.Collections.Generic;
using Game.Enums;
using UnityEngine;

namespace Game.Inventories
{

    [CreateAssetMenu(fileName = "EquipmentSetupLibrary", menuName = ("Game/Setup/New Equipment Setup Library"))]
    public class SO_EquipmentSetupLibrary : ScriptableObject
    {
        [SerializeField] EquipmentSetupByCurseList[] equipmentSetupByCurseList = null;

        Dictionary<CurseTypes, Dictionary<EquipLocation, SO_EquipableItem>> equipmentByCurseLookupTable = null;

        public IEnumerable<EquipLocation> GetEquipLocations(CurseTypes curseType)
        {
            BuildLookup();

            if (!equipmentByCurseLookupTable.ContainsKey(curseType))
            {
                yield break;
            }

            foreach (var location in equipmentByCurseLookupTable[curseType])
            {
                yield return location.Key;
            }
        }
        public SO_EquipableItem GetEquipableItemSO(CurseTypes curseType, EquipLocation equipLocation)
        {
            BuildLookup();

            if (!equipmentByCurseLookupTable[curseType].ContainsKey(equipLocation))
            {
                return null;
            }

            return equipmentByCurseLookupTable[curseType][equipLocation];
        }


        private void BuildLookup()
        {
            if (equipmentByCurseLookupTable != null) return;

            equipmentByCurseLookupTable = new Dictionary<CurseTypes, Dictionary<EquipLocation, SO_EquipableItem>>();

            foreach (EquipmentSetupByCurseList equipmentSetupByCurseItem in equipmentSetupByCurseList)
            {
                var equipmentLookupTable = new Dictionary<EquipLocation, SO_EquipableItem>();

                foreach (SetupEquipmentItems setupEquipmentItem in equipmentSetupByCurseItem.setupEquipmentItemList)
                {
                    equipmentLookupTable[setupEquipmentItem.equipLocation] = setupEquipmentItem.equipableItemSO;
                }

                equipmentByCurseLookupTable[equipmentSetupByCurseItem.curseType] = equipmentLookupTable;
            }
        }

        [System.Serializable]
        class EquipmentSetupByCurseList
        {
            public CurseTypes curseType;
            public SetupEquipmentItems[] setupEquipmentItemList;
        }

        [System.Serializable]
        class SetupEquipmentItems
        {
            public EquipLocation equipLocation;
            public SO_EquipableItem equipableItemSO;
        }

       
    }
}
