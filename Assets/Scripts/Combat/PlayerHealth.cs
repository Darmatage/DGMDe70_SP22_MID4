using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Combat
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] float timeInvincible = 2.0f;
        [SerializeField] float playerHealth = 20f;
        private float playerMaxHealth = 20f;
        private bool isDead = false;
        private bool isInvincible;
        private float invincibleTimer;
        private void Update()
        {
            if (isInvincible)
            {
                invincibleTimer -= Time.deltaTime;
                if (invincibleTimer < 0)
                {
                    isInvincible = false;
                }
            }
        }
        public float GetHealthPoints()
        {
            return playerHealth;
        }
        public float GetMaxHealthPoints()
        {
            return playerMaxHealth;
        }
        public bool IsDead()
        {
            return isDead;
        }
        public void ChangeHealth(float amount)
        {
            if (amount < 0)
            {
                if (isInvincible) {return;}

                isInvincible = true;
                invincibleTimer = timeInvincible;
            }

            playerHealth = Mathf.Clamp(playerHealth + amount, 0, playerMaxHealth);
        }

        public void TakeDamage(float damage)
        {
            playerHealth = Mathf.Max(playerHealth - damage, 0);
            if(playerHealth == 0)
            {
                Die();
            }
        }
        public float GetPercentage()
        {
            return (playerHealth / playerMaxHealth) * 100;
        }
        private void Die()
        {
            if (isDead) return;

            isDead = true;
        }
    }   
}
