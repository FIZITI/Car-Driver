using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FsmStateIdle : FsmState
{
    private Rigidbody _rigidbody;
    private Fsm _fsm;

    public FsmStateIdle(Fsm fsm, Rigidbody rigidbody) : base(fsm)
    {
        _rigidbody = rigidbody;
    }

    public override void Update()
    {
        CheckNewState();
    }

    private void CheckNewState()
    {
        if (_rigidbody.velocity != Vector3.zero)
            Fsm.SetState<FsmStateFriction>();
    }
}