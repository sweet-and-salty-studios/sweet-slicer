using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target = default;
    [SerializeField] private float movementSpeedMultiplier = default;

    private Camera mainCamera = default;
    private Vector3 offset = default;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();    
    }

    private void Start()
    {
        offset = transform.position - target.position;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.smoothDeltaTime * movementSpeedMultiplier);
    }
}
