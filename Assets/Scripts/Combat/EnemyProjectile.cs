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
        [SerializeField] float projectileSpeed = 1f;
        [SerializeField] float maxLifeTime = 5f;
        [SerializeField] bool isHoming = false;
        PlayerHealth target = null;
        float damage = 0f;
        
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
            //transform.position = Vector2.MoveTowards(transform.position, target.transform.position, step);
            
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
    }
    
}
