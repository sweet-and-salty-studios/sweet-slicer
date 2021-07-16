using System.Collections;
using UnityEngine;

namespace NinjaSlicer.Core
{
    public class CharacterEngine : MonoBehaviour
    {
        [Space]
        [Header("Movement")]
        [SerializeField] private float movementSpeedMultiplier = 10f;
        [SerializeField] [Range(0, 10)] private float maxSpeed = 10f;

        private bool isRunning = default;
        private Rigidbody rb = default;

        public float VelocityMagnitude { get => rb.velocity.magnitude; }

        private void Awake()
        {
            rb = GetComponentInChildren<Rigidbody>();
        }

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(3);
            isRunning = true;
        }

        private void FixedUpdate()
        {
            if(isRunning == false)
                return;

            rb.AddForce(Vector3.forward * movementSpeedMultiplier * Time.deltaTime, ForceMode.Acceleration);
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }
    }
}