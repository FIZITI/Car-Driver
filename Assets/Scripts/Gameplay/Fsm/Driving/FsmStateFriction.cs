using UnityEngine;

public class FsmStateFriction : FsmState
{
    private KeyCode _brakeKey;

    public FsmStateFriction(Fsm fsm, KeyCode brakeKey) : base(fsm)
    {
        _brakeKey = brakeKey;
    }

    public override void Update()
    {
        CheckNewState();
    }

    private void CheckNewState()
    {
        if (Input.GetKey(_brakeKey))
            Fsm.SetState<FsmStateDrift>();
    }
}