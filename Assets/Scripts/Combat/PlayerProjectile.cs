using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Combat
{
    /// <summary>
    /// Script to be put on projectile prefabs and then placed into the SO_EnemyClassStats ScriptableObject
    /// </summary>
    public class PlayerProjectile : MonoBehaviour
    {
        [SerializeField] float projectileSpeed = 1f;
        [SerializeField] float maxLifeTime = 5f;
        private Vector3 launchDirection = new Vector3(0,0,0);
        private float damage = 0f;
        
        private void Start()
        {
            transform.right = launchDirection;
            //transform.right = target.transform.position - transform.position;
        }
        private void Update()
        {
            //if(launchDirection == null) return;

            float step = projectileSpeed * Time.deltaTime;

            // if(isHoming && !target.IsDead())
            // {
            //     transform.right = target.transform.position - transform.position;
            // }
            transform.Translate(Vector3.right * step);
            
        }
        public void SetTarget(Vector3 launchDirection, float damage)
        {
            this.launchDirection = launchDirection;
            this.damage = damage;

            Destroy(gameObject, maxLifeTime);
        }
        private void OnTriggerEnter2D(Collider2D other) 
        {
            if(other.GetComponent<EnemyHealth>() == null) return;
            // if(target.IsDead()) return;

            other.GetComponent<EnemyHealth>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
    
}
