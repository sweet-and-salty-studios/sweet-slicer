using UnityEngine;

namespace NinjaSlicer.Core
{
    public class AnimatorController : MonoBehaviour
    {
        private Animator animator = default;

        private int movementSpeedHash = default;
        private int attackingHash = default;

        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
        }

        private void Start()
        {
            movementSpeedHash = Animator.StringToHash("MovementSpeed");
            attackingHash = Animator.StringToHash("IsAttacking");
        }

        public void IsAttacking(bool isAttacking) 
        {
            animator.SetBool(attackingHash, isAttacking);
        }

        public void SetMovementSpeed(float speed)
        {
            animator.SetFloat(movementSpeedHash, Mathf.Clamp01(speed), 0.1f, Time.deltaTime);
        }
    }
}