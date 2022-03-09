using System;
using System.Collections;
using System.Collections.Generic;
using Game.Combat;
using Game.Control;
using Game.Enums;
using Game.Inventories;
using Game.Utils;
using UnityEngine;

namespace Game.Curses
{
    public class PlayerCurses : MonoBehaviour
    {
        [SerializeField] CurseTypes selectedCurse = CurseTypes.Werewolf;
        [Tooltip("Curse List Library")]
        [SerializeField] SO_CurseListLibrary curseListLibrary;

        LazyValue<SO_Curse> equipedCurseMonster;
        LazyValue<SO_Curse> equipedCurseHuman;

        private Equipment playerEquipment;
        private PlayerTransformControl playerTransform;
        private PlayerCombat playerCombat;
        private Inventory playerInventory;

        private SO_InventoryItem tempEquipedWeapon;

        private void Awake() 
        {
            equipedCurseMonster = new LazyValue<SO_Curse>(GetInitialCurseMonster);
            equipedCurseHuman = new LazyValue<SO_Curse>(GetInitialCurseHuman);

            playerEquipment = GetComponent<Equipment>();
            playerTransform = GetComponent<PlayerTransformControl>();
            playerCombat = GetComponent<PlayerCombat>();
            playerInventory = GetComponent<Inventory>();
        }

        private SO_Curse GetInitialCurseHuman()
        {
            return curseListLibrary.GetCurseSO(selectedCurse, PlayerTransformState.Human);
        }

        private SO_Curse GetInitialCurseMonster()
        {
            return curseListLibrary.GetCurseSO(selectedCurse, PlayerTransformState.Monster);
        }

        private void Start()
        {
            equipedCurseMonster.ForceInit();
            equipedCurseHuman.ForceInit();

            playerEquipment.AddItem(EquipLocation.Curse, equipedCurseHuman.value);
        }

        private void OnEnable()
        {
            EventHandler.TransformEvent += AddCurse;
            EventHandler.TransformEvent += RemoveCurse;
        }

        private void OnDisable()
        {
            EventHandler.TransformEvent -= AddCurse;
            EventHandler.TransformEvent -= RemoveCurse;
        }

        public SO_Curse GetCurse()
        {
            return equipedCurseMonster.value;
        }
        private void AddCurse()
        {
            if (!playerTransform.IsMonster)
            {
                playerEquipment.RemoveItem(EquipLocation.Curse);
                playerEquipment.AddItem(EquipLocation.Curse, equipedCurseMonster.value);
                EquipCurseAttack();
            }
        }
        public void RemoveCurse()
        {
            if (playerTransform.IsMonster)
            {
                playerEquipment.RemoveItem(EquipLocation.Curse);
                UnequipCurseAttack();
                playerEquipment.AddItem(EquipLocation.Curse, equipedCurseHuman.value);
            }
        }

        private void EquipCurseAttack()
        {
            if (equipedCurseMonster.value.GetCurseWeapon() != null)
            {
                tempEquipedWeapon = playerEquipment.GetItemInSlot(EquipLocation.Weapon);
                if (tempEquipedWeapon != null)
                {
                    playerInventory.AddToFirstEmptySlot(tempEquipedWeapon, 1);
                    playerCombat.EquipWeapon(equipedCurseMonster.value.GetCurseWeapon());
                    playerEquipment.AddItem(EquipLocation.Weapon, equipedCurseMonster.value.GetCurseWeapon());
                }
                else
                {
                    playerCombat.EquipWeapon(equipedCurseMonster.value.GetCurseWeapon());
                    playerEquipment.AddItem(EquipLocation.Weapon, equipedCurseMonster.value.GetCurseWeapon());
                }
            }
        }

        private void UnequipCurseAttack()
        {
            if (equipedCurseMonster.value.GetCurseWeapon() != null)
            {
                if (tempEquipedWeapon != null)
                {
                    int weaponSlot = playerInventory.GetItemSlot(tempEquipedWeapon, 1);
                    playerInventory.RemoveFromSlot(weaponSlot, 1);
                    playerCombat.EquipWeapon(tempEquipedWeapon as SO_WeaponItem);
                    playerEquipment.AddItem(EquipLocation.Weapon, tempEquipedWeapon as SO_WeaponItem);
                }
                else
                {
                    playerEquipment.RemoveItem(EquipLocation.Weapon);
                }
            }
        }

        [System.Serializable]
        struct CursePairTypes
        {
            public CurseTypes curseType;
            public SO_Curse curseHumanForm;
            public SO_Curse curseMonsterForm;
        }  
    }
}
