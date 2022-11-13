using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

// subState ?
public class WaitingState : BaseAbstractState
{
    public WaitingState(StateManager _context) : base(_context) { }

    float timerWaiting;

    public override void OnStateEnter()
    {
        Debug.Log("Entering WaitingState");
        timerWaiting = Random.Range(0.5f, 2f);
        context.animator.SetBool("Walking", false);

    }


    public override void OnStateUpdate()
    {
        if (timerWaiting > 0f)
        {

            timerWaiting -= Time.deltaTime;


        }
        else
        {
            SwitchState(State.previousState);
        }
    }

    public override void OnStateFixedUpdate()
    {

    }



    public override void OnStateExit()
    {
        Debug.Log("Exiting WaitingState");
    }

    public override void OnTriggerEnter(Collider trigger)
    {

    }

    public override void OnTriggerExit(Collider trigger)
    {

    }
}
