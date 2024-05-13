using UnityEngine;

public class FsmStateDrift : FsmState
{
    private WheelCollider[] _wheelColliders;
    private float _brakeTorque;
    private KeyCode _brakeKey;
    private WheelFrictionCurve _rearForwardFriction;
    private WheelFrictionCurve _rearSidewaysFriction;
    private float _driftForwardStiffness;
    private float _driftSidewaysStiffness;
    private float _normalForwardStiffness;
    private float _normalSidewaysStiffness;

    public FsmStateDrift(Fsm fsm, WheelCollider[] wheelColliders, float brakeTorque, KeyCode brakeKey, float driftForwardStiffness, float driftSidewaysStiffness) : base(fsm)
    {
        _wheelColliders = wheelColliders;
        _brakeTorque = brakeTorque;
        _brakeKey = brakeKey;
        _driftForwardStiffness = driftForwardStiffness;
        _driftSidewaysStiffness = driftSidewaysStiffness;
    }

    public override void Enter()
    {
        EnterDrift();
    }

    public override void Exit()
    {
        ExitDrift();
    }

    public override void Update()
    {
        if (!Input.GetKey(_brakeKey))
            Fsm.SetState<FsmStateFriction>();
    }

    private void EnterDrift()
    {
        foreach (WheelCollider wheelCollider in _wheelColliders)
        {
            wheelCollider.motorTorque = 0f;
            wheelCollider.brakeTorque = _brakeTorque;

            SetNormalStiffness(wheelCollider);
            Friction—hanger(wheelCollider, _driftForwardStiffness, _driftSidewaysStiffness);
        }
    }

    private void ExitDrift()
    {
        foreach (WheelCollider wheelCollider in _wheelColliders)
        {
            wheelCollider.brakeTorque = 0f;

            Friction—hanger(wheelCollider, _normalForwardStiffness, _normalSidewaysStiffness);
        }
    }

    private void Friction—hanger(WheelCollider wheelCollider, float forwardStiffness, float sidewaysStiffness)
    {
        _rearForwardFriction = wheelCollider.forwardFriction;
        _rearSidewaysFriction = wheelCollider.sidewaysFriction;

        _rearForwardFriction.stiffness = forwardStiffness;
        _rearSidewaysFriction.stiffness = sidewaysStiffness;

        wheelCollider.forwardFriction = _rearForwardFriction;
        wheelCollider.sidewaysFriction = _rearSidewaysFriction;
    }

    private void SetNormalStiffness(WheelCollider wheelColider)
    {
        _normalForwardStiffness = wheelColider.forwardFriction.stiffness;
        _normalSidewaysStiffness = wheelColider.sidewaysFriction.stiffness;
    }
}