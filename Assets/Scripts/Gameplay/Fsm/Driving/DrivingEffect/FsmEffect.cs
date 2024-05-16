using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FsmEffect : MonoBehaviour
{
    [SerializeField] private Rigidbody _carRigidbody;
    [SerializeField] private ParticleSystem[] _tireParticleSmokes;
    [SerializeField] private TrailRenderer[] _tireTrailSkids;
    [SerializeField] private float _minPossibleMagnitude;
    [SerializeField] private KeyCode _newStateKey;
    private Fsm _fsm;

    private void Awake()
    {
        _fsm = new Fsm();

        _fsm.AddState(new FsmStateOnEffect(_fsm, _carRigidbody, _tireParticleSmokes, _tireTrailSkids, _minPossibleMagnitude));
        _fsm.AddState(new FsmStateOffEffect(_fsm, _carRigidbody, _minPossibleMagnitude, _newStateKey));

        _fsm.SetState<FsmStateOnEffect>();
    }

    private void Update()
    {
        _fsm.Update();
    }
}