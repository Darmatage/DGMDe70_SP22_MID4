using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void MovementDelegate(float inputX, float inputY, bool isWalking, bool isRunning, bool isIdle,
    bool isAttackingRight, bool isAttackingLeft, bool isAttackingUp, bool isAttackingDown,
    bool idleUp, bool idleDown, bool idleLeft, bool idleRight);

public class EventHandler
{
    // Movement Event
    public static event MovementDelegate MovementEvent;

    // Movement Event Call For Publishers
    public static void CallMovementEvent(float inputX, float inputY, bool isWalking, bool isRunning, bool isIdle,
    bool isAttackingRight, bool isAttackingLeft, bool isAttackingUp, bool isAttackingDown,
    bool idleUp, bool idleDown, bool idleLeft, bool idleRight)
    {
        if (MovementEvent != null)
            MovementEvent(inputX, inputY,
                isWalking, isRunning, isIdle,
                isAttackingRight, isAttackingLeft, isAttackingUp, isAttackingDown,
                idleUp, idleDown, idleLeft, idleRight);
    }

}
