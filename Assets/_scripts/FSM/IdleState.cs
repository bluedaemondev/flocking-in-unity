using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private FiniteStateMachine _fsm;

    private float untilRestingTime = 5; // cantidad de dias entre feriado y feriado
    private float currentTime = 0;

    public IdleState(FiniteStateMachine m_fsm)
    {
        this._fsm = m_fsm;
    }

    public void OnStart()
    {
        Debug.Log("Idling hunter...");
    }

    public void OnUpdate()
    {
        //Debug.Log("Idling...");
        this.currentTime += Time.deltaTime;

        if (currentTime >= untilRestingTime)
        {
            this.currentTime = 0;
            _fsm.TotalEnergy = 100;

            _fsm.ChangeState(HunterEnum.Patrol);
        }
    }

    public void OnExit()
    {
        Debug.Log("Sali de idle");
    }
}
