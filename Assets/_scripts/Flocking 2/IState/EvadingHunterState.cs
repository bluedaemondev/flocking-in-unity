using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvadingHunterState : IState
{
    IMovement movementHandler;
    PigFiniteStateMachine fsm;
    Pig pig;
    Vector3 velocity;

    public EvadingHunterState(Transform boid, Transform hunter, PigFiniteStateMachine fsm, Pig pig, float maxSpeed, float maxForce)
    {
        this.fsm = fsm;
        this.movementHandler = new EvadeHunterMovement(boid, hunter, 1, maxSpeed, maxForce);
        this.pig = pig;
    }

    public void OnExit()
    {
        Debug.Log("exit evade");
    }

    public void OnStart()
    {
        Debug.Log("start evade");
    }

    public void OnUpdate()
    {
        movementHandler.Move(ref velocity);
        pig.ApplyForce(velocity);
    }

}
