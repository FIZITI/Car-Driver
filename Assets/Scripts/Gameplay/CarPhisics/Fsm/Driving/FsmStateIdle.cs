using UnityEngine;

public class FsmStateIdle : FsmState
{
    private Rigidbody _rigidbody;
    private Vector3 _moveDirection;
    private KeyCode _brakeKey;
    private float _speedStiil;


    public FsmStateIdle(Fsm fsm, Rigidbody rigidbody, Vector3 moveDirection, KeyCode brakeKey, float speedStiil) : base(fsm)
    {
        _rigidbody = rigidbody;
        _moveDirection = moveDirection;
        _brakeKey = brakeKey;
        _speedStiil = speedStiil;
    }

    public override void Update()
    {
        Debug.Log($"[Idle] {_moveDirection}");
        CheckNewState();
    }

    private void CheckNewState()
    {
        if (Mathf.Abs(_rigidbody.velocity.z) >= _speedStiil || _moveDirection.z != 0)
            Fsm.SetState<FsmStateDrive>();
    }
}