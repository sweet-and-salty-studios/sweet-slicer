using NinjaSlicer.Core;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [Space]
    [Header("Prefabs")]
    [SerializeField] private Weapon energyBladePrefab = default;
    [SerializeField] private GameObject targetIndicatorPrefab = default;

    public Weapon EnergyBladePrefab { get => energyBladePrefab; }
    public GameObject TargetIndicatorPrefab { get => targetIndicatorPrefab; }

    public static ResourceManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(Instance);
        }

        Instance = this;
    }
}
