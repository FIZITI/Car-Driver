using UnityEngine;

public class FsmStateFriction : FsmState, IControllable
{
    private KeyCode _brakeKey;
    private Rigidbody _rigidbody;
    private Vector3 _moveDirection;
    private IMoveControllable _moveControllable;

    public FsmStateFriction(Fsm fsm, KeyCode brakeKey, Rigidbody rigidbody, IMoveControllable moveControllable) : base(fsm)
    {
        _brakeKey = brakeKey;
        _rigidbody = rigidbody;
        _moveControllable = moveControllable;
    }

    public override void Enter()
    {
        
    }

    public override void Update()
    {
        Debug.Log("[Friction]");
        _moveControllable.MoveInWheel(_moveDirection);
        CheckNewState();
    }

    public override void Exit()
    {
        
    }

    private void CheckNewState()
    {
        if (Input.GetKey(_brakeKey))
            Fsm.SetState<FsmStateDrift>();
        else if (_moveDirection.z != 0)
            Fsm.SetState<FsmStateIdle>();
    }

    public void Move(Vector3 direction)
    {
        _moveDirection = direction;
    }
}