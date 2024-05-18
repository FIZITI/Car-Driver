using UnityEngine;

public class FsmStateFriction : FsmState, IControllable
{
    private KeyCode _brakeKey;
    private Rigidbody _rigidbody;
    private Vector3 _moveDirection;

    public FsmStateFriction(Fsm fsm, KeyCode brakeKey, Rigidbody rigidbody) : base(fsm)
    {
        _brakeKey = brakeKey;
        _rigidbody = rigidbody;
    }

    public override void Update()
    {
        CheckNewState();
    }

    private void CheckNewState()
    {
        if (Input.GetKey(_brakeKey))
            Fsm.SetState<FsmStateDrift>();
        else if (_rigidbody.velocity == Vector3.zero)
            Fsm.SetState<FsmStateIdle>();
    }

    public void Move(Vector3 direction)
    {
        _moveDirection = direction;
    }
}