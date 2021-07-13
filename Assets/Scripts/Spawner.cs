using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Sliceable sliceablePrefab = default;
    [SerializeField] private float spawnTime = default;
    [SerializeField] private float repeatRate = default;

    private void Start()
    {
        InvokeRepeating("Spawn", spawnTime, repeatRate);
    }

    private void Spawn() 
    {
        Instantiate(sliceablePrefab, transform.position, Quaternion.identity);
    }
}
