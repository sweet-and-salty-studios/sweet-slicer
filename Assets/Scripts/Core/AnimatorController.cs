using UnityEngine;

namespace Ninja_Slicer.Core
{
    public class AnimatorController : MonoBehaviour
    {
        private Animator animator = default;

        private int movementSpeedHash = default;
        private int attackHash = default;

        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
        }

        private void Start()
        {
            movementSpeedHash = Animator.StringToHash("MovementSpeed");
            attackHash = Animator.StringToHash("Attack");
        }

        public void SetAttackTrigger()
        {
            animator.SetTrigger(attackHash);
        }

        public void SetMovementSpeed(float speed)
        {
            animator.SetFloat(movementSpeedHash, Mathf.Clamp01(speed), 0.1f, Time.deltaTime);
        }
    }
}