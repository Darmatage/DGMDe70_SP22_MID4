using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    struct RoomStruct {
        bool hasTop;
        GameObject exit_top;

        bool hasLeft;
        GameObject exit_left;

        bool hasBottom;
        GameObject exit_bottom;

        bool hasRight;
        GameObject exit_right;
    }
}
