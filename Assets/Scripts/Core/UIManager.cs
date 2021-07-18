using UnityEngine;

public class UIManager : MonoBehaviour
{
    public UIManager Instance { get; private set; } = default;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(Instance);
        }

        Instance = this;
    }
}
