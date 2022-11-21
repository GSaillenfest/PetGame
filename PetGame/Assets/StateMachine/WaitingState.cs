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
        if (_context.currentRole.Equals(_context.learner))
        {
            timerWaiting = 0f;
        }
        timerWaiting = Random.Range(0.1f, 0.25f);
        //context.animator.SetBool("Walking", false);

    }


    public override void OnStateUpdate()
    {
        if (timerWaiting > 0f)
        {

            timerWaiting -= Time.deltaTime;


        }
        else if (timerWaiting < 0f)
        {
            SwitchState(State.previousState);
        }
    }

    public override void OnStateFixedUpdate()
    {

    }



    public override void OnStateExit()
    {

    }

    public override void OnTriggerEnter(Collider trigger)
    {

    }

    public override void OnTriggerStay(Collider trigger)
    {

    }

    public override void OnTriggerExit(Collider trigger)
    {

    }
}
