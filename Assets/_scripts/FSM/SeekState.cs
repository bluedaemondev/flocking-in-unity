using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekState : IState
{
    private FiniteStateMachine fsm;
    private Transform chaseTarget;
    private Transform m_transform;
    private float speed = 5f;

    private float energyPerSecond = 10f;

    private float rangeArrive;

    private Vector3 movementDirection;
    public SeekState(FiniteStateMachine fsm, Transform myTransform, Transform firstTarget, float arriveRadius = 2f)
    {
        this.fsm = fsm;
        this.chaseTarget = firstTarget;
        this.m_transform = myTransform;
        this.rangeArrive = arriveRadius;

    }

    public void OnExit()
    {
        Debug.Log("Leaving chase state");

    }

    public void OnStart()
    {
        Debug.Log("Chasing ... " + chaseTarget.name);
    }

    public void OnUpdate()
    {
        movementDirection = (chaseTarget.position - m_transform.position);
        movementDirection.y = 0;
        m_transform.GetComponent<Animator>().SetBool("moving", true);

        //if (rangeArrive <= movementDirection.magnitude)
        //{
        //fsm.ChangeState(HunterEnum.Attacking);
        //fsm.ChangeState(HunterEnum.Idle);
        Debug.Log("should be attacking _ fsm");
        //m_transform.GetComponent<Animator>().SetBool("moving", false);

        //}
        //else
        //{
        m_transform.forward = movementDirection.normalized;

        Debug.Log(m_transform.position + m_transform.forward * Time.deltaTime * speed);

        m_transform.position += m_transform.forward * Time.deltaTime * speed;

        fsm.TotalEnergy -= energyPerSecond * Time.deltaTime ;

        if (fsm.TotalEnergy <= 0)
        {
            fsm.ChangeState(HunterEnum.Idle);
            m_transform.GetComponent<Animator>().SetBool("moving", false);

            fsm.TotalEnergy = 100;
        }
        //}

    }


}
