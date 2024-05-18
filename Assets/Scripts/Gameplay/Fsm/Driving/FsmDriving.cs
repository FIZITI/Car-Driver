using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FsmDriving : MonoBehaviour, IControllable
{
    [SerializeField] private KeyCode _brakeKey;
    [SerializeField] private WheelCollider[] _wheelColliders;
    [SerializeField] private float _brakeTorque;
    [SerializeField] private float _driftForwardStiffness;
    [SerializeField] private float _driftSidewaysStiffness;
    [SerializeField] private ParticleSystem[] _tireParticleSmokes;
    [SerializeField] private TrailRenderer[] _tireTrailSkids;
    [SerializeField] private Rigidbody _carRigidbody;
    [SerializeField] private float _minPossibleMagnitude;
    private Vector3 _moveDirection;
    private Fsm _fsm;

    private void Awake()
    {
        _fsm = new Fsm();

        _fsm.AddState(new FsmStateIdle(_fsm, _carRigidbody));
        _fsm.AddState(new FsmStateFriction(_fsm, _brakeKey, _carRigidbody));
        _fsm.AddState(new FsmStateDrift(_fsm, _wheelColliders, _brakeTorque, _brakeKey, _driftForwardStiffness, _driftSidewaysStiffness, _tireParticleSmokes, _tireTrailSkids, _carRigidbody));

        _fsm.SetState<FsmStateIdle>();
    }

    private void Update()
    {
        _fsm.Update();
    }

    public void Move(Vector3 direction)
    {
        _moveDirection = direction;
    }
}