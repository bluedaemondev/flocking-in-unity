using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigFiniteStateMachine : FiniteStateMachine
{
    //public enum BoidState
    //{
    //    Idle,
    //    Patrol,
    //    SearchingFood,
    //    ChasingFood,
    //    Eating,
    //    Dead
    //}

    new Dictionary<BoidState, IState> _allStates = new Dictionary<BoidState, IState>();


    public void AddState(BoidState key, IState state)
    {
        if (_allStates.ContainsKey(key)) return;
        _allStates.Add(key, state);
    }
    public void ChangeState(BoidState state)
    {
        if (!_allStates.ContainsKey(state)) return;

        _currentState.OnExit();
        _currentState = _allStates[state];
        _currentState.OnStart();
    }
    
}
