using UnityEngine;

public class Sliceable : MonoBehaviour
{
    [Space]
    [Header("Settings")]
    [SerializeField] private bool isSolid = true;
    [SerializeField] private bool reverseWireTriangles = false;
    [SerializeField] private bool useGravity = false;
    [SerializeField] private bool shareVertices = false;
    [SerializeField] private bool smoothVertices = false;

    public bool IsSolid => isSolid;
    public bool ReverseWireTriangles => reverseWireTriangles;
    public bool UseGravity => useGravity;
    public bool ShareVertices => shareVertices;
    public bool SmoothVertices => smoothVertices;

    internal void AddSettings(bool isSolid, bool reverseWireTriangles, bool useGravity)
    {
        this.isSolid = isSolid;
        this.reverseWireTriangles = reverseWireTriangles;
        this.useGravity = useGravity;
    }
}
