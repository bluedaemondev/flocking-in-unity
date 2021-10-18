using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BoidState
{
    Idle,
    Patrol,
    SearchingFood,
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
    public Animator m_animator;
    public IMovement currentMovement;
    
    FiniteStateMachine _fsm;



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


}