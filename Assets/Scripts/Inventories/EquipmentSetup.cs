using System;
using System.Collections;
using System.Collections.Generic;
using Game.Combat;
using Game.Control;
using Game.Core;
using Game.Enums;
using Game.Saving;
using Game.Utils;
using UnityEngine;

namespace Game.Inventories
{
    public class EquipmentSetup : MonoBehaviour
    {
        [Tooltip("Setup Equipment Library")]
        [SerializeField] SO_EquipmentSetupLibrary equipmentSetupLibrary;

        private CurseTypes selectedCurse;
        private Equipment playerEquipment;

        private void Awake() 
        {
            selectedCurse = GameObject.FindWithTag(Tags.SAVING_STATE_PERSISTS_TAG).GetComponent<SavedFileSingleton>().GetCurseType();
            playerEquipment = GetComponent<Equipment>();
        }

        private void OnEnable()
        {
            EventHandler.LoadFirstSceneEvent += StartEquipmentSetup;
        }

        private void OnDisable()
        {
            EventHandler.LoadFirstSceneEvent -= StartEquipmentSetup;
        }

        private void StartEquipmentSetup()
        {
            var equipLocationList = equipmentSetupLibrary.GetEquipLocations(selectedCurse);
            foreach (var equipmentLocation in equipLocationList)
            {
                playerEquipment.AddItem(equipmentLocation, equipmentSetupLibrary.GetEquipableItemSO(selectedCurse, equipmentLocation));
                Debug.Log(equipmentSetupLibrary.GetEquipableItemSO(selectedCurse, equipmentLocation));
            } 
        }
        
    }
}
