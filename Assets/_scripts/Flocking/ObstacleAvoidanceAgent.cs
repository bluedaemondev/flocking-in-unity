using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidanceAgent : MonoBehaviour
{
    Vector3 _velocity;
    public float maxSpeed;
    public float maxForce;
    public float forwardDistance;

    public GameObject debugForwardObject;

    private void Start()
    {
        ApplyForce(transform.forward * maxSpeed);
    }

    private void Update()
    {
        debugForwardObject.transform.position = transform.position + transform.forward * forwardDistance;
        CheckBounds();
        ApplyForce(ObstacleAvoidance());
        transform.position += _velocity * Time.deltaTime;
        transform.forward = _velocity.normalized;
    }



    Vector3 ObstacleAvoidance()
    {
        Vector3 desired = new Vector3();

        if (Physics.Raycast(transform.position, transform.forward, forwardDistance))
        {
            desired = transform.position + transform.right * 10;
            desired.Normalize();
            desired *= maxSpeed;
        }
        else
        {
            return desired;
        }
       
        Vector3 steering = desired - _velocity;
        steering = Vector3.ClampMagnitude(steering, maxForce);

        return steering;
    }

    void CheckBounds()
    {
        float leftBound = ScreenUtils.Instance.LeftBound;
        float rightBound = ScreenUtils.Instance.RightBound;
        float upBound = ScreenUtils.Instance.UpperBound;
        float downBound = ScreenUtils.Instance.LowerBound;

        if (transform.position.x > rightBound) transform.position = new Vector3(leftBound, transform.position.y, transform.position.z);
        if (transform.position.x < leftBound) transform.position = new Vector3(rightBound, transform.position.y, transform.position.z);
        if (transform.position.z < downBound) transform.position = new Vector3(transform.position.x, transform.position.y, upBound);
        if (transform.position.z > upBound) transform.position = new Vector3(transform.position.x, transform.position.y, downBound);
    }
    void ApplyForce(Vector3 force)
    {
        _velocity += force;
        _velocity = Vector3.ClampMagnitude(_velocity, maxSpeed);
    }



}
