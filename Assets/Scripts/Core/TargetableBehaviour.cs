using UnityEngine;

namespace NinjaSlicer.Core
{
    public class TargetableBehaviour : MonoBehaviour, ITargetable
    {
        private GameObject targetIndicator = default;

        public void Activate()
        {
            targetIndicator = Instantiate(ResourceManager.Instance.TargetIndicatorPrefab, transform);
        }

        public void Deactivate()
        {
            if(targetIndicator = null) targetIndicator.SetActive(false);
        }
    }
}