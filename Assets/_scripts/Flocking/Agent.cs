using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{
    private Vector3 _velocity;
    public float maxSpeed;
    public float maxForce;

    public float arriveDistance;

    [Header("Fleeing target")]
    public GameObject target;
    [Header("Seeking target")]
    public GameObject target2;

    public float SeekWeight;
    public float FleeWeight;
    // Start is called before the first frame update
    void Start()
    {
        ApplyForce(transform.forward);
    }

    // Update is called once per frame
    void Update()
    {
        // Seek();
        // Arrive();

        // if ()
        //{
        //  Flee();
        float fw = 0 ;
        if((target.transform.position - transform.position).magnitude < 2)
        {
            fw = FleeWeight;
        }
        //
        ApplyForce(Seek(target2) * SeekWeight);
        ApplyForce(Flee(target) * fw);
        //}
        // else 

        transform.position += _velocity * Time.deltaTime;
        transform.forward = _velocity.normalized;

    }

    Vector3 Seek(GameObject tar)
    {
        Vector3 desired = tar.transform.position - transform.position;
        desired.Normalize();
        desired *= maxSpeed;

        Vector3 steering = desired - _velocity;
        steering = Vector3.ClampMagnitude(steering, maxForce);

        //ApplyForce(steering);
        return steering;
    }

    Vector3 Flee(GameObject tar)
    {
        Vector3 desired = tar.transform.position - transform.position;
        desired.Normalize();
        desired *= maxSpeed;
        desired = -desired;

        Vector3 steering = desired - _velocity;
        steering = Vector3.ClampMagnitude(steering, maxForce);

        return steering;
    }

    void Arrive()
    {
        Vector3 desired = target.transform.position - transform.position;


        if (desired.magnitude < arriveDistance)
        {

            //  float speed = maxSpeed * (desired.magnitude / arriveDistance);
            //  Debug.Log(speed);

            float speed = Map(desired.magnitude, 0, arriveDistance, 0, maxSpeed);
            desired.Normalize();
            desired *= speed;
        }
        else
        {
            desired.Normalize();
            desired *= maxSpeed;
        }

        Vector3 steering = desired - _velocity;
        steering = Vector3.ClampMagnitude(steering, maxForce);

        ApplyForce(steering);
    }

    void ApplyForce(Vector3 force)
    {
        //_velocity += _velocity;
        _velocity = Vector3.ClampMagnitude(_velocity + force, maxSpeed);
    }

    float Map(float from, float fromMin, float fromMax, float toMin, float toMax)
    {
        return (from - toMin) / (fromMax - fromMin) * (toMax - toMin) + fromMin;
    }
}
