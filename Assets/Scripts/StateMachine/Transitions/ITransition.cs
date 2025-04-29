using System;

public interface ITransition
{
    IState TargetState { get; }
    Func<bool> Condition { get; }


}