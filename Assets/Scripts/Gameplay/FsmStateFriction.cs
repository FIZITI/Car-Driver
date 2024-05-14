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
        CheckerTriggerReplace();
    }

    private void CheckerTriggerReplace()
    {
        if (Input.GetKey(_brakeKey))
            Fsm.SetState<FsmStateDrift>();
    }
}