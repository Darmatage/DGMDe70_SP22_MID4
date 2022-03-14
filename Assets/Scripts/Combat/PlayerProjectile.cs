using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Combat
{
    public class PlayerProjectile : MonoBehaviour
    {
        [SerializeField] float projectileSpeed = 1f;
        [SerializeField] float maxLifeTime = 5f;
        private Vector3 launchDirection = new Vector3(0,0,0);
        private float damage = 0f;
        
        private void Start()
        {
            transform.right = launchDirection;
        }
        private void Update()
        {
            float step = projectileSpeed * Time.deltaTime;
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
            if(other.GetComponent<IHealth>() == null) return;
            other.GetComponent<IHealth>().TakeDamage(GameObject.FindWithTag(Tags.PLAYER_TAG), damage);
            Destroy(gameObject);
        }
    }
    
}
