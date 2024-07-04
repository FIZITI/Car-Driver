using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FsmDriving : MonoBehaviour, IControllable
{
    [SerializeField] private KeyCode _brakeKey;
    [SerializeField] private WheelCollider[] _backWheelColliders;
    [SerializeField] private Rigidbody _carRigidbody;
    [SerializeField] private float _brakeTorque;
    [SerializeField] private float _speedTorque;
    [SerializeField] private float _speedStiil;
    [SerializeField] private float _driftForwardStiffness;
    [SerializeField] private float _driftSidewaysStiffness;
    [SerializeField] private float _minPossibleMagnitude;
    [SerializeField] private ParticleSystem[] _tireParticleSmokes;
    [SerializeField] private TrailRenderer[] _tireTrailSkids;
    private Fsm _fsm;

    private void Awake()
    {
        _fsm = new Fsm();

        _fsm.AddState(new FsmStateDrive(_fsm, _backWheelColliders, _brakeKey, _speedTorque));
        _fsm.AddState(new FsmStateBrake(_fsm, _tireParticleSmokes, _tireTrailSkids, _backWheelColliders, _carRigidbody, _brakeKey, _driftForwardStiffness, _driftSidewaysStiffness, _brakeTorque, _speedStiil));

        _fsm.SetState<FsmStateDrive>();
    }

    private void Update()
    {
        _fsm.Update();
    }

    public void Move(Vector3 direction)
    {
        _fsm.UpdateInput(direction);
    }
}