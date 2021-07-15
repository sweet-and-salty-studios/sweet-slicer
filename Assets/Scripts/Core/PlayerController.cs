using UnityEngine;

namespace Ninja_Slicer.Core
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Weapon weapon = default;

        private CharacterEngine characterEngine = default;
        private CharacterInput characterInput = default;
        private AnimatorController animatorController = default;

        private void Awake()
        {
            characterEngine = GetComponentInChildren<CharacterEngine>();
            characterInput = GetComponentInChildren<CharacterInput>();
            animatorController = GetComponentInChildren<AnimatorController>();
        }

        private void Update()
        {
            if(animatorController == null || characterInput == null)
                return;

            if(characterInput.IsAttackKeyPressed)
            {
                animatorController.SetAttackTrigger();
            }

            animatorController.SetMovementSpeed(characterEngine.VelocityMagnitude);
        }
    }
}