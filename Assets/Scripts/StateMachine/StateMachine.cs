using System;
using System.Collections.Generic;

public class StateMachine {
    StateNode current;
    Dictionary<Type, StateNode> nodes = new();
    HashSet<ITransition> anyTransitions = new();

    public void Update(){
        var transition = GetTransition();
        if(transition != null)
            ChangeState(transition.TargetState);
        current.State?.Update();
        
    }
    public void FixedUpdate(){
        current.State?.FixedUpdate();
    }

    public void SetState(IState state){
        current = nodes[state.GetType()];
        current.State?.OnEnter();
    }

    void ChangeState(IState state){
        if(state == current.State) return;

        var previousState = current.State;
        var nextState = nodes[state.GetType()].State;

        previousState?.OnExit();
        nextState?.OnEnter();
        current = nodes[state.GetType()];
    }

    private ITransition GetTransition()
    {
        // Check if the current state is null
        if (current == null)
        {
            return null; // No transition can occur if there is no current state
        }

        // Check any transitions first
        foreach (ITransition transition in anyTransitions)
        {
            if (transition.Condition.Invoke())
            {
                return transition;
            }
        }

        // Check transitions specific to the current state
        foreach (ITransition transition in current.Transitions)
        {
            if (transition != null && transition.Condition.Invoke())
            {
                return transition;
            }
        }

        return null; // No valid transition found
    }

    public void AddTransition(IState from, IState to, Func<bool> condition){
        GetOrAddNode(from).AddTransition(GetOrAddNode(to).State, condition);
    }
    public void AddAnyTransition(IState to, Func<bool> condition){
        anyTransitions.Add(new Transition(GetOrAddNode(to).State, condition));
    }

    StateNode GetOrAddNode(IState state){
        StateNode node = nodes.GetValueOrDefault(state.GetType());

        if(node == null){
            node = new StateNode(state);
            nodes.Add(state.GetType(), node);
        }
        return node;
        
    }


    private class StateNode {
        public IState State {get; }
        public HashSet<ITransition> Transitions {get; }
        public StateNode(IState state) {
            State = state;
            Transitions = new HashSet<ITransition>();
        }

        public void AddTransition(IState targetState, Func<bool> condition) {
            Transitions.Add(new Transition(targetState, condition));
        }
    }
}