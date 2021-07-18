using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NinjaSlicer.Core
{

    public class CharacterEngine : MonoBehaviour
    {
        [Space]
        [Header("Movement")]
        [SerializeField] private float movementSpeedMultiplier = 10f;
        [SerializeField] [Range(0, 10)] private float maxSpeed = 10f;
        [SerializeField] private LayerMask tagableLayers = default;

        private readonly Queue<ITargetable> targets = new Queue<ITargetable>();

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

        private void OnTriggerEnter(Collider other)
        {
            if((tagableLayers & (1 << other.gameObject.layer)) != 0)
            {
                var targetable = GetComponent<ITargetable>();
                if(targetable == null) return;

                targetable.Activate();
                targets.Enqueue(targetable);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if((tagableLayers & (1 << other.gameObject.layer)) != 0)
            {
                if(targets.Count == 0) return;
                
                var targetable = targets.Dequeue();
                targetable.Deactivate();
            }
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