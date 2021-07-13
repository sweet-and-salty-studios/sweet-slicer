using UnityEngine;

public class ProductionLine : MonoBehaviour
{
    [SerializeField] private Vector3 force = default;
    
    private void OnCollisionEnter(Collision collision)
    {
        var rb = collision.gameObject.GetComponent<Rigidbody>();
        if(rb == null) return;

        rb.AddForce(force, ForceMode.Force);
    }

    private void OnCollisionExit(Collision collision)
    {
        
    }
}
