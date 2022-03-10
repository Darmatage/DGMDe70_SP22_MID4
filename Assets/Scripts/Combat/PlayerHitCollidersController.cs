using System.Collections;
using System.Collections.Generic;
using Game.Movement;
using UnityEngine;

namespace Game.Combat
{
    public class PlayerHitCollidersController : MonoBehaviour
    {
        // The hitboxes of our 4 different directions
        [SerializeField] private GameObject hitBox_Up;
        [SerializeField] private GameObject hitBox_Bottom;
        [SerializeField] private GameObject hitBox_Left;
        [SerializeField] private GameObject hitBox_Right;

        public void ActivateMeleeHitCollider(bool isAttackingUp, bool isAttackingRight, bool isAttackingDown, bool isAttackingLeft)
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

    }
    
}

