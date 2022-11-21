using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class StateManager : MonoBehaviour
{
    public BaseAbstractState currentState;
    public BaseAbstractState currentRole;
    public BaseAbstractState previousState;
    public IdleState idle;
    public PlayingState playing;
    public WaitingState waiting;
    public FollowingPathState following;
    public UnassignedRole unassigned;
    public ExplorerRole explorer;
    public LearnerRole learner;

    //public Animator animator;
    public NavMeshAgent navMeshAgent;
    public List<Vector3> pathPoints;
    public int pathIndex;
    public Rigidbody creatureRb;
    public GameObject toy;
    public CameraController cameraController;
    public Vector3 foodStock;
    public Vector3 colonyEnter;

    float speed = 300f;
    public float Speed { get { return speed; } set { speed = Mathf.Min(value, 600f); } }

    private void Awake()
    {
        idle = new(this);
        playing = new(this);
        waiting = new(this);
        following = new(this);
        unassigned = new(this);
        explorer = new(this);
        learner = new(this);
        colonyEnter = GameObject.Find("ColonyGate").transform.position;
        foodStock = GameObject.Find("FoodStock").transform.position;

        //animator = GetComponentInChildren<Animator>();
        navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        cameraController = FindObjectOfType<CameraController>();
    }
    // Start is called before the first frame update
    void Start()
    {
        creatureRb = GetComponentInChildren<Rigidbody>();
        currentRole = unassigned;
        currentState = idle;
        currentState.OnStateEnter();
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnStateUpdate();
        currentRole.OnStateUpdate();
    }

    private void FixedUpdate()
    {
        currentState.OnStateFixedUpdate();
        currentRole.OnStateFixedUpdate();
    }

    public void SwitchState(State state)
    {
        //Debug.Log("Current state is : " + currentState.ToString());
        //Debug.Log("New state is : " + state.ToString());
        BaseAbstractState newState = FindState(state);
        currentState.OnStateExit();
        previousState = currentState;
        currentState = newState;
        currentState.OnStateEnter();
    }
    
    public void SwitchRole(Role role)
    {
        BaseAbstractState newRole = FindRole(role);
        currentRole.OnStateExit();
        //previousRole = currentRole;
        currentRole = newRole;
        currentRole.OnStateEnter();
    }

    BaseAbstractState FindState(State state)
    {
        BaseAbstractState selectedState = state.ToString() switch
        {
            "idle" => idle,
            "playing" => playing,
            "waiting" => waiting,
            "previousState" => previousState,
            "following" => following,
            _ => waiting,
        };
        return selectedState;
    }

    BaseAbstractState FindRole(Role role)
    {
        BaseAbstractState selectedRole = role.ToString() switch
        {
            "explorer" => explorer,
            "learner" => learner,
            //"feeder" => feeder,
            //"warrior" => warrior,
            "unassigned" => unassigned,
            _ => unassigned,
        };
        return selectedRole;
    }

    private void OnTriggerEnter(Collider trigger)
    {
        currentState.OnTriggerEnter(trigger);
        currentRole.OnTriggerEnter(trigger);
    }

    private void OnTriggerStay(Collider trigger)
    {
        currentState.OnTriggerStay(trigger);
        currentRole.OnTriggerStay(trigger);
    }

    private void OnTriggerExit(Collider trigger)
    {
        currentState.OnTriggerExit(trigger);
        currentRole.OnTriggerExit(trigger);

        if (trigger.CompareTag("BorderWall"))
        {
            SwitchState(State.waiting);
            Debug.Log("changing State");
        }
    }

}

public enum State
{
    idle,
    playing,
    waiting,
    previousState,
    following
}

public enum Role
{
    learner,
    explorer,
    feeder,
    warrior,
    unassigned
}


