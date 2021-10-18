using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvadeHunterMovement : IMovement
{
    Transform m_transform;
    Transform evadeTarget;

    float futureTime;
    float maxSpeed;
    float maxForce;


    public EvadeHunterMovement(Transform transform, Transform hunter, float marginFutureTime, float maxSpeed, float maxForce)
    {
        this.m_transform = transform;
        this.evadeTarget = hunter;
        this.futureTime = marginFutureTime;
        this.maxSpeed = maxSpeed;
        this.maxForce = maxForce;
    }

    public void Move(ref Vector3 resultVelocity)
    {
        Vector3 futurePos = evadeTarget.transform.position + (evadeTarget.transform.forward /*GetComponent<Hunter>().GetVelocity()*/
                                                                * futureTime * Time.deltaTime);


        Vector3 desired = futurePos - m_transform.position;
        desired.Normalize();
        desired *= maxSpeed;
        desired *= -1;

        Vector3 steering = desired - resultVelocity;
        steering = Vector3.ClampMagnitude(steering, maxForce);

        resultVelocity = Vector3.ClampMagnitude(resultVelocity + steering, maxSpeed);

    }

}
