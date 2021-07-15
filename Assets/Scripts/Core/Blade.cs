using Utilities;
using UnityEngine;

public class Blade : MonoBehaviour
{
    [SerializeField] private float sensitivity = 5f;
    [SerializeField] private string mouseInputeAxis = "Mouse Y";
    [SerializeField] private LayerMask slicableLayers = default;

    private Collider slideCollider = default;

    private void Awake()
    {
        slideCollider = GetComponent<Collider>();    
    }

    private void Update()
    {
        transform.eulerAngles += Vector3.forward * -Input.GetAxis(mouseInputeAxis) * sensitivity;

        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            TrySliceGameObjects();
        }
    }

    private void TrySliceGameObjects()
    {
        var sliceableColliders = Physics.OverlapBox(slideCollider.bounds.center, slideCollider.bounds.extents, Quaternion.identity, slicableLayers);

        foreach(var sliceableCollider in sliceableColliders)
        {
            var sliceableGameObject = sliceableCollider.gameObject;
            var sliceableParent = sliceableGameObject.transform.parent;

            var slicableHull = SliceGameObject(sliceableGameObject, null);
            if(slicableHull == null) continue;

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

    public SlicedHull SliceGameObject(GameObject gameObject, Material crossSectionMaterial = null) 
    {
        return gameObject.Slice(transform.position, transform.up, crossSectionMaterial);
    }
}
