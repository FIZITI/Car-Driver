using UnityEngine;

public class FsmStateDrive : FsmState
{
    private WheelCollider[] _wheelColliders;
    private Vector3 _moveDirection;
    private KeyCode _brakeKey;
    private float _speedTorque;

    public FsmStateDrive(Fsm fsm, WheelCollider[] wheels, KeyCode brakeKey, float speedTorque) : base(fsm)
    {
        _wheelColliders = wheels;
        _brakeKey = brakeKey;
        _speedTorque = speedTorque;
    }

    public override void Update()
    {
        ReadInput();
        Debug.Log($"[DRIVE] {_moveDirection}");
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