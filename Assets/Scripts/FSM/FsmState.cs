using UnityEngine;

public abstract class FsmState
{
    protected readonly Fsm Fsm;
    protected Vector3 _direction;

    public FsmState(Fsm fsm)
    {
        Fsm = fsm;
    }

    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }
    public void UpdateInput(Vector3 direction)
    {
        _direction = direction;
    }
}