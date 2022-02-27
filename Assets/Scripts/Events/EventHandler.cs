using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void PlayerInputDelegate(float inputX, float inputY, bool isWalking, bool isRunning, bool isIdle,
    bool isAttackingRight, bool isAttackingLeft, bool isAttackingUp, bool isAttackingDown,
    bool idleUp, bool idleDown, bool idleLeft, bool idleRight);

public class EventHandler
{
    // Movement Event
    public static event PlayerInputDelegate PlayerInputEvent;

    // Movement Event Call For Publishers
    public static void CallPlayerInputEvent(float inputX, float inputY, bool isWalking, bool isRunning, bool isIdle,
    bool isAttackingRight, bool isAttackingLeft, bool isAttackingUp, bool isAttackingDown,
    bool idleUp, bool idleDown, bool idleLeft, bool idleRight)
    {
        if (PlayerInputEvent != null)
            PlayerInputEvent(inputX, inputY,
                isWalking, isRunning, isIdle,
                isAttackingRight, isAttackingLeft, isAttackingUp, isAttackingDown,
                idleUp, idleDown, idleLeft, idleRight);
    }

}
