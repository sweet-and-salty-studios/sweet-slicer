using UnityEngine;
using Utilities.MeshSlicing;

namespace NinjaSlicer.Core
{
    public class EnergyBlade : Weapon
    {
        private LineRenderer lineRenderer = default;
        [SerializeField] private Vector3 energyStartPosition = default;
        [SerializeField] private Vector3 energyEndPosition = default;
        [SerializeField] private Material crossSectionMaterial = default;

        private void Awake()
        {
            lineRenderer = GetComponentInChildren<LineRenderer>();
        }

        private void Start()
        {
            lineRenderer.positionCount = 2;
            lineRenderer.SetPositions(new Vector3[] { energyStartPosition, energyEndPosition });
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(energyStartPosition, 0.05f);
            Gizmos.DrawWireSphere(energyEndPosition, 0.05f);
        }

        public override void Attack()
        {
            //var sliceableColliders = Physics.OverlapBox(effectAreaCollider.bounds.center, effectAreaCollider.bounds.extents, Quaternion.identity, hitLayers);

            //foreach(var sliceableCollider in sliceableColliders)
            //{
            //    var sliceableGameObject = sliceableCollider.gameObject;
            //    var sliceableParent = sliceableGameObject.transform.parent;

            //    Material crossSectionMaterial = null;

            //    var slicableHull = sliceableGameObject.Slice(transform.position, transform.up, crossSectionMaterial);

            //    if(slicableHull == null)
            //        continue;

            //    var upperObject = slicableHull.CreateUpperHull(sliceableGameObject, null);
            //    var lowerObject = slicableHull.CreateLowerHull(sliceableGameObject, null);

            //    upperObject.transform.SetParent(sliceableParent, false);
            //    lowerObject.transform.SetParent(sliceableParent, false);

            //    upperObject.layer = sliceableGameObject.layer;
            //    lowerObject.layer = sliceableGameObject.layer;

            //    upperObject.AddComponent<MeshCollider>().convex = true;
            //    lowerObject.AddComponent<MeshCollider>().convex = true;

            //    upperObject.AddComponent<Rigidbody>().AddForce((Vector3.right + Vector3.up) * 4, ForceMode.Impulse);
            //    lowerObject.AddComponent<Rigidbody>().AddForce((Vector3.left + Vector3.up) * 4, ForceMode.Impulse);

            //    sliceableGameObject.SetActive(false);
            //}
        }
    
        private void OnTriggerExit(Collider other)
        {
            if(other.gameObject.layer == 8)
            {
                var sliceableGameObject = other.gameObject;
                var sliceableParent = sliceableGameObject.transform.parent;

                var slicableHull = sliceableGameObject.Slice(transform.position, transform.up, crossSectionMaterial);

                if(slicableHull == null)
                    return;

                var upperObject = slicableHull.CreateUpperHull(sliceableGameObject, crossSectionMaterial);
                var lowerObject = slicableHull.CreateLowerHull(sliceableGameObject, crossSectionMaterial);

                upperObject.transform.SetParent(sliceableParent, false);
                lowerObject.transform.SetParent(sliceableParent, false);

                upperObject.layer = sliceableGameObject.layer;
                lowerObject.layer = sliceableGameObject.layer;

                upperObject.AddComponent<MeshCollider>().convex = true;
                lowerObject.AddComponent<MeshCollider>().convex = true;

                var rigidbody = upperObject.AddComponent<Rigidbody>();
                rigidbody.AddForce((Vector3.right + Vector3.up) * 4, ForceMode.Impulse);
                rigidbody.interpolation = RigidbodyInterpolation.Interpolate;

                rigidbody = lowerObject.AddComponent<Rigidbody>();
                rigidbody.AddForce((Vector3.left + Vector3.up) * 4, ForceMode.Impulse);
                rigidbody.interpolation = RigidbodyInterpolation.Interpolate;

                sliceableGameObject.SetActive(false);

                Destroy(sliceableGameObject, 1);
                Destroy(upperObject, 4);
                Destroy(lowerObject, 4);
            }
        }
    }
}