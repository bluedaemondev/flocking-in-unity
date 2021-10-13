using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : MonoBehaviour
{
    [SerializeField]
    private FiniteStateMachine _fsm;
    [SerializeField]
    private HunterEnum currentState;

    [SerializeField]
    private List<Transform> nodes;
    [SerializeField]
    private float patrolMovementSpeed;
    [SerializeField]
    private float chaseMovementSpeed;

    [SerializeField]
    private float arriveRadius = 2f;
    [SerializeField]
    private LayerMask pigMask;

    // Start is called before the first frame update
    void Start()
    {
        _fsm = new FiniteStateMachine();

        _fsm.AddState(HunterEnum.Idle, new IdleState(this._fsm));
        _fsm.AddState(HunterEnum.Patrol, new PatrolState(this._fsm, transform, nodes,pigMask ,patrolMovementSpeed));
        _fsm.AddState(HunterEnum.Chasing, new SeekState(this._fsm, transform, FlockingBrain.Instance.GetRandomBoid(), arriveRadius));
        //_fsm.AddState(HunterEnum.Attacking, new AttackingState())




        _fsm.ChangeState(currentState);
    }

    // Update is called once per frame
    void Update()
    {
        _fsm.OnUpdate();
    }
}
