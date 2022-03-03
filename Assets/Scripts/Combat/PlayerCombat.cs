using System.Collections;
using System.Collections.Generic;
using Game.Enums;
using Game.Inventories;
using Game.Movement;
using Game.PlayerClass;
using Game.Saving;
using Game.Utils;
using UnityEngine;

namespace Game.Combat
{
    public class PlayerCombat : MonoBehaviour, ISaveable
    {

        [SerializeField] Transform rangeAttackLaunchPosition;
        [SerializeField] SO_WeaponItem defaultWeapon = null;
        SO_WeaponItem currentWeaponConfig;
        //LazyValue<PlayerWeaponPrefab> currentWeapon;
        Equipment equipment;

        private void Awake() 
        {
            currentWeaponConfig = defaultWeapon;
            //currentWeapon = new LazyValue<PlayerWeaponPrefab>(SetupDefaultWeapon);
            equipment = GetComponent<Equipment>();
            if(equipment)
            {
                equipment.equipmentUpdated += UpdateWeapon;
            }
        }
        // private PlayerWeaponPrefab SetupDefaultWeapon()
        // {
        //     return AttachWeapon(defaultWeapon);
        // }

        private void OnEnable()
        {
            EventHandler.PlayerInputEvent += SetAttackDirection;
        }

        private void OnDisable()
        {
            EventHandler.PlayerInputEvent -= SetAttackDirection;
        }
        // void Start() 
        // {
        //     currentWeapon.ForceInit();
        // }
        public void EquipWeapon(SO_WeaponItem weapon)
        {
            currentWeaponConfig = weapon;
            AttachWeapon(weapon);
        }

        private void UpdateWeapon()
        {
            var weapon = equipment.GetItemInSlot(EquipLocation.Weapon) as SO_WeaponItem;
            if (weapon == null)
            {
                EquipWeapon(defaultWeapon);
            }
            else
            {
                EquipWeapon(weapon);
            }
        }
        private void AttachWeapon(SO_WeaponItem weapon)
        {
            PlayerHitCollidersController playerWeapon = GetComponentInChildren<PlayerHitCollidersController>();
            Animator animator = playerWeapon.gameObject.GetComponent<Animator>();
            weapon.Spawn(animator);
        }

        private void SetAttackDirection(float xInput, float yInput, bool isWalking, bool isRunning, bool isIdle, bool isMakingAttack,
            bool isAttackingRight, bool isAttackingLeft, bool isAttackingUp, bool isAttackingDown,
            bool idleUp, bool idleDown, bool idleLeft, bool idleRight)
        {
            if(!isMakingAttack) return;

            float damage = GetComponent<PlayerBaseStats>().GetStat(PlayerStats.BaseDamage);
            //Debug.Log("Attack Damage: " + damage);

            if(currentWeaponConfig.HasProjectile())
            {
                Vector3 launchDirection = new Vector3(0.0f, 0.0f, 0.0f);
                if (isAttackingUp) 
                {
                    launchDirection = Vector3.up;
                }
                if (isAttackingRight) 
                {
                    launchDirection = Vector3.right;
                }
                if (isAttackingLeft) 
                {
                    launchDirection = Vector3.left;
                }
                if (isAttackingDown) 
                {
                    launchDirection = Vector3.down;
                }
                currentWeaponConfig.LaunchProjectile(rangeAttackLaunchPosition, launchDirection, damage);
            }
            else
            {
                GetComponentInChildren<PlayerHitCollidersController>().ActivateMeleeHitCollider(isAttackingRight, isAttackingLeft, isAttackingUp, isAttackingDown);
            }
        }

        object ISaveable.CaptureState()
        {
            return currentWeaponConfig.name;
        }

        void ISaveable.RestoreState(object state)
        {
            string weaponName = (string)state;
            SO_WeaponItem weapon = UnityEngine.Resources.Load<SO_WeaponItem>(weaponName);
            EquipWeapon(weapon);
        }

    }
    
}

