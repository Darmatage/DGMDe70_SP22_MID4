using System;
using System.Collections;
using System.Collections.Generic;
using Game.Combat;
using Game.Control;
using Game.Core;
using Game.Enums;
using Game.Inventories;
using Game.Saving;
using Game.Utils;
using UnityEngine;

namespace Game.Curses
{
    public class PlayerCurses : MonoBehaviour, ISaveable
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
        private CooldownStore cooldownStore;
        private PlayerHealth playerHealth;

        private SO_InventoryItem tempEquipedWeapon;
        private float gameTick = 0f;
        private bool isGamePaused = false;

        private void Awake() 
        {
            equipedCurseMonster = new LazyValue<SO_Curse>(GetInitialCurseMonster);
            equipedCurseHuman = new LazyValue<SO_Curse>(GetInitialCurseHuman);

            playerEquipment = GetComponent<Equipment>();
            playerTransform = GetComponent<PlayerTransformControl>();
            playerCombat = GetComponent<PlayerCombat>();
            playerInventory = GetComponent<Inventory>();
            cooldownStore = GetComponent<CooldownStore>();
            playerHealth = GetComponent<PlayerHealth>();
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
            EventHandler.TransformEvent += ActivateCurse;
            EventHandler.ActiveGameUI += GamePaused;
        }

        private void OnDisable()
        {
            EventHandler.TransformEvent -= ActivateCurse;
            EventHandler.ActiveGameUI -= GamePaused;
        }

        private void Update() 
        {
            if (!isGamePaused)
            {
                EarlyCurseTransform();
            }
        }

        private void EarlyCurseTransform()
        {
            gameTick += Time.deltaTime;
            if (gameTick >= Settings.secondsPerGameSecond)
            {
                gameTick -= Settings.secondsPerGameSecond;
                if (cooldownStore.GetTimeRemaining(equipedCurseMonster.value) > 0 && !playerTransform.IsMonster)
                {
                    if (equipedCurseHuman.value.HasCurseEffects(CurseEffectTypes.DamageHealth))
                    {
                        playerHealth.TakeDamage(equipedCurseHuman.value.GetCurseEffectModifier(CurseEffectTypes.DamageHealth));
                    }
                    
                    Debug.Log("Curse cooldown penalty! ");
                }
            }
        }

        public SO_Curse GetCurse()
        {
            return equipedCurseMonster.value;
        }
        private void ActivateCurse()
        {
            if (!playerTransform.IsMonster)
            {
                equipedCurseMonster.value.ActivateCurse(gameObject);
                playerEquipment.RemoveItem(EquipLocation.Curse);
                playerEquipment.AddItem(EquipLocation.Curse, equipedCurseMonster.value);

                EquipCurseAttack();
            }
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

        private void GamePaused(bool gameState)
        {
            isGamePaused = gameState;
        }

        private struct CursePairRecord
        {
            public SO_Curse curseHumanForm;
            public SO_Curse curseMonsterForm;
            public CooldownStore cooldownStore;
            public PlayerTransformControl playerTransform;
            public bool isGamePaused;
        }

        object ISaveable.CaptureState()
        {
            var cursePairRecord = new CursePairRecord();

            cursePairRecord.curseHumanForm = equipedCurseHuman.value;
            cursePairRecord.curseMonsterForm = equipedCurseMonster.value;
            cursePairRecord.cooldownStore = cooldownStore;
            cursePairRecord.playerTransform = playerTransform;
            cursePairRecord.isGamePaused = isGamePaused;

            return cursePairRecord;
        }

        void ISaveable.RestoreState(object state)
        {
            var cursePairRecord = (CursePairRecord)state;

            equipedCurseHuman.value = cursePairRecord.curseHumanForm;
            equipedCurseMonster.value = cursePairRecord.curseMonsterForm;
            cooldownStore = cursePairRecord.cooldownStore;
            playerTransform = cursePairRecord.playerTransform;
            isGamePaused = cursePairRecord.isGamePaused;

            ActivateCurse();
        }
    }
}
