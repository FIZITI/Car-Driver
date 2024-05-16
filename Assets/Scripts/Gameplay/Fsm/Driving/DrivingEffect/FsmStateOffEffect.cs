using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FsmStateOffEffect : FsmState
{
    private Rigidbody _rigidbody;
    private float _minPossibleMagnitude;
    private KeyCode _newStateKey;
    private Fsm _fsm;

    public FsmStateOffEffect(Fsm fsm, Rigidbody rigidbody, float minPossibleMagnitude, KeyCode newStateKey) : base(fsm)
    {
        _fsm = fsm;
        _rigidbody = rigidbody;
        _minPossibleMagnitude = minPossibleMagnitude;
        _newStateKey = newStateKey;
    }

    public override void Update()
    {
        Debug.Log(_rigidbody.velocity.magnitude);
        CheckNewState();
    }

    private void CheckNewState()
    {
        if (Input.GetKey(_newStateKey))
            Fsm.SetState<FsmStateOnEffect>();
    }
}