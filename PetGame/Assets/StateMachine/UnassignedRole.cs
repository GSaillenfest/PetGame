using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class UnassignedRole : BaseAbstractState
{

    public UnassignedRole(StateManager _context) : base(_context) { }


    public override void OnStateEnter()
    {

    }


    public override void OnStateUpdate()
    {

    }

    public override void OnStateFixedUpdate()
    {

    }

    public override void OnStateExit()
    {
    }

    public override void OnTriggerEnter(Collider trigger)
    {
        if (trigger.CompareTag("LearningZone"))
        {
            SwitchRole(Role.learner);
        }
    }

    public override void OnTriggerStay(Collider trigger)
    {
    }

    public override void OnTriggerExit(Collider trigger)
    {

    }
}
