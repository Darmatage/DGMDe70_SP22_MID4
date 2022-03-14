using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Combat
{
    /// <summary>
    /// Script to be put on projectile prefabs and then placed into the SO_EnemyClassStats ScriptableObject
    /// </summary>
    public class EnemyProjectile : MonoBehaviour
    {
        [SerializeField] float projectileSpeed = 5f;
        [SerializeField] float maxLifeTime = 2f;
        [SerializeField] bool isHoming = false;
        private PlayerHealth target = null;
        private float damage = 0f;
        
        private void Start()
        {
            transform.right = target.transform.position - transform.position;
        }
        private void Update()
        {
            if(target == null) return;

            float step = projectileSpeed * Time.deltaTime;

            if(isHoming && !target.IsDead())
            {
                transform.right = target.transform.position - transform.position;
            }
            transform.Translate(Vector3.right * step);
            
        }
        public void SetTarget(PlayerHealth target, float damage)
        {
            this.target = target;
            this.damage = damage;

            Destroy(gameObject, maxLifeTime);
        }
        private void OnTriggerEnter2D(Collider2D other) 
        {
            if(other.GetComponent<PlayerHealth>() != target) return;
            if(target.IsDead()) return;

            target.TakeDamage(damage);
            Destroy(gameObject);
        }
        private void OnCollisionEnter2D(Collision2D other)
        {
            Destroy(gameObject);
        }
    }
    
}
