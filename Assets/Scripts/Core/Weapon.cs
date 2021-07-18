using UnityEngine;

namespace NinjaSlicer.Core
{
    public abstract class Weapon : MonoBehaviour
    {
        [Space]
        [Header("Settings")]
        [SerializeField] protected LayerMask hitLayers = default;

        protected Collider effectAreaCollider = default;

        private void Awake()
        {
            effectAreaCollider = GetComponent<Collider>();
        }
    }
}