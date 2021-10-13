using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    private Vector3 _velocity;

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
    BoundedArea contactArea;

    [SerializeField]
    float forwardDistance;

    [SerializeField]
    GameObject currentlySeeking;
    [SerializeField]
    LayerMask whatIsFood;

    public Vector3 Velocity { get => _velocity; }
    public float MaxForce { get => maxForce; }

    // Start is called before the first frame update
    void Start()
    {
        FlockingBrain.Instance.AddBoid(this);

        Vector3 randomForce = new Vector3(Random.Range(-2, 2), 0, Random.Range(-2, 2));
        randomForce.Normalize();
        randomForce *= maxSpeed;

        randomForce = Vector3.ClampMagnitude(randomForce, MaxForce);

        ApplyForce(randomForce);
    }

    // Update is called once per frame
    void Update()
    {
        //contactArea.CheckBounds(this.transform);
        FoodFinder();

        UpdateValues();

        ApplyForce(FlockingBrain.Instance.CalculateCohesion(this) * cohesionWeight);
        ApplyForce(FlockingBrain.Instance.CalculateSeparation(this) * separationWeight);
        ApplyForce(FlockingBrain.Instance.CalculateAlign(this) * alignWeight);
        
        if (currentlySeeking != null)
            ApplyForce(FlockingBrain.Instance.CalculateSeek(currentlySeeking.transform.position, this) * seekWeight);
        
        _velocity.y = 0;

        transform.position += Velocity * Time.deltaTime;
        transform.forward = Velocity.normalized;
    }

    void UpdateValues()
    {
        separationWeight = FlockingBrain.Instance.GlobalSeparationWeight;
        cohesionWeight = FlockingBrain.Instance.GlobalCohesionWeight;
        alignWeight = FlockingBrain.Instance.GlobalAlignWeight;
        maxForce = FlockingBrain.Instance.GlobalMaxForce;
        maxSpeed = FlockingBrain.Instance.GlobalMaxSpeed;
        viewDistance = FlockingBrain.Instance.ViewDistance;
    }

    void ApplyForce(Vector3 force)
    {
        this._velocity += force;
        this._velocity = Vector3.ClampMagnitude(this._velocity, this.maxSpeed);
    }

    

    void FoodFinder()
    {
        var aux = Physics.SphereCastAll(transform.position, viewDistance, Vector3.zero, viewDistance, whatIsFood.value);
        if (aux.Length > 0)
        {
            Debug.Log(string.Format("{0} pickups in range, following {1}", aux.Length, aux[0].transform.name));
            currentlySeeking = aux[0].transform.gameObject;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, viewDistance);
    }
}
