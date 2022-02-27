using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int rows;
    public int columns;
    public GameObject root;
    public GameObject roomPrefab;

    void Awake() {
        // GameObject newRoom;
        // Vector3 currentPosition = transform.position;

        // for (int i = 0; i < rows; ++i) {
        //     for (int j = 0; j < columns; ++j) {
        //         currentPosition.x = transform.position.x + (j * 18);
        //         currentPosition.y = transform.position.y + (i * 10);

        //         newRoom = Instantiate(roomPrefab, currentPosition, Quaternion.identity);
        //         newRoom.transform.SetParent(gameObject.transform);
        //     }
        // }
    }
}
