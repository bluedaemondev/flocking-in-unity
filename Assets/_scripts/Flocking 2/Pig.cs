using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BoidState
{
    Idle,
    Patrol,
    EvadingHunter,
    ChasingFood,
    Eating,
    Dead
}
/// <summary>
/// Pig controla la logica de animacion y contiene una referencia a cada tipo de movimiento
/// Tiene un metodo para cambiar de estado, y su FSM propia
/// 
/// </summary>
public class Pig : MonoBehaviour
{
    public BoidState CurrentState;

    private Animator m_animator;
    private PigFiniteStateMachine _fsm;

    [Header("Animation params")]
    public string movingPatrolBool = "isMoving";
    public string avoidHunterTrigger = "evadeHunter";
    public string chaseFood = "chaseFood";
    public string eating = "eating";
    public string deadTrigger = "dying";


    // Pasar a Imovement

    // max total speed
    [SerializeField]
    private float maxSpeed;

    // max force applyed by every steering done
    [SerializeField]
    private float maxForce;

    // target view distance, flock neighbors, pickups
    [SerializeField]
    private float viewDistance;

    [Header("Weighing")]
    [SerializeField, Range(0.1f, 1f)]
    private float separationWeight;
    [SerializeField, Range(0.1f, 1f)]
    private float cohesionWeight;
    [SerializeField, Range(0.1f, 1f)]
    private float alignWeight;
    [SerializeField, Range(0.1f, 1f)]
    private float seekWeight;

    public Vector3 _velocity;


    private void Awake()
    {
        this._fsm = new PigFiniteStateMachine();
        this._fsm.AddState(BoidState.Idle, new BlankState());
        this._fsm.AddState(BoidState.Patrol, new BlankState());
        this._fsm.AddState(BoidState.EvadingHunter, new EvadingHunterState(transform, FindObjectOfType<Hunter>().transform, this._fsm, this, maxSpeed * 1.15f, maxForce));
        this._fsm.AddState(BoidState.ChasingFood, new BlankState());
        this._fsm.AddState(BoidState.Eating, new BlankState());
        this._fsm.AddState(BoidState.Dead, new BlankState());

        this.m_animator = GetComponent<Animator>();

        this._fsm.ChangeState(BoidState.EvadingHunter);

    }

    public void ApplyForce(Vector3 force)
    {
        _velocity = Vector3.ClampMagnitude(_velocity + force, maxSpeed);
        this.transform.position += _velocity;
    }



}