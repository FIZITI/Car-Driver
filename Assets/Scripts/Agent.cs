using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    private Transform _targetTransform;


    private void Start()
    {
        _targetTransform = _target.transform;
    }

    private void Update()
    {

    }

    private void SetTargetPosition()
    {

    }

    private void SetAgentPosition()
    {

    }
}