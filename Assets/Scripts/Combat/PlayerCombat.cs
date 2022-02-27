using System.Collections;
using System.Collections.Generic;
using Game.Movement;
using UnityEngine;

namespace Game.Combat
{
    public class PlayerCombat : MonoBehaviour
    {
        [SerializeField] float weaponDamage = 5f;
        [SerializeField] bool rangeWeapon = false;
        [SerializeField] Transform rangeAttackLaunchPosition;
        [SerializeField] PlayerProjectile projectile = null;
        // The hitboxes of our 4 different directions

        private void OnEnable()
        {
            EventHandler.PlayerInputEvent += SetAttackDirection;
        }

        private void OnDisable()
        {
            EventHandler.PlayerInputEvent -= SetAttackDirection;
        }

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

