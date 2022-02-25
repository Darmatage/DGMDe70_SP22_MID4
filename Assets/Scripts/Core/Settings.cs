using UnityEngine;

namespace Game.Core
{
    public static class Settings
    {
        // Player Animation Parameters
        public static int xInput;
        public static int yInput;
        public static int isWalking;
        public static int isRunning;
        public static int isAttackingRight;
        public static int isAttackingLeft;
        public static int isAttackingUp;
        public static int isAttackingDown;

        // Shared Animation Parameters
        public static int idleUp;
        public static int idleDown;
        public static int idleLeft;
        public static int idleRight;

        // static constructor
        static Settings()
        {
            // Player Animation Parameters
            xInput = Animator.StringToHash("xInput");
            yInput = Animator.StringToHash("yInput");
            isWalking = Animator.StringToHash("isWalking");
            isRunning = Animator.StringToHash("isRunning");
            isAttackingRight = Animator.StringToHash("isAttackingRight");
            isAttackingLeft = Animator.StringToHash("isAttackingLeft");
            isAttackingUp = Animator.StringToHash("isAttackingUp");
            isAttackingDown = Animator.StringToHash("isAttackingDown");

            // Shared Animation parameters
            idleUp = Animator.StringToHash("idleUp");
            idleDown = Animator.StringToHash("idleDown");
            idleLeft = Animator.StringToHash("idleLeft");
            idleRight = Animator.StringToHash("idleRight");
        }

    }

    
}