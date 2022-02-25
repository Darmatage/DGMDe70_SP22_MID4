using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Combat
{
    public class PlayerHitColliders : MonoBehaviour
    {
        private float weaponDamage = 5f;

        private void OnTriggerEnter2D(Collider2D other)
        {
            EnemyAttack(other);
        }
        private void EnemyAttack(Collider2D other)
        {
            if (other.gameObject.CompareTag(Tags.EnemyTag)) 
            {
                Debug.Log("Attacking: " + other);

                other.GetComponent<EnemyHealth>().TakeDamage(weaponDamage);
            }
        }
    }
}
