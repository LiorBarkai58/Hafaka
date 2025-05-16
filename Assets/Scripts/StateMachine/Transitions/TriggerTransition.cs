using System;

public class TriggerTransition : ITransition
{
    public IState TargetState {get; }
    public Func<bool> Condition {get; }

    private bool shouldTransition;

    public TriggerTransition(IState targetState)
    {
        TargetState = targetState;
        Condition = () => {
            bool current = shouldTransition;
            Reset();
            return current;
        };
    }

    public void Trigger()
    {
        shouldTransition = true;
    }

    public void Reset()
    {
        shouldTransition = false;
    }

}