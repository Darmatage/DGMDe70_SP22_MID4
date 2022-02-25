using UnityEngine;
using Game.Core;

namespace Game.Animation
{
    public class MovementAnimationParameterControl : MonoBehaviour
    {
        private Animator animator;

        // Use this for initialisation
        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            EventHandler.MovementEvent += SetAnimationParameters;
        }

        private void OnDisable()
        {
            EventHandler.MovementEvent -= SetAnimationParameters;
        }

        private void SetAnimationParameters(float xInput, float yInput, bool isWalking, bool isRunning, bool isIdle,
            bool isAttackingRight, bool isAttackingLeft, bool isAttackingUp, bool isAttackingDown,
            bool idleUp, bool idleDown, bool idleLeft, bool idleRight)
        {
            animator.SetFloat(Settings.xInput, xInput);
            animator.SetFloat(Settings.yInput, yInput);
            animator.SetBool(Settings.isWalking, isWalking);
            animator.SetBool(Settings.isRunning, isRunning);

            if (isAttackingRight)
                animator.SetTrigger(Settings.isAttackingRight);
            if (isAttackingLeft)
                animator.SetTrigger(Settings.isAttackingLeft);
            if (isAttackingUp)
                animator.SetTrigger(Settings.isAttackingUp);
            if (isAttackingDown)
                animator.SetTrigger(Settings.isAttackingDown);

            if (idleUp)
                animator.SetTrigger(Settings.idleUp);
            if (idleDown)
                animator.SetTrigger(Settings.idleDown);
            if (idleLeft)
                animator.SetTrigger(Settings.idleLeft);
            if (idleRight)
                animator.SetTrigger(Settings.idleRight);
        }
    }
}
