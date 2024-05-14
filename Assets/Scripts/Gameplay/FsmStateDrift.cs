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
    private ParticleSystem[] _tireParticleSmokes;
    private TrailRenderer[] _tireTrailSkids;

    public FsmStateDrift(Fsm fsm, WheelCollider[] wheelColliders, float brakeTorque, KeyCode brakeKey, float driftForwardStiffness, float driftSidewaysStiffness, ParticleSystem[] tireParticleSmokes, TrailRenderer[] tireTrailSkids) : base(fsm)
    {
        _wheelColliders = wheelColliders;
        _brakeTorque = brakeTorque;
        _brakeKey = brakeKey;
        _driftForwardStiffness = driftForwardStiffness;
        _driftSidewaysStiffness = driftSidewaysStiffness;
        _tireParticleSmokes = tireParticleSmokes;
        _tireTrailSkids = tireTrailSkids;
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

        OnTrailSkids();
        OnParticleSmokes();
    }

    private void ExitDrift()
    {
        foreach (WheelCollider wheelCollider in _wheelColliders)
        {
            wheelCollider.brakeTorque = 0f;

            Friction—hanger(wheelCollider, _normalForwardStiffness, _normalSidewaysStiffness);
        }

        OffTrailSkids();
        OffParticleSmokes();
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

    private void OnParticleSmokes()
    {
        foreach (ParticleSystem partical in _tireParticleSmokes)
        {
            partical.Play();
        }
    }

    private void OffParticleSmokes()
    {
        foreach (ParticleSystem partical in _tireParticleSmokes)
        {
            partical.Stop();
        }
    }

    private void OnTrailSkids()
    {
        foreach (TrailRenderer trail in _tireTrailSkids)
        {
            trail.emitting = true;
        }
    }

    private void OffTrailSkids()
    {
        foreach (TrailRenderer trail in _tireTrailSkids)
        {
            trail.emitting = false;
        }
    }
}