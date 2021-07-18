using UnityEngine;

namespace NinjaSlicer.Core
{
    public class CharacterInput : MonoBehaviour
    {
        [SerializeField] private string mouseInputeAxis = "Mouse Y";

        public float MouseAxisY { get => Input.GetAxis(mouseInputeAxis); }
        public bool IsAttackKeyDown { get => Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space); }
        public bool IsAttackKeyHeld { get => Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space); }
        public bool IsAttackKeyUp { get => Input.GetMouseButtonUp(0) || Input.GetKeyUp(KeyCode.Space); }

    }
}