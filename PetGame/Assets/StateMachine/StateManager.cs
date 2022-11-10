using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    BaseAbstractState currentState;
    public BaseAbstractState previousState;
    public IdleState idle;
    public PlayingState playing;
    public LearningState learning;
    public WaitingState waiting;

    public Animator animator;

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

    public void SwitchState(BaseAbstractState state)
    {
        currentState.OnStateExit();
        previousState = currentState;
        currentState = state;
        currentState.OnStateEnter();
    }

    private void OnTriggerEnter(Collider trigger)
    {
        currentState.OnTriggerEnter(trigger);
    }
}
