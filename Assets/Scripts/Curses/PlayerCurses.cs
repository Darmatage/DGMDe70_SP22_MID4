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
        //[SerializeField] CurseTypes selectedCurse = CurseTypes.Werewolf;
        [Tooltip("Curse List Library")]
        [SerializeField] SO_CurseListLibrary curseListLibrary;

        CurseTypes selectedCurse;
        LazyValue<SO_Curse> equipedCurseMonster;
        LazyValue<SO_Curse> equipedCurseHuman;

        private Equipment playerEquipment;
        private PlayerCombat playerCombat;
        private Inventory playerInventory;

        private SO_InventoryItem tempEquipedWeapon;
        private bool isMonster;
        private bool isGamePaused = false;
        private float gameTick = 0f;

        private void Awake() 
        {
            selectedCurse = GameObject.FindWithTag(Tags.SAVING_STATE_PERSISTS_TAG).GetComponent<SavedFileSingleton>().GetCurseType();

            equipedCurseMonster = new LazyValue<SO_Curse>(GetInitialCurseMonster);
            equipedCurseHuman = new LazyValue<SO_Curse>(GetInitialCurseHuman);

            playerEquipment = GetComponent<Equipment>();
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

            isMonster = GetComponent<PlayerTransformControl>().IsMonster;

            playerEquipment.AddItem(EquipLocation.Curse, equipedCurseHuman.value);
        }
        private void OnEnable()
        {
            EventHandler.PlayerTransformStateEvent += TransformPlayer;
            EventHandler.ActiveGameUI += GamePaused;
        }

        private void OnDisable()
        {
            EventHandler.PlayerTransformStateEvent -= TransformPlayer;
            EventHandler.ActiveGameUI -= GamePaused;
        }

        private void Update() 
        {
            if (!isGamePaused)
            {
                EarlyCurseTransform();
            }
        }

        public SO_Curse GetCurse()
        {
            return equipedCurseMonster.value;
        }

        public string GetCurseName()
        {
            return equipedCurseMonster.value.GetDisplayName();
        }
        public string GetCurseDescription()
        {
            return equipedCurseMonster.value.GetDescription();
        }

        public string[] GetCurseMonsterEffectNames()
        {
            return equipedCurseMonster.value.GetCurseEffectNames();
        }
        public string[] GetCurseHumanEffectNames()
        {
            return equipedCurseHuman.value.GetCurseEffectNames();
        }

        private void TransformPlayer(PlayerTransformState transformState)
        {
            if (transformState == PlayerTransformState.Monster && isMonster == false) 
            {
                isMonster = true;
            }
            if (transformState == PlayerTransformState.Human && isMonster == true)
            {
                isMonster = false;
            }
            ActivateCurse();
        }

        private void ActivateCurse()
        {
            if (isMonster == true)
            {
                equipedCurseMonster.value.ActivateCurse(gameObject);
                playerEquipment.RemoveItem(EquipLocation.Curse);
                playerEquipment.AddItem(EquipLocation.Curse, equipedCurseMonster.value);

                EquipCurseAttack();
            }
            if (isMonster == false)
            {
                playerEquipment.RemoveItem(EquipLocation.Curse);
                UnequipCurseAttack();
                playerEquipment.AddItem(EquipLocation.Curse, equipedCurseHuman.value);
            }
        }

        private void EarlyCurseTransform()
        {
            gameTick += Time.deltaTime;
            if (gameTick >= Settings.secondsPerGameSecond)
            {
                gameTick -= Settings.secondsPerGameSecond;
                if (GetComponent<CooldownStore>().GetTimeRemaining(equipedCurseMonster.value) > 0 && !isMonster)
                {
                    if (equipedCurseHuman.value.HasCurseEffects(CurseEffectTypes.DamageHealth))
                    {
                        Debug.Log("Curse cooldown penalty! ");
                        GetComponent<PlayerHealth>().TakeDamage(equipedCurseHuman.value.GetCurseEffectModifier(CurseEffectTypes.DamageHealth));
                    }
                }
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
            public string curseHumanFormName;
            public string curseMonsterFormName;
            public bool IsGamePaused;
        }

        object ISaveable.CaptureState()
        {
            var cursePairRecord = new CursePairRecord();

            cursePairRecord.curseHumanFormName = equipedCurseHuman.value.name;
            cursePairRecord.curseMonsterFormName = equipedCurseMonster.value.name;
            cursePairRecord.IsGamePaused = isGamePaused;

            return cursePairRecord;
        }

        void ISaveable.RestoreState(object state)
        {
            var cursePairRecord = (CursePairRecord)state;

            equipedCurseHuman.value = UnityEngine.Resources.Load<SO_Curse>(cursePairRecord.curseHumanFormName);
            equipedCurseMonster.value = UnityEngine.Resources.Load<SO_Curse>(cursePairRecord.curseMonsterFormName);
            isGamePaused = cursePairRecord.IsGamePaused;
        }
    }
}
