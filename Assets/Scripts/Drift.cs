using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drift : MonoBehaviour
{
    [SerializeField] private WheelCollider[] _wheelColliders;
    [SerializeField] private float _brakeTorque;
    [SerializeField] private KeyCode _brakeKey;
    private bool _isBraking = false;

    private float _allCountOfDrift;
    private float _nowDriftCount;

    void Update()
    {
        if (Input.GetKeyDown(_brakeKey))
        {
            _isBraking = true;
        }
        else if (Input.GetKeyUp(_brakeKey))
        {
            _isBraking = false;
        }
    }

    void FixedUpdate()
    {
        if (_isBraking)
        {
            foreach (WheelCollider wheelCollider in _wheelColliders)
            {
                wheelCollider.motorTorque = 0f;
                wheelCollider.brakeTorque = _brakeTorque;
            }
            _nowDriftCount += Time.fixedDeltaTime;
        }
        else
        {
            foreach (WheelCollider wheelCollider in _wheelColliders)
            {
                wheelCollider.brakeTorque = 0f;
            }
        }
    }
}