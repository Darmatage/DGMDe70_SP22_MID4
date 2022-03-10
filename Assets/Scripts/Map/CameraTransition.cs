using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransition : MonoBehaviour
{
    public float delta;
    public Vector3 newPosition;

    void LateUpdate() {
        if (transform.position != newPosition) {
            transform.position =
                Vector3.Lerp(transform.position, newPosition, delta);
        }
    }
}
