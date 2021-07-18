using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private BodyPartController[] bodyPartControllers = default;

    private void Awake()
    {
        bodyPartControllers = GetComponentsInChildren<BodyPartController>();
    }

    public void Deactivate()
    {
        foreach(var bodyPartController in bodyPartControllers)
        {
            if(bodyPartController == null || bodyPartController.enabled == false) continue;

            bodyPartController.Collider.isTrigger = false;
            bodyPartController.Rigidbody.useGravity = true;
        }

        Destroy(this);
    }
}
