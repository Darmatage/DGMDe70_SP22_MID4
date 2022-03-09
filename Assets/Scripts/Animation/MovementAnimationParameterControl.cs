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
            EventHandler.PlayerInputEvent += SetAnimationParameters;
            EventHandler.PlayerAttackEvent += SetRangeAttackAnimationParameters;
        }

        private void OnDisable()
        {
            EventHandler.PlayerInputEvent -= SetAnimationParameters;
            EventHandler.PlayerAttackEvent -= SetRangeAttackAnimationParameters;
        }

        private void SetAnimationParameters(float xInput, float yInput, bool isWalking, bool isRunning, bool isIdle, bool isMakingAttack,
            bool isAttackingUp, bool isAttackingRight, bool isAttackingDown, bool isAttackingLeft,
            bool idleUp, bool idleDown, bool idleLeft, bool idleRight)
        {
            animator.SetFloat(Settings.xInput, xInput);
            animator.SetFloat(Settings.yInput, yInput);
            animator.SetBool(Settings.isWalking, isWalking);
            animator.SetBool(Settings.isRunning, isRunning);

            if (idleUp)
                animator.SetTrigger(Settings.idleUp);
            if (idleDown)
                animator.SetTrigger(Settings.idleDown);
            if (idleLeft)
                animator.SetTrigger(Settings.idleLeft);
            if (idleRight)
                animator.SetTrigger(Settings.idleRight);
        }

        private void SetRangeAttackAnimationParameters(bool isRangeAttackingUp, bool isRangeAttackingRight, bool isRangeAttackingDown, bool isRangeAttackingLeft)
        {
            if (isRangeAttackingUp)
                animator.SetTrigger(Settings.isAttackingUp);
            if (isRangeAttackingRight)
                animator.SetTrigger(Settings.isAttackingRight);
            if (isRangeAttackingDown)
                animator.SetTrigger(Settings.isAttackingDown);
            if (isRangeAttackingLeft)
                animator.SetTrigger(Settings.isAttackingLeft);
        }
    }
}
