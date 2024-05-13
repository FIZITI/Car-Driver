using System;
using System.Collections.Generic;

public class Fsm
{
    private FsmState StateCurrent { get; set; }

    private Dictionary<Type, FsmState> _states = new Dictionary<Type, FsmState>();

    public void AddState(FsmState state)
    {
        _states.Add(state.GetType(), state);
    }

    public void SetState<Type>() where Type : FsmState
    {
        var type = typeof(Type);

        if (StateCurrent != null && StateCurrent.GetType() == type)
        {
            return;
        }

        if (_states.TryGetValue(type, out var newState))
        {
            StateCurrent?.Exit();

            StateCurrent = newState;

            StateCurrent.Enter();
        }
    }

    public void Update()
    {
        StateCurrent?.Update();
    }
}