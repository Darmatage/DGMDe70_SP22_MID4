using System.Collections;
using System.Collections.Generic;
using Game.Enums;
using Game.Inventories;
using Game.Movement;
using Game.Utils;
using UnityEngine;

namespace Game.Combat
{
    public class PlayerCombat : MonoBehaviour
    {
        [SerializeField] float weaponDamage = 5f;
        [SerializeField] bool rangeWeapon = false;
        [SerializeField] Transform rangeAttackLaunchPosition;
        [SerializeField] PlayerProjectile projectile = null;

        [SerializeField] Transform weaponPosition = null;
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
            //currentWeapon.value = AttachWeapon(weapon);
        }

        private void UpdateWeapon()
        {
            var weapon = equipment.GetItemInSlot(EquipLocation.Hand_Right) as SO_WeaponItem;
            if (weapon == null)
            {
                EquipWeapon(defaultWeapon);
            }
            else
            {
                EquipWeapon(weapon);
            }
        }
        // private PlayerWeaponPrefab AttachWeapon(SO_WeaponItem weapon)
        // {
        //     //Animator animator = GetComponent<Animator>();
        //     return weapon.Spawn(weaponPosition, animator);
        // }

        private void SetAttackDirection(float xInput, float yInput, bool isWalking, bool isRunning, bool isIdle,
            bool isAttackingRight, bool isAttackingLeft, bool isAttackingUp, bool isAttackingDown,
            bool idleUp, bool idleDown, bool idleLeft, bool idleRight)
        {
            if(rangeWeapon)
            {
                if (isAttackingUp) 
                {
                    LaunchProjectile(Vector3.up);
                }
                if (isAttackingRight) 
                {
                    LaunchProjectile(Vector3.right);
                }
                if (isAttackingLeft) 
                {
                    LaunchProjectile(Vector3.left);
                }
                if (isAttackingDown) 
                {
                    LaunchProjectile(Vector3.down);
                }
            }
            else
            {
                GetComponentInChildren<PlayerHitCollidersController>().ActivateMeleeHitCollider(isAttackingRight, isAttackingLeft, isAttackingUp, isAttackingDown);
            }
        }

        private void LaunchProjectile(Vector3 launchDirection)
        {
            PlayerProjectile projectInstance = Instantiate(projectile, rangeAttackLaunchPosition.position, Quaternion.identity, GameObject.FindGameObjectWithTag(Tags.ProjectilesTag).transform);
            projectInstance.SetTarget(launchDirection, weaponDamage);
        }

    }
    
}

