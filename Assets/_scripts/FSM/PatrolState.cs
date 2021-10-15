using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    private Transform m_transform;
    private FiniteStateMachine fsm;
    private List<Transform> waypoints;
    int currentWaypoint;

    LayerMask whatIsFood;

    private float energyPerSecond = 10f;

    public PatrolState(FiniteStateMachine fsm, Transform transf, List<Transform> waypoints, LayerMask pigMask, float speed = 5)
    {
        this.fsm = fsm;
        this.m_transform = transf;
        this.waypoints = waypoints;
        this.currentWaypoint = 0;
        this.whatIsFood = pigMask;
    }

    public void OnStart()
    {
        Debug.Log("Entre en patrol");
    }

    public void OnUpdate()
    {
        //Debug.Log("Patroling...");
        if (waypoints != null && waypoints.Count > 0)
        {
            SearchForPigs();
            ChaseWaypoint();
        }
        else
            fsm.ChangeState(HunterEnum.Idle);

    }

    public void OnExit()
    {
        Debug.Log("Sali de patrol");

    }
    private void SearchForPigs()
    {
        var aux = Physics.SphereCastAll(m_transform.position, 10, Vector3.zero, 0, whatIsFood.value);
        Debug.DrawRay(m_transform.position, m_transform.forward * 10, Color.green);

        Debug.Log(string.Format("{0} pigs in range, following {1}", aux.Length, aux.Length > 0 ? aux[0].transform.name : "not assigned"));

        if (aux.Length > 0)
        {
            fsm.ChangeState(HunterEnum.Chasing);
        }
    }

    private Vector3 ChaseWaypoint()
    {
        Vector3 dir = this.waypoints[this.currentWaypoint].position - this.m_transform.position;
        dir.y = 0;




        this.m_transform.forward = dir;

        this.m_transform.position += this.m_transform.forward * 5 * Time.deltaTime;


        if (dir.magnitude < 0.1f)
        {
            this.currentWaypoint++;
            if (this.currentWaypoint > this.waypoints.Count - 1)
            {
                this.currentWaypoint = 0;
            }
        }

        this.fsm.TotalEnergy -= energyPerSecond * Time.deltaTime;

        if (fsm.TotalEnergy <= 0)
        {
            fsm.ChangeState(HunterEnum.Idle);
            fsm.TotalEnergy = 100;
        }

        return dir;
    }
}
