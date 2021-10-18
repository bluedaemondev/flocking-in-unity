using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Boid : MonoBehaviour
{
    private Vector3 _velocity;
    internal Collider m_collider;

    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float maxForce;
    [SerializeField]
    private float viewDistance;
    [SerializeField]
    private float separationWeight;
    [SerializeField]
    private float cohesionWeight;
    [SerializeField]
    private float alignWeight;

    [SerializeField]
    private float seekWeight;


    [SerializeField]
    float forwardDistance;

    [SerializeField]
    GameObject currentlySeeking;
    [SerializeField]
    LayerMask whatIsFood;

    public Vector3 Velocity { get => _velocity; }
    public float MaxForce { get => maxForce; set => maxForce = value; }
    public float MaxSpeed { get => maxSpeed; set => maxSpeed = value; }
    public float ViewDistance { get => viewDistance; set => viewDistance = value; }

    //public FiniteStateMachine _fsm;

    // Start is called before the first frame update
    //void Start()
    //{
    //    ////this.contactArea = FlockingBrain.Instance.spawnBounds;
    //    //FlockingBrain.Instance.AddBoid(this);

    //    //Vector3 randomForce = new Vector3(Random.Range(-6, 2), 0, Random.Range(-6, 6));
    //    //randomForce.Normalize();
    //    //randomForce *= maxSpeed;

    //    //randomForce = Vector3.ClampMagnitude(randomForce, MaxForce);

    //    ////ApplyForce(randomForce);
    //}
    private void Start()
    {

        FoodFinder();
    }
    // Update is called once per frame
    void Update()
    {
        FoodFinder();


        ApplyForce(FlockingBrain.Instance.CalculateCohesion(this) * cohesionWeight * (1-seekWeight));
        ApplyForce(FlockingBrain.Instance.CalculateSeparation(this) * (1 - seekWeight));
        ApplyForce(FlockingBrain.Instance.CalculateAlign(this) * (1 - seekWeight));


        if (currentlySeeking != null)
        {

            ApplyForce(FlockingBrain.Instance.CalculateSeek(currentlySeeking.transform.position, this) * seekWeight);

            if ((currentlySeeking.transform.position - transform.position).magnitude <= 1.5f)
            {
                currentlySeeking.GetComponent<Pickup>().BeEaten(this);
                _velocity = Vector3.zero;

                ApplyForce(FlockingBrain.Instance.CalculateArrive(currentlySeeking, 1, this));
            }
        }





        _velocity.y = 0;

        transform.position += Velocity * Time.deltaTime;
        transform.forward = Velocity.normalized;
    }

    void ApplyForce(Vector3 force)
    {
        this._velocity += force;
        this._velocity = Vector3.ClampMagnitude(this._velocity, this.MaxSpeed);
    }

    void FoodFinder()
    {
        if (currentlySeeking != null)
            return;

        foreach (var pickup in PickupsManager.Instance.LoadedPickups)
        {
            if (!pickup.hasSeeker /*&& Vector3.Magnitude(transform.position - pickup.transform.position) < forwardDistance*/)
            {
                this.currentlySeeking = pickup.gameObject;
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, ViewDistance);
    }

    //List<Transform> GetNearbyBoids(Boid agent)
    //{
    //    List<Transform> context = new List<Transform>();
    //    Collider[] colsContext = Physics.OverlapSphere(agent.transform.position, viewDistance);
    //    foreach(Collider col in colsContext)
    //    {
    //        if (col != agent.m_collider)
    //            context.Add(col.transform);
    //    }
    //    return context;
    //}
}
