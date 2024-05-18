using System;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private IControllable[] _controllables;
    private GameInput _gameInput;

    private void Awake()
    {
        _gameInput = new GameInput();
        _gameInput.Enable();

        _controllables= GetComponentsInChildren<IControllable>();

        if (_controllables == null)
        {
            throw new Exception($"No IControllable on this object: {gameObject.name}");
        }
    }

    private void Update()
    {
        ReadMovement();
    }

    private void ReadMovement()
    {
        var direction = _gameInput.Gameplay.Movement.ReadValue<Vector3>();
        Debug.Log(direction);

        foreach (IControllable controllable in _controllables)
        {
            if (controllable != null)
            {
                controllable.Move(direction);
            }
            else
            {
                throw new Exception($"No controllable in children object {controllable}");
            }
        }
    }
}