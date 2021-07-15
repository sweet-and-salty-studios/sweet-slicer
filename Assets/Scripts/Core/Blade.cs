using Utilities;
using UnityEngine;

namespace Ninja_Slicer.Core
{
    public class Blade : Weapon
    {
        public override void Attack()
        {
            var sliceableColliders = Physics.OverlapBox(effectAreaCollider.bounds.center, effectAreaCollider.bounds.extents, Quaternion.identity, hitLayers);

            foreach(var sliceableCollider in sliceableColliders)
            {
                var sliceableGameObject = sliceableCollider.gameObject;
                var sliceableParent = sliceableGameObject.transform.parent;

                Material crossSectionMaterial = null;

                var slicableHull = sliceableGameObject.Slice(transform.position, transform.up, crossSectionMaterial);

                if(slicableHull == null)
                    continue;

                var upperObject = slicableHull.CreateUpperHull(sliceableGameObject, null);
                var lowerObject = slicableHull.CreateLowerHull(sliceableGameObject, null);

                upperObject.transform.SetParent(sliceableParent, false);
                lowerObject.transform.SetParent(sliceableParent, false);

                upperObject.layer = sliceableGameObject.layer;
                lowerObject.layer = sliceableGameObject.layer;

                upperObject.AddComponent<MeshCollider>().convex = true;
                lowerObject.AddComponent<MeshCollider>().convex = true;

                upperObject.AddComponent<Rigidbody>().AddForce((Vector3.right + Vector3.up) * 4, ForceMode.Impulse);
                lowerObject.AddComponent<Rigidbody>().AddForce((Vector3.left + Vector3.up) * 4, ForceMode.Impulse);

                sliceableGameObject.SetActive(false);
            }
        }

        public override void Rotation(float rotationSpeed)
        {
            transform.eulerAngles += Vector3.forward * rotationSpeed * rotationMultiplier;
        }
    }
}