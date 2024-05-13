using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IControllable))]
public class InputController : MonoBehaviour
{
    private IControllable _controllable;
    private GameInput _gameInput;

    private void Awake()
    {
        _gameInput = new GameInput();
        _gameInput.Enable();

        if (TryGetComponent(out IControllable controllable))
        {
            _controllable = controllable;
        }
    }

/*    private void AbilityOnPerformed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        throw new NotImplementedException();
    }

    private void OnEnable()
    {
        _gameInput.Gameplay.Ability.performed += AbilityOnPerformed;
    }

    private void OnDisable()
    {
        _gameInput.Gameplay.Ability.performed -= AbilityOnPerformed;
    }*/

    private void Update()
    {
        ReadMovement();
    }

    private void ReadMovement()
    {
        var direction = _gameInput.Gameplay.Movement.ReadValue<Vector3>();
        _controllable.Move(direction);
    }
}