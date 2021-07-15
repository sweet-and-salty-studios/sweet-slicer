using UnityEngine;

namespace Ninja_Slicer.Core
{
    public class CharacterInput : MonoBehaviour
    {
        [SerializeField] private string mouseInputeAxis = "Mouse Y";

        public float MouseAxisY { get => Input.GetAxis(mouseInputeAxis); }
        public bool IsAttackKeyPressed { get => Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space); }
    }
}