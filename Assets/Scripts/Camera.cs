using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    [SerializeField] private float _positionOffset;
    private Vector3 _newPosition;

    private void Awake()
    {
        _newPosition = transform.position;

    }

    private void LateUpdate()
    {
        _newPosition.x = _target.transform.position.x - _positionOffset;
        _newPosition.z = _target.transform.position.z;
        transform.position = _newPosition;
    }
}