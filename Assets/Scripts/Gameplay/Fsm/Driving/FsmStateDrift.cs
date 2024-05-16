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
    private float _minPossibleMagnitude;
    private bool _enableEffect = false;
    private const bool _turnOn = true;

    public FsmStateDrift(Fsm fsm, WheelCollider[] wheelColliders, float brakeTorque, KeyCode brakeKey, float driftForwardStiffness, float driftSidewaysStiffness, ParticleSystem[] tireParticleSmokes, TrailRenderer[] tireTrailSkids, Rigidbody rigidbody, float minPossibleMagnitude) : base(fsm)
    {
        _wheelColliders = wheelColliders;
        _brakeTorque = brakeTorque;
        _brakeKey = brakeKey;
        _driftForwardStiffness = driftForwardStiffness;
        _driftSidewaysStiffness = driftSidewaysStiffness;
        _tireParticleSmokes = tireParticleSmokes;
        _tireTrailSkids = tireTrailSkids;
        _rigidbody = rigidbody;
        _minPossibleMagnitude = minPossibleMagnitude;
    }

    public override void Enter()
    {
        EnterDrift();
    }

    public override void Exit()
    {
        ExitDrift();
/*        SwitchEffect(false);
        _enableEffect = false;*/
    }

    public override void Update()
    {
        CheckNewState();
/*        CheckEnableEffect();*/
    }

    private void CheckNewState()
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

/*        _enableEffect = SwitchParticleInclusion(_tireParticleSmokes, _turnOn);*/
/*        OnTrailSkids();*/
/*        OnParticleSmokes();*/
    }

    private void ExitDrift()
    {
        foreach (WheelCollider wheelCollider in _wheelColliders)
        {
            wheelCollider.brakeTorque = 0f;

            Friction—hanger(wheelCollider, _normalForwardStiffness, _normalSidewaysStiffness);
        }

        /*        _enableEffect = SwitchParticleInclusion(_tireParticleSmokes, !_turnOn);*/
        /*        OffTrailSkids();*/
        /*        OffParticleSmokes();*/
/*        _enableEffect = SwitchEffect(!_turnOn);*/
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

/*    private bool OnParticleSmokes()
    {
        foreach (ParticleSystem partical in _tireParticleSmokes)
        {
            SwitchParticleInclusion(partical, true);
        }

        return true;
    }

    private bool OffParticleSmokes()
    {
        foreach (ParticleSystem partical in _tireParticleSmokes)
        {
            SwitchParticleInclusion(partical, false);
        }

        return false;
    }*/

/*    private bool SwitchParticleInclusion(ParticleSystem[] particals, bool turnOn = false)
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
                Debug.Log("Stop");
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
    }*/

    /*    private void SwitchTrailInclusion(TrailRenderer trail, bool _turnOn)
        {
            if (_turnOn)
            {
                trail.emitting = true;
            }
            else
            {
                trail.emitting = false;
            }
        }

        private bool OnTrailSkids()
        {
            foreach (TrailRenderer trail in _tireTrailSkids)
            {

            }

            return true;
        }

        private bool OffTrailSkids()
        {
            foreach (TrailRenderer trail in _tireTrailSkids)
            {

            }

            return false;
        }*/

/*    private void CheckEnableEffect()
    {
        if (_rigidbody.velocity.magnitude < _minPossibleMagnitude && _enableEffect)
        {
            _enableEffect = SwitchEffect(!_turnOn);
*//*            _enableEffect = SwitchParticleInclusion(_tireParticleSmokes, !_turnOn);
            SwitchTrailInclusion(_tireTrailSkids, !_turnOn);*//*
        }
        else if (_rigidbody.velocity.magnitude > _minPossibleMagnitude && !_enableEffect)
        {
            _enableEffect = SwitchEffect(_turnOn);
*//*            _enableEffect = SwitchParticleInclusion(_tireParticleSmokes, _turnOn);
            SwitchTrailInclusion(_tireTrailSkids, _turnOn);*//*
        }
    }

    private bool SwitchEffect(bool turnOn)
    {
        SwitchParticleInclusion(_tireParticleSmokes, turnOn);
        SwitchTrailInclusion(_tireTrailSkids, turnOn);
        return turnOn;
    }*/
}