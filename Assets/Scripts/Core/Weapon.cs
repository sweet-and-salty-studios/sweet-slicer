using UnityEngine;

namespace Ninja_Slicer.Core
{
    public abstract class Weapon : MonoBehaviour
    {
        [Space]
        [Header("Settings")]
        [SerializeField] protected LayerMask hitLayers = default;
        [SerializeField] protected float rotationMultiplier = 5f;

        protected Collider effectAreaCollider = default;

        private void Awake()
        {
            effectAreaCollider = GetComponent<Collider>();
        }

        public abstract void Rotation(float roattionSpeed);
        public abstract void Attack();
    }

}