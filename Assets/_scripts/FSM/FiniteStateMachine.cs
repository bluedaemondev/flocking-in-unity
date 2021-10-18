using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine
{
    public IState _currentState = new BlankState();
    public Dictionary<HunterEnum, IState> _allStates = new Dictionary<HunterEnum, IState>();

    private float totalEnergy;

    public float TotalEnergy { get => totalEnergy; set => totalEnergy = value; }

    public FiniteStateMachine()
    {
        TotalEnergy = 100;
    }

    public virtual void OnUpdate()
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
