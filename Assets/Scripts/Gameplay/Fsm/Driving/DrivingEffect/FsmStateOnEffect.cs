using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FsmStateOnEffect : FsmState
{
    private Rigidbody _rigidbody;
    private ParticleSystem[] _tireParticleSmokes;
    private TrailRenderer[] _tireTrailSkids;
    private float _minPossibleMagnitude;
    private Fsm _fsm;
    private KeyCode _newStateKey;
    private bool _enableEffect;
    private const bool _turnOn = true;

    public FsmStateOnEffect(Fsm fsm, Rigidbody rigidbody, ParticleSystem[] smokes, TrailRenderer[] trail, float minPossibleMagnitude) : base(fsm)
    {
        _fsm = fsm;
        _rigidbody = rigidbody;
        _tireParticleSmokes = smokes;
        _tireTrailSkids = trail;
        _minPossibleMagnitude = minPossibleMagnitude;
        _enableEffect = false;
    }

    public override void Enter()
    {
        EnterEffect();
    }

    public override void Exit()
    {
        ExitEffect();
    }

    public override void Update()
    {
        CheckNewState();
    }

    private void CheckNewState()
    {
        if (!Input.GetKey(_newStateKey))
            Fsm.SetState<FsmStateOffEffect>();
    }

    private void EnterEffect()
    {
        SwitchEffect(_turnOn);
    }

    private void ExitEffect()
    {
        SwitchEffect(!_turnOn);
    }

    private void CheckEnableEffect()
    {
/*        if (_rigidbody.velocity.magnitude < _minPossibleMagnitude && _enableEffect)
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
        }*/
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
    }

    private bool SwitchEffect(bool turnOn)
    {
        SwitchParticleInclusion(_tireParticleSmokes, turnOn);
        SwitchTrailInclusion(_tireTrailSkids, turnOn);
        return turnOn;
    }
}