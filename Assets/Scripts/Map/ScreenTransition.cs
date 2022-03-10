using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenTransition : MonoBehaviour
{
    public Vector3 cameraShift;
    public Vector3 playerShift;
    private CameraTransition cameraTransition;

    void Start() {
        cameraTransition = Camera.main.GetComponent<CameraTransition>();
        cameraTransition.newPosition = Camera.main.transform.position;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            // Vector3 newPosition = Camera.main.transform.position;
            // newPosition += cameraShift;

            other.transform.position += playerShift;
            cameraTransition.newPosition += cameraShift;
        }
    }
}
