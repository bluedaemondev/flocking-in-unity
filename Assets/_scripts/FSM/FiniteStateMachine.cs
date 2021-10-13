using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine
{
    IState _currentState = new BlankState();
    Dictionary<HunterEnum, IState> _allStates = new Dictionary<HunterEnum, IState>();

    private float totalEnergy;

    public float TotalEnergy { get => totalEnergy; set => totalEnergy = value; }

    public FiniteStateMachine()
    {
        TotalEnergy = 100;
    }

    public void OnUpdate()
    {
        _currentState.OnUpdate();
    }

    public void ChangeState(HunterEnum id)
    {
        if (!_allStates.ContainsKey(id)) return;

        _currentState.OnExit();
        _currentState = _allStates[id];
        _currentState.OnStart();
    }

    public void AddState(HunterEnum id, IState state)
    {
        if (_allStates.ContainsKey(id)) return;
        _allStates.Add(id, state);
    }
}
