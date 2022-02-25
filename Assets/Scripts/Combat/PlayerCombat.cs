using System.Collections;
using System.Collections.Generic;
using Game.Movement;
using UnityEngine;

namespace Game.Combat
{
    public class PlayerCombat : MonoBehaviour
    {
        [SerializeField] float weaponDamage = 5f;
        // The hitboxes of our 4 different directions
        [SerializeField] private GameObject hitBox_Up;
        [SerializeField] private GameObject hitBox_Bottom;
        [SerializeField] private GameObject hitBox_Left;
        [SerializeField] private GameObject hitBox_Right;

        private void OnEnable()
        {
            EventHandler.MovementEvent += SetActivateHitCollider;
        }

        private void OnDisable()
        {
            EventHandler.MovementEvent -= SetActivateHitCollider;
        }

        private void SetActivateHitCollider(float xInput, float yInput, bool isWalking, bool isRunning, bool isIdle,
            bool isAttackingRight, bool isAttackingLeft, bool isAttackingUp, bool isAttackingDown,
            bool idleUp, bool idleDown, bool idleLeft, bool idleRight)
        {
            if (isAttackingUp) 
            {
                hitBox_Up.SetActive(true);
            }
            if (isAttackingRight) 
            {
                hitBox_Right.SetActive(true);
            }
            if (isAttackingLeft) 
            {
                hitBox_Left.SetActive(true);
            }
            if (isAttackingDown) 
            {
                hitBox_Bottom.SetActive(true);
            }
        }

        //Using the animation triggers to reset the Hitboxes
        public void DeactivateHitCollider()
        {
            hitBox_Left.SetActive(false);
            hitBox_Right.SetActive(false);
            hitBox_Bottom.SetActive(false);
            hitBox_Up.SetActive(false);
        }

        public float GetWeaponDamage()
        {
            return weaponDamage;
        }

    }
    
}

