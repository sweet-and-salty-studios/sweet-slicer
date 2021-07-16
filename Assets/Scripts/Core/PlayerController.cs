﻿using UnityEngine;

namespace NinjaSlicer.Core
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Weapon startingWeaponPrefab = default;
        [SerializeField] private Weapon currentWeapon = default;
        [SerializeField] private Transform weaponPivot = default;

        private CharacterEngine characterEngine = default;
        private CharacterInput characterInput = default;
        private AnimatorController animatorController = default;

        private void Awake()
        {
            characterEngine = GetComponentInChildren<CharacterEngine>();
            characterInput = GetComponentInChildren<CharacterInput>();
            animatorController = GetComponentInChildren<AnimatorController>();
        }

        private void Start()
        {
            currentWeapon = Instantiate(startingWeaponPrefab, weaponPivot);
        }

        private void Update()
        {
            if(animatorController == null || characterInput == null)
                return;

            if(characterInput.IsAttackKeyDown)
            {
                Time.timeScale = 0.25f;
                animatorController.IsAttacking(true);
            }

            if(characterInput.IsAttackKeyUp)
            {
                Time.timeScale = 1f;
                animatorController.IsAttacking(false);
            }

            animatorController.SetMovementSpeed(characterEngine.VelocityMagnitude);
        }
    }
}