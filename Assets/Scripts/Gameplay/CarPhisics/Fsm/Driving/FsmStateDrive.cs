using UnityEngine;

public class FsmStateDrive : FsmState
{
    private WheelCollider[] _wheelColliders;
    private Rigidbody _rigidbody;
    private Vector3 _moveDirection;
    private KeyCode _brakeKey;
    private float _speedStiil;
    private float _speedTorque;

    public FsmStateDrive(Fsm fsm, WheelCollider[] wheels, Rigidbody rigidbody, Vector3 moveDirection, KeyCode brakeKey, float speedStiil, float speedTorque) : base(fsm)
    {
        _wheelColliders = wheels;
        _rigidbody = rigidbody;
        _moveDirection = moveDirection;
        _brakeKey = brakeKey;
        _speedStiil = speedStiil;
        _speedTorque = speedTorque;
    }

    public override void Update()
    {
        Debug.Log("[Drive]");
        Debug.Log(_rigidbody.velocity);
        Drive();
        CheckNewState();
    }

    private void CheckNewState()
    {
        if (Input.GetKey(_brakeKey))
            Fsm.SetState<FsmStateBrake>();
        else if (Mathf.Abs(_rigidbody.velocity.z) < _speedStiil && _moveDirection.z == 0)
            Fsm.SetState<FsmStateIdle>();
    }

    private void Drive()
    {
        foreach (WheelCollider wheel in _wheelColliders)
        {
            wheel.motorTorque =  _speedTorque;
        }
    }
}