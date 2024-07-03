using UnityEngine;

public class FsmStateBrake : FsmState
{
    private ParticleSystem[] _tireParticleSmokes;
    private TrailRenderer[] _tireTrailSkids;
    private WheelCollider[] _wheelColliders;
    private Rigidbody _rigidbody;
    private KeyCode _brakeKey;
    private WheelFrictionCurve _rearForwardFriction;
    private WheelFrictionCurve _rearSidewaysFriction;
    private float _driftForwardStiffness;
    private float _driftSidewaysStiffness;
    private float _brakeTorque;
    private float _speedStiil;
    private float _normalForwardStiffness;
    private float _normalSidewaysStiffness;
    private const bool _turnOn = true;

    public FsmStateBrake(Fsm fsm, ParticleSystem[] tireParticleSmokes, TrailRenderer[] tireTrailSkids, WheelCollider[] wheelColliders, Rigidbody rigidbody, KeyCode brakeKey, float driftForwardStiffness, float driftSidewaysStiffness, float brakeTorque, float speedStiil) : base(fsm)
    {
        _tireParticleSmokes = tireParticleSmokes;
        _tireTrailSkids = tireTrailSkids;
        _wheelColliders = wheelColliders;
        _rigidbody = rigidbody;
        _brakeKey = brakeKey;
        _driftForwardStiffness = driftForwardStiffness;
        _driftSidewaysStiffness = driftSidewaysStiffness;
        _brakeTorque = brakeTorque;
        _speedStiil = speedStiil;
    }

    public override void Enter()
    {
        EnterBrake();
    }

    public override void Exit()
    {
        ExitBrake();
    }

    public override void Update()
    {
        Debug.Log("[Drift]");
        Debug.Log(_rigidbody.velocity);
        StopCarWithEffect();
        CheckNewState();
    }

    private void CheckNewState()
    {
        if (!Input.GetKey(_brakeKey))
            Fsm.SetState<FsmStateIdle>();
    }

    private void StopCarWithEffect()
    {
        if (Mathf.Abs(_rigidbody.velocity.z) < _speedStiil)
        {
            SwitchEffect();
        }
    }

    private void EnterBrake()
    {
        foreach (WheelCollider wheelCollider in _wheelColliders)
        {
            wheelCollider.motorTorque = 0f;
            wheelCollider.brakeTorque = _brakeTorque;

            SetNormalStiffness(wheelCollider);
            FrictionÑhanger(wheelCollider, _driftForwardStiffness, _driftSidewaysStiffness);
        }

        SwitchEffect(_turnOn);
    }

    private void ExitBrake()
    {
        foreach (WheelCollider wheelCollider in _wheelColliders)
        {
            wheelCollider.brakeTorque = 0f;

            FrictionÑhanger(wheelCollider, _normalForwardStiffness, _normalSidewaysStiffness);
        }

        SwitchEffect();
    }

    private void FrictionÑhanger(WheelCollider wheelCollider, float forwardStiffness, float sidewaysStiffness)
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