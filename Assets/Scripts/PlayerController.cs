using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb = default;
    private bool isRunning = default;
    [SerializeField] private float movementSpeed = 10;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private IEnumerator Start() 
    {
        yield return new WaitForSeconds(3);
        isRunning = true;
    }

    private void FixedUpdate()
    {
        if(isRunning == false) return;

        rb.velocity = Vector3.forward * movementSpeed * Time.deltaTime;
    }
}
