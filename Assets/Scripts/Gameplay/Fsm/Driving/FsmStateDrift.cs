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
    private Rigidbody _rigidbody;
    private const bool _turnOn = true;

    public FsmStateDrift(Fsm fsm, WheelCollider[] wheelColliders, float brakeTorque, KeyCode brakeKey, float driftForwardStiffness, float driftSidewaysStiffness, ParticleSystem[] tireParticleSmokes, TrailRenderer[] tireTrailSkids, Rigidbody rigidbody) : base(fsm)
    {
        _wheelColliders = wheelColliders;
        _brakeTorque = brakeTorque;
        _brakeKey = brakeKey;
        _driftForwardStiffness = driftForwardStiffness;
        _driftSidewaysStiffness = driftSidewaysStiffness;
        _tireParticleSmokes = tireParticleSmokes;
        _tireTrailSkids = tireTrailSkids;
        _rigidbody = rigidbody;
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
        Debug.Log("[Drift]");
        Debug.Log(_rigidbody.velocity);
        CheckNewState();
    }

    private void CheckNewState()
    {
        if (!Input.GetKey(_brakeKey))
            Fsm.SetState<FsmStateFriction>();
        else if (_rigidbody.velocity == Vector3.zero)
            Fsm.SetState<FsmStateIdle>();
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

        SwitchEffect(_turnOn);
    }

    private void ExitDrift()
    {
        foreach (WheelCollider wheelCollider in _wheelColliders)
        {
            wheelCollider.brakeTorque = 0f;

            Friction—hanger(wheelCollider, _normalForwardStiffness, _normalSidewaysStiffness);
        }

        SwitchEffect();
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

    private bool SwitchParticleInclusion(ParticleSystem[] particals, bool turnOn = false)
    {
        foreach (ParticleSystem partical in particals)
        {
            if (turnOn)
            {
                partical.Play();
            }
            else
            {
                partical.Stop();
                Debug.Log("Stop particle drift");
            }
        }

        return turnOn;
    }

    private bool SwitchTrailInclusion(TrailRenderer[] trails, bool turnOn = false)
    {
        foreach (TrailRenderer trail in trails)
        {
            if (turnOn)
            {
                trail.emitting = true;
            }
            else
            {
                trail.emitting = false;
            }
        }

        return turnOn;
    }

    private bool SwitchEffect(bool turnOn = false)
    {
        SwitchParticleInclusion(_tireParticleSmokes, turnOn);
        SwitchTrailInclusion(_tireTrailSkids, turnOn);
        return turnOn;
    }
}