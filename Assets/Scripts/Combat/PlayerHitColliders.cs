using System.Collections;
using System.Collections.Generic;
using Game.Enums;
using Game.PlayerClass;
using UnityEngine;

namespace Game.Combat
{
    public class PlayerHitColliders : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            MeleeAttack(other);
        }
        private void MeleeAttack(Collider2D other)
        {
            if (other.gameObject.CompareTag(Tags.ENEMY_TAG)) 
            {
                Debug.Log("Attacking: " + other);
                float damage = GetComponentInParent<PlayerBaseStats>().GetStat(PlayerStats.BaseDamage);
                other.GetComponent<EnemyHealth>().TakeDamage(other.gameObject, damage);
            }
        }
    }
}
