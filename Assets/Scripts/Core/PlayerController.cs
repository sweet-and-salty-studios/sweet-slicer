using System.Collections;
using UnityEngine;

namespace Ninja_Slicer.Core
{
    public class PlayerController : MonoBehaviour
    {
        [Space]
        [Header("Movement")]
        private Rigidbody rb = default;
        private bool isRunning = default;
        [SerializeField] private float movementSpeedMultiplier = 10;
        [SerializeField] [Range(0, 10)] private float maxSpeed = 5f;

        [Space]
        [Header("Input")]
        [SerializeField] private string mouseInputeAxis = "Mouse Y";
        [SerializeField] private Weapon weapon = default;

        private Animator animator = default;
        private int movementSpeedHash = default;

        public bool IsAttackKeyPressed { get => Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space); }

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
        }

        private IEnumerator Start()
        {
            movementSpeedHash = Animator.StringToHash("MovementSpeed");

            yield return new WaitForSeconds(3);
            isRunning = true;
        }

        private void Update()
        {
            if(weapon != null)
            {
                weapon.Rotation(-Input.GetAxis(mouseInputeAxis));

                if(IsAttackKeyPressed)
                {
                    weapon.Attack();
                }
            }

            if(animator != null)
            {
                animator.SetFloat(movementSpeedHash, Mathf.Clamp01(rb.velocity.magnitude));
            }
        }

        private void FixedUpdate()
        {
            if(isRunning == false)
                return;

            rb.AddForce(Vector3.forward, ForceMode.Acceleration);

            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
            Debug.Log(rb.velocity.magnitude);
        }
    }

}