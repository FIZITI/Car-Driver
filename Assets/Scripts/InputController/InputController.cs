using System;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private List<IControllable> _controllables = new List<IControllable>();
    private GameInput _gameInput;
    private Vector3 _currentDirection;
    private IControllable _controllableFromCurrentObject;
    private IControllable[] _controllablesFromChildren;
    private const float _smoothing = 7f;

    private void Awake()
    {
        _gameInput = new GameInput();
        _gameInput.Enable();

        _controllableFromCurrentObject = GetComponent<IControllable>();
        _controllablesFromChildren = GetComponentsInChildren<IControllable>();

        _controllables.AddRange(_controllablesFromChildren);

        if (_controllables.Count == 0)
        {
            throw new Exception($"No IControllable on this object or its children: {gameObject.name}");
        }
    }

    private void Update()
    {
        ReadMovement();
    }

    private void ReadMovement()
    {
        var direction = _gameInput.Gameplay.Movement.ReadValue<Vector3>();

        _currentDirection = Vector3.Lerp(_currentDirection, direction, _smoothing * Time.deltaTime);

        foreach (IControllable controllable in _controllables)
        {
            if (controllable != null)
            {
                controllable.Move(_currentDirection);
            }
            else
            {
                throw new Exception($"No controllable in children object {controllable}");
            }
        }

        if (_controllableFromCurrentObject != null)
        {
            Debug.Log($"CurrentObjectControllable NOT NULL {_controllableFromCurrentObject} {_currentDirection}" );
            _controllableFromCurrentObject.Move(_currentDirection);
        }
    }
}