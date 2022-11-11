using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateManager : MonoBehaviour
{
    BaseAbstractState currentState;
    public BaseAbstractState previousState;
    public IdleState idle;
    public PlayingState playing;
    public LearningState learning;
    public WaitingState waiting;

    public Animator animator;
    public NavMeshAgent navMeshAgent;

    public Rigidbody creatureRb;

    public GameObject toy;
    float speed = 300f;
    public float Speed { get { return speed; } set { speed = Mathf.Min(value, 600f); } }

    private void Awake()
    {
        idle = new(this);
        playing = new(this);
        learning = new(this);
        waiting = new(this);

        animator = GetComponentInChildren<Animator>();
        navMeshAgent = GetComponentInChildren<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        creatureRb = GetComponentInChildren<Rigidbody>();
        currentState = idle;
        currentState.OnStateEnter();
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnStateUpdate();
    }

    private void FixedUpdate()
    {
        currentState.OnStateFixedUpdate();
    }

    public void SwitchState(State state)
    {
        BaseAbstractState newState = FindState(state);
        currentState.OnStateExit();
        previousState = currentState;
        currentState = newState;
        currentState.OnStateEnter();
    }

    BaseAbstractState FindState(State state)
    {
        BaseAbstractState selectedState = state.ToString() switch
        {
            "idle" => idle,
            "playing" => playing,
            "waiting" => waiting,
            "learning" => learning,
            "previousState" => previousState,
            _ => waiting,
        };
        return selectedState;
    }

    private void OnTriggerEnter(Collider trigger)
    {
        currentState.OnTriggerEnter(trigger);
    }

    private void OnTriggerExit(Collider trigger)
    {
        currentState.OnTriggerExit(trigger);
    }

}

public enum State
{
    idle,
    playing,
    waiting,
    learning,
    previousState
}


