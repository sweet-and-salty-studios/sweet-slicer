using Assets.Scripts;
using UnityEngine;

public class Blade : MonoBehaviour
{
    [SerializeField] private Transform cutSensor1 = null;
    [SerializeField] private Transform cutSensor2 = null;
    [SerializeField] private float forceAppliedToCut = 3f;
    [SerializeField] private Rigidbody rb = default;
    
    private Vector3 triggerEnterTipPosition;
    private Vector3 triggerEnterBasePosition;
    private Vector3 triggerExitTipPosition;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            rb.AddForce(Vector3.down * 100, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        triggerEnterTipPosition = cutSensor1.position;
        triggerEnterBasePosition = cutSensor2.position;
    }

    private void OnTriggerExit(Collider other)
    {
        triggerExitTipPosition = cutSensor1.transform.position;

        //Create a triangle between the tip and base so that we can get the normal
        var side1 = triggerExitTipPosition - triggerEnterTipPosition;
        var side2 = triggerExitTipPosition - triggerEnterBasePosition;

        //Get the point perpendicular to the triangle above which is the normal
        //https://docs.unity3d.com/Manual/ComputingNormalPerpendicularVector.html
        var normal = Vector3.Cross(side1, side2).normalized;

        //Transform the normal so that it is aligned with the object we are slicing's transform.
        var transformedNormal = ((Vector3)(other.gameObject.transform.localToWorldMatrix.transpose * normal)).normalized;

        //Get the enter position relative to the object we're cutting's local transform
        var transformedStartingPoint = other.gameObject.transform.InverseTransformPoint(triggerEnterTipPosition);

        var plane = new Plane();

        plane.SetNormalAndPosition(
                transformedNormal,
                transformedStartingPoint);

        var direction = Vector3.Dot(Vector3.up, transformedNormal);

        //Flip the plane so that we always know which side the positive mesh is on
        if (direction < 0)
        {
            plane = plane.flipped;
        }

        var slices = Slicer.Slice(plane, other.gameObject);
        Destroy(other.gameObject);

        var rigidbody = slices[1].GetComponent<Rigidbody>();
        var newNormal = transformedNormal + Vector3.up * forceAppliedToCut;
        rigidbody.AddForce(newNormal, ForceMode.Impulse);
    }
}
