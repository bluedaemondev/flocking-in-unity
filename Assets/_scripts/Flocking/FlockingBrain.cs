using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockingBrain : MonoBehaviour
{
    public static FlockingBrain Instance
    {
        get
        {
            return _instance;
        }
    }

    private static FlockingBrain _instance;

    private List<Boid> loadedBoids;

    [Header("Boid Values")]
    [SerializeField]
    private float globalViewDistance;
    [SerializeField]
    private float globalSeparationWeight;
    [SerializeField]
    private float globalCohesionWeight;
    [SerializeField]
    private float globalAlignWeight;
    [SerializeField]
    private float globalMaxSpeed;
    [SerializeField]
    private float viewDistance;

    [SerializeField, Range(0.01f, 1f)]
    private float globalMaxForce;

    public List<Boid> LoadedBoids { get => loadedBoids; }

    public float GlobalSeparationWeight { get => globalSeparationWeight; }
    public float GlobalMaxSpeed { get => globalMaxSpeed; }
    public float GlobalAlignWeight { get => globalAlignWeight; }
    public float GlobalCohesionWeight { get => globalCohesionWeight; }
    public float GlobalMaxForce { get => globalMaxForce; }
    public float ViewDistance { get => viewDistance; }

    public Transform GetRandomBoid()
    {
        return this.loadedBoids[Random.Range(0, loadedBoids.Count)].transform;
    }

    // Start is called before the first frame update
    void Awake()
    {
        _instance = this;
        loadedBoids = new List<Boid>();
    }

    public void AddBoid(Boid b)
    {
        if (!loadedBoids.Contains(b))
            loadedBoids.Add(b);
    }

    public Vector3 CalculateCohesion(Boid b)
    {
        Vector3 desired = new Vector3();
        int nearbyBoids = 0;

        foreach (var item in LoadedBoids)
        {
            if (item != this && Vector3.Distance(item.transform.position, transform.position) < viewDistance)
            {
                nearbyBoids++;
                desired += item.transform.position;
            }
        }

        desired.y = 0;
        if (nearbyBoids == 0) return Vector3.zero;

        desired /= nearbyBoids;
        desired = desired - transform.position;
        desired.Normalize();
        desired *= globalMaxSpeed;

        // Vector3 steering = Seek(desired);
        Vector3 steering = desired - b.Velocity;
        steering = Vector3.ClampMagnitude(steering, b.MaxForce);

        return steering;
    }
    public Vector3 CalculateSeparation(Boid b)
    {
        Vector3 desired = new Vector3();

        int boidsNearby = 0;
        foreach (var boid in LoadedBoids)
        {
            Vector3 distance = (boid.transform.position - transform.position);

            if (boid != this && distance.magnitude < viewDistance)
            {
                // desired += distance;
                desired.x += distance.x;
                desired.z += distance.z;
                boidsNearby++;
            }
        }
        //desired.y = 0;

        if (boidsNearby == 0) return Vector3.zero;

        desired /= boidsNearby;
        desired.Normalize();
        desired *= globalMaxSpeed;
        desired *= -1;

        Vector3 steering = desired - b.Velocity;
        steering = Vector3.ClampMagnitude(steering, b.MaxForce);
        //steering.y = 0;
        return steering;
    }

    public Vector3 CalculateAlign(Boid b)
    {
        Vector3 desired = new Vector3();

        int boidsNearby = 0;
        foreach (var item in LoadedBoids)
        {
            if (item != this && item != null)
            {
                Vector3 dist = item.transform.position - transform.position;
                if (Vector3.Magnitude(dist) < viewDistance)
                {
                    boidsNearby++;
                    desired.x += item.Velocity.x;
                    desired.z += item.Velocity.z;
                }
            }
        }
        if (boidsNearby == 0) return Vector3.zero;
        desired = desired / boidsNearby;
        desired.Normalize();
        desired *= globalMaxSpeed;

        Vector3 steering = Vector3.ClampMagnitude(desired - b.Velocity, b.MaxForce);

        return steering;
    }

    public Vector3 CalculateSeek(Vector3 pos, Boid b)
    {
        Vector3 desired = pos - transform.position;
        desired.Normalize();
        desired *= GlobalMaxSpeed;

        Vector3 steering = desired - b.Velocity;
        steering = Vector3.ClampMagnitude(steering, GlobalMaxForce);

        return steering;
    }


    //public Vector3 CalculateObstacleAvoidance(Boid b)
    //{
    //    Vector3 desired = new Vector3();

    //    Debug.DrawRay(transform.position, transform.forward * forwardDistance, Color.red);
    //    if (Physics.Raycast(transform.position, transform.forward, forwardDistance))
    //    {
    //        desired = transform.position + transform.right * 10;
    //        desired.Normalize();
    //        desired *= maxSpeed;
    //    }
    //    else
    //    {
    //        return desired;
    //    }

    //    Vector3 steering = desired - Velocity;
    //    steering = Vector3.ClampMagnitude(steering, MaxForce);

    //    return steering;
    //}


}
