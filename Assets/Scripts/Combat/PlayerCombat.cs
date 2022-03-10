using System.Collections;
using System.Collections.Generic;
using Game.Enums;
using Game.Inventories;
using Game.Movement;
using Game.PlayerClass;
using Game.Saving;
using Game.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Combat
{
    public class PlayerCombat : MonoBehaviour, ISaveable
    {

        [SerializeField] Transform rangeAttackLaunchPosition;
        [SerializeField] SO_WeaponItem _defaultWeapon = null;
        public SO_WeaponItem DefaultWeapon {get { return _defaultWeapon; }}
        SO_WeaponItem currentWeaponConfig;
        Equipment equipment;

        private void Awake() 
        {
            currentWeaponConfig = _defaultWeapon;
            equipment = GetComponent<Equipment>();
            if(equipment)
            {
                equipment.equipmentUpdated += UpdateWeapon;
            }
        }

        private void OnEnable()
        {
            EventHandler.PlayerInputEvent += SetMeleeAttackDirection;
            EventHandler.PlayerInputEvent += SetRangeAttackDirection;
        }

        private void OnDisable()
        {
            EventHandler.PlayerInputEvent -= SetMeleeAttackDirection;
            EventHandler.PlayerInputEvent -= SetRangeAttackDirection;
        }

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
                EquipWeapon(_defaultWeapon);
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

        private void SetMeleeAttackDirection(float xInput, float yInput, bool isWalking, bool isRunning, bool isIdle, bool isMakingAttack,
            bool isAttackingUp, bool isAttackingRight, bool isAttackingDown, bool isAttackingLeft,
            bool idleUp, bool idleDown, bool idleLeft, bool idleRight)
        {
            if(!isMakingAttack) return;
            if(!currentWeaponConfig.HasProjectile())
            {
                bool isAttackUp = false;
                bool isAttackRight = false;
                bool isAttackDown = false;
                bool isAttackLeft = false;

                Vector3 meleeAttackDirection = GetAttackDirection();

                if (meleeAttackDirection.y > 0.7f)
                {
                    Debug.Log("Up");
                    isAttackUp = true;
                }
                if (meleeAttackDirection.x > 0.7f)
                {
                    Debug.Log("Right");
                    isAttackRight = true;
                }
                if (meleeAttackDirection.y < -0.7f)
                {
                    Debug.Log("Down");
                    isAttackDown = true;
                }
                if (meleeAttackDirection.x < -0.7f)
                {
                    Debug.Log("Left");
                    isAttackLeft = true;
                }

                EventHandler.CallPlayerAttackEvent(isAttackUp, isAttackRight, isAttackDown, isAttackLeft);
                GetComponentInChildren<PlayerHitCollidersController>().ActivateMeleeHitCollider(isAttackUp, isAttackRight, isAttackDown, isAttackLeft);
            }
        }

        private void SetRangeAttackDirection(float xInput, float yInput, bool isWalking, bool isRunning, bool isIdle, bool isMakingAttack,
            bool isAttackingUp, bool isAttackingRight, bool isAttackingDown, bool isAttackingLeft,
            bool idleUp, bool idleDown, bool idleLeft, bool idleRight)
        {
            if(!isMakingAttack) return;
            if(currentWeaponConfig.HasProjectile())
            {
                float damage = GetComponent<PlayerBaseStats>().GetStat(PlayerStats.BaseDamage);

                bool isAttackUp = false;
                bool isAttackRight = false;
                bool isAttackDown = false;
                bool isAttackLeft = false;

                Vector3 projectileLaunchDirection = GetAttackDirection();

                if (projectileLaunchDirection.y > 0.7f)
                {
                    Debug.Log("Up");
                    isAttackUp = true;
                }
                if (projectileLaunchDirection.x > 0.7f)
                {
                    Debug.Log("Right");
                    isAttackRight = true;
                }
                if (projectileLaunchDirection.y < -0.7f)
                {
                    Debug.Log("Down");
                    isAttackDown = true;
                }
                if (projectileLaunchDirection.x < -0.7f)
                {
                    Debug.Log("Left");
                    isAttackLeft = true;
                }

                EventHandler.CallPlayerAttackEvent(isAttackUp, isAttackRight, isAttackDown, isAttackLeft);
                currentWeaponConfig.LaunchProjectile(rangeAttackLaunchPosition, projectileLaunchDirection, damage);
            }
        }

        private Vector3 GetAttackDirection()
        {
            Vector3 mousePos = Mouse.current.position.ReadValue();
            mousePos.z = Camera.main.nearClipPlane;
            Vector3 clickPos = Camera.main.ScreenToWorldPoint(mousePos);

            //Debug.Log("Mouse Pos: " + clickPos);

            Vector3 launchDirection = new Vector3(
                clickPos.x - transform.position.x,
                clickPos.y - transform.position.y,
                0
            );

            launchDirection.Normalize();
            return launchDirection;
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

