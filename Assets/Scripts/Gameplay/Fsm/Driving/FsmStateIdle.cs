using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FsmStateIdle : FsmState, IControllable
{
    private Vector3 _moveDirection;
    private Rigidbody _rigidbody;
    private Fsm _fsm;

    public FsmStateIdle(Fsm fsm, Rigidbody rigidbody) : base(fsm)
    {
        _rigidbody = rigidbody;
    }

    public override void Update()
    {
        Debug.Log("[Idle]");
        Debug.Log(_moveDirection);
        CheckNewState();
    }

    private void CheckNewState()
    {
        if (_moveDirection.z != 0)
            Fsm.SetState<FsmStateFriction>();
    }

    public void Move(Vector3 direction)
    {
        Debug.Log(direction);
        _moveDirection = direction;
    }
}