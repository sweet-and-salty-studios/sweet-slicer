using UnityEngine;
using Utilities.MeshSlicing;

namespace NinjaSlicer.Core
{
    public class EnergyBlade : Weapon
    {
        [SerializeField] private Transform bladeStartPoint = default;
        [SerializeField] private Transform bladeEndPoint = default;
        [SerializeField] private Material crossSectionMaterial = default;
        private LineRenderer lineRenderer = default;

        private Vector3 triggerEnterStartPosition = default;
        private Vector3 triggerEnterEndPosition = default;
        private Vector3 triggerExitEndPosition = default;
        private Vector3 sliceCenter = default;
        private UnityEngine.Plane plane = default;

        private void Awake()
        {
            lineRenderer = GetComponentInChildren<LineRenderer>();
        }

        private void Start()
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPositions(new Vector3[] { bladeStartPoint.localPosition, bladeEndPoint.localPosition });
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(triggerEnterStartPosition, 0.05f);

            Gizmos.color = Color.green;
            Gizmos.DrawSphere(triggerEnterEndPosition, 0.05f);

            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(triggerExitEndPosition, 0.05f);

            Gizmos.color = Color.black;
            Gizmos.DrawSphere(sliceCenter, 0.05f);

            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(sliceCenter, plane.normal * 100);
        }

        private void OnTriggerEnter(Collider other)
        {
            SliceStart();
        }

        private void SliceStart()
        {
            triggerEnterStartPosition = bladeStartPoint.position;
            triggerEnterEndPosition = bladeEndPoint.position;
        }

        private void OnTriggerExit(Collider other)
        {
            switch(other.gameObject.layer)
            {
                case 8:
                case 9:
                    SliceEnd();
                    HandleSlicedObject(other.gameObject);
                    break;
                default:
                    break;
            }
        }

        private void HandleSlicedObject(GameObject sliceableGameObject)
        {
            var sliceableHull = sliceableGameObject.Slice(sliceCenter, plane.normal, crossSectionMaterial);
            if(sliceableHull == null)
                return;

            var upperObject = sliceableHull.CreateUpperHull(sliceableGameObject, crossSectionMaterial);
            var lowerObject = sliceableHull.CreateLowerHull(sliceableGameObject, crossSectionMaterial);

            var sliceableParent = sliceableGameObject.transform.parent;
            upperObject.transform.SetParent(sliceableParent, false);
            upperObject.layer = sliceableGameObject.layer;
            var rigidbody = upperObject.AddComponent<Rigidbody>();
            rigidbody.AddForce(Vector3.right + Vector3.up, ForceMode.Impulse);
            rigidbody.interpolation = RigidbodyInterpolation.Interpolate;

            lowerObject.transform.SetParent(sliceableParent, false);
            lowerObject.layer = sliceableGameObject.layer;
            rigidbody = lowerObject.AddComponent<Rigidbody>();
            rigidbody.AddForce(Vector3.left + Vector3.up, ForceMode.Impulse);
            rigidbody.interpolation = RigidbodyInterpolation.Interpolate;

            sliceableGameObject.SetActive(false);
            Destroy(sliceableGameObject, 1);
            Destroy(upperObject, 4);
            Destroy(lowerObject, 4);
        }

        private void SliceEnd()
        {
            triggerExitEndPosition = bladeEndPoint.position;
            plane.Set3Points(triggerEnterStartPosition, triggerEnterEndPosition, triggerExitEndPosition);
            sliceCenter = triggerEnterStartPosition + triggerEnterEndPosition + triggerExitEndPosition;
            sliceCenter /= 3;
        }
    }
}