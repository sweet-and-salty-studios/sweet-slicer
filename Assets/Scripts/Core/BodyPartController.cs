using UnityEngine;

public class BodyPartController : MonoBehaviour
{
    public Rigidbody Rigidbody { get; private set; } = default;
    public Collider Collider { get; private set; } = default;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Collider = GetComponent<Collider>();
    }
}
