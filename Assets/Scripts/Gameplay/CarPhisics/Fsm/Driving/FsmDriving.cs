using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FsmDriving : MonoBehaviour, IControllable
{
    [SerializeField] private KeyCode _brakeKey;
    [SerializeField] private WheelCollider[] _wheelColliders;
    [SerializeField] private Rigidbody _carRigidbody;
    [SerializeField] private float _brakeTorque;
    [SerializeField] private float _speedTorque;
    [SerializeField] private float _speedStiil;
    [SerializeField] private float _driftForwardStiffness;
    [SerializeField] private float _driftSidewaysStiffness;
    [SerializeField] private float _minPossibleMagnitude;
    [SerializeField] private ParticleSystem[] _tireParticleSmokes;
    [SerializeField] private TrailRenderer[] _tireTrailSkids;
    private Vector3 _moveDirection;
    private Fsm _fsm;

    private void Awake()
    {
        _fsm = new Fsm();

        _fsm.AddState(new FsmStateIdle(_fsm, _carRigidbody, _moveDirection,  _brakeKey, _speedStiil));
        _fsm.AddState(new FsmStateDrive(_fsm, _wheelColliders, _carRigidbody, _moveDirection, _brakeKey, _speedStiil, _speedTorque));
        _fsm.AddState(new FsmStateBrake(_fsm, _tireParticleSmokes, _tireTrailSkids, _wheelColliders, _carRigidbody, _brakeKey, _driftForwardStiffness, _driftSidewaysStiffness, _brakeTorque, _speedStiil));

        _fsm.SetState<FsmStateIdle>();
    }

    private void Update()
    {
        _fsm.Update();
    }

    public void Move(Vector3 direction)
    {
        Debug.Log($"FsmDRIVINING {direction}");
        _moveDirection = direction;
        Debug.Log($"moveDirection: {_moveDirection}, direction: {direction}");
    }
}