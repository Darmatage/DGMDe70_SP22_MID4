// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class PlayerMoveTest : MonoBehaviour
// {
//     public Rigidbody2D rb;
//     public float moveSpeed = 5f;
//     public Vector2 movement;

//     // Start is called before the first frame update
//     void Start() {
//         rb = GetComponent<Rigidbody2D>();        
//     }

//     // Update is called once per frame
//     void FixedUpdate() {
//        movement.x = Input.GetAxisRaw("Horizontal");
//        movement.y = Input.GetAxisRaw("Vertical");
//        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime); 
//     }
// }
