using System;
using System.Collections;
using System.Collections.Generic;
using Game.Combat;
using Game.Control;
using Game.Enums;
using Game.Inventories;
using UnityEngine;

namespace Game.Curses
{
    public class PlayerCurses : MonoBehaviour
    {
        [SerializeField] SO_Curse equipedCurse;
        Equipment playerEquipment;
        PlayerTransformControl playerTransform;
        PlayerCombat playerCombat;
        Inventory playerInventory;

        private SO_InventoryItem tempEquipedWeapon;

        private void Awake() 
        {
            //var player = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG);
            playerEquipment = GetComponent<Equipment>();
            playerTransform = GetComponent<PlayerTransformControl>();
            playerCombat = GetComponent<PlayerCombat>();
            playerInventory = GetComponent<Inventory>();
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
        

        private void AddCurse()
        {
            if (!playerTransform.IsMonster)
            {
                playerEquipment.AddItem(EquipLocation.Curse, equipedCurse);
                if(equipedCurse.GetCurseWeapon() != null)
                {
                    tempEquipedWeapon = playerEquipment.GetItemInSlot(EquipLocation.Weapon);
                    if (tempEquipedWeapon != null)
                    {
                        playerInventory.AddToFirstEmptySlot(tempEquipedWeapon, 1);
                        playerCombat.EquipWeapon(equipedCurse.GetCurseWeapon());
                        playerEquipment.AddItem(EquipLocation.Weapon, equipedCurse.GetCurseWeapon());
                    }
                } 
            }
        }

        public void RemoveCurse()
        {
            if (playerTransform.IsMonster)
            {
                playerEquipment.RemoveItem(EquipLocation.Curse);
                if(equipedCurse.GetCurseWeapon() != null)
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
                    // playerCombat.EquipWeapon(equipedCurse.GetCurseWeapon());
                    
                } 
            }
        }

    }
}
