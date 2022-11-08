using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    BaseAbstractState currentState;
    public IdleState idle = new();
    public PlayingState playing = new();
    public LearningState learning = new();

    public Rigidbody creatureRb;

    public GameObject toy;

    // Start is called before the first frame update
    void Start()
    {
        creatureRb = GetComponent<Rigidbody>();
        currentState = idle;
        currentState.OnStateEnter(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnStateUpdate(this);  
    }

    public void SwitchState(BaseAbstractState state)
    {
        currentState.OnStateExit(this);
        currentState = state;
        currentState.OnStateEnter(this);
    }

    private void OnTriggerEnter(Collider trigger)
    {
        currentState.OnTriggerEnter(this, trigger);
    }
}
