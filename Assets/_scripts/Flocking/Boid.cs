using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Boid : MonoBehaviour
{
    private Vector3 _velocity;
    internal Collider m_collider;

    // max total speed
    [SerializeField]
    private float maxSpeed;
    // max force applyed by every steering done
    [SerializeField]
    private float maxForce;
    // target view distance, flock neighbors, pickups
    [SerializeField]
    private float viewDistance;

    public BoundedArea areaWalk;

    [Header("Weighing")]
    [SerializeField, Range(0.1f, 1f)]
    private float separationWeight;
    [SerializeField, Range(0.1f, 1f)]
    private float cohesionWeight;
    [SerializeField, Range(0.1f, 1f)]
    private float alignWeight;
    [SerializeField, Range(0.1f, 1f)]
    private float seekWeight;

    public bool useGlobal = true;

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
        areaWalk = FindObjectOfType<BoundedArea>();
    }
    // Update is called once per frame
    void Update()
    {
        CheckGlobalValues();
        
        FoodFinder();

        ApplyForce(FlockingBrain.Instance.CalculateCohesion(this) * cohesionWeight);
        ApplyForce(FlockingBrain.Instance.CalculateSeparation(this) * separationWeight);
        ApplyForce(FlockingBrain.Instance.CalculateAlign(this) * alignWeight);


        if (currentlySeeking != null)
        {           
            var distance = (currentlySeeking.transform.position - transform.position).magnitude;

            if (distance <= 5f)
            {
                //_velocity = Vector3.zero;
                ApplyForce(FlockingBrain.Instance.CalculateSeek(currentlySeeking.transform.position, this) * seekWeight);
                ApplyForce(FlockingBrain.Instance.CalculateArrive(currentlySeeking, 5, this));

                if(distance <= 1.5f)
                {
                    currentlySeeking.GetComponent<Pickup>().BeEaten(this);

                    currentlySeeking = null;
                }
            }
        }



        areaWalk.CheckBounds(transform);

        _velocity.y = 0;

        transform.position += Velocity * Time.deltaTime;
        transform.forward = Velocity.normalized;
    }

    void CheckGlobalValues()
    {
        if (!useGlobal)
            return;

        this.cohesionWeight = FlockingBrain.Instance.GlobalCohesionWeight;
        separationWeight = FlockingBrain.Instance.GlobalSeparationWeight;
        alignWeight = FlockingBrain.Instance.GlobalAlignWeight;

        maxForce = FlockingBrain.Instance.GlobalMaxForce;
        maxSpeed = FlockingBrain.Instance.GlobalMaxSpeed;
        viewDistance  = FlockingBrain.Instance.ViewDistance;

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
