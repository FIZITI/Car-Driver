using UnityEngine;

public class FsmStateDrive : FsmState
{
    private WheelCollider[] _wheelColliders;
    private Rigidbody _rigidbody;
    private Vector3 _moveDirection;
    private KeyCode _brakeKey;
    private float _speedTorque;

    public FsmStateDrive(Fsm fsm, WheelCollider[] wheels, Rigidbody rigidbody, KeyCode brakeKey, float speedTorque) : base(fsm)
    {
        _wheelColliders = wheels;
        _rigidbody = rigidbody;
        _brakeKey = brakeKey;
        _speedTorque = speedTorque;
    }

    public override void Update()
    {
        Debug.Log(_rigidbody.velocity);
        ReadInput();
        Drive();
        CheckNewState();
    }

    private void CheckNewState()
    {
        if (Input.GetKey(_brakeKey))
            Fsm.SetState<FsmStateBrake>();
    }

    private void ReadInput()
    {
        _moveDirection = _direction;
    }

    private void Drive()
    {
        foreach (WheelCollider wheel in _wheelColliders)
        {
            wheel.motorTorque = _moveDirection.z * _speedTorque;
        }
    }
}