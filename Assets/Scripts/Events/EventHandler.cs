using System;
using System.Collections;
using System.Collections.Generic;
using Game.Enums;
using UnityEngine;

public delegate void PlayerInputDelegate(float inputX, float inputY, bool isWalking, bool isRunning, bool isIdle, bool isMakingAttack,
    bool isAttackingRight, bool isAttackingLeft, bool isAttackingUp, bool isAttackingDown,
    bool idleUp, bool idleDown, bool idleLeft, bool idleRight);

public delegate void PlayerTransformStateDelegate(PlayerTransformState playerTransformState);

public class EventHandler
{
    // Movement Event
    public static event PlayerInputDelegate PlayerInputEvent;

    // Movement Event Call For Publishers
    public static void CallPlayerInputEvent(float inputX, float inputY, bool isWalking, bool isRunning, bool isIdle, bool isMakingAttack,
    bool isAttackingRight, bool isAttackingLeft, bool isAttackingUp, bool isAttackingDown,
    bool idleUp, bool idleDown, bool idleLeft, bool idleRight)
    {
        if (PlayerInputEvent != null)
            PlayerInputEvent(inputX, inputY,
                isWalking, isRunning, isIdle, isMakingAttack,
                isAttackingRight, isAttackingLeft, isAttackingUp, isAttackingDown,
                idleUp, idleDown, idleLeft, idleRight);
    }

    // Pause game and controls while in UI
    public static event Action<bool> ActiveGameUI;
    public static void CallActiveGameUI(bool isGamePaused)
    {
        if (ActiveGameUI != null)
        {
            ActiveGameUI(isGamePaused);
        }
    }

    // Pause game and controls while in UI
    public static event Action<bool> InteractActionEvent;
    public static void CallInteractActionEvent(bool isKeyPressed)
    {
        if (InteractActionEvent != null)
        {
            InteractActionEvent(isKeyPressed);
        }
    }

    // Transform Player
    public static event Action TransformEvent;
    public static void CallTransformEvent()
    {
        if (TransformEvent != null)
        {
            TransformEvent();
        }
    }

    public static event PlayerTransformStateDelegate PlayerTransformStateEvent;
    public static void CallPlayerTransformStateEvent(PlayerTransformState playerTransformState)
    {
        if (PlayerTransformStateEvent != null)
        {
            PlayerTransformStateEvent(playerTransformState);
        }
    }


    // Transform Player
    public static event Action QuitPlayGameEvent;
    public static void CallQuitPlayGameEvent()
    {
        if (QuitPlayGameEvent != null)
        {
            QuitPlayGameEvent();
        }
    }

}
