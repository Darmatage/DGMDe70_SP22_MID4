using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Inventories
{
    [RequireComponent(typeof(Pickup))]
    public class PickupControl : MonoBehaviour
    {
        [SerializeField] float attrackDistance = 3f;
        [SerializeField] float moveSpeed = 3f;
        private Pickup pickup;
        private GameObject player;

        private void Awake()
        {
            pickup = GetComponent<Pickup>();
            player = GameObject.FindWithTag(Tags.PLAYER_TAG);
        }

        private void Update() 
        {
            if (InRangeOfPlayer())
            {
                MoveTo(player.transform.position);
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (pickup.InventoryHasSpace() && pickup.IsPlayerPickup(other.gameObject))
            {
                pickup.PickupItem();
            }
        }

        private bool InRangeOfPlayer()
        {  
            float distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);
            return distanceToPlayer < attrackDistance;
        }

        private void MoveTo(Vector2 destination)
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, destination, step);
        }

        //Called by Unity to visulize the guard area
        private void OnDrawGizmos() 
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, attrackDistance);
        }
    }
}