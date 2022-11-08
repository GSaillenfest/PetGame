using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearningState : BaseAbstractState
{

    public override void OnStateEnter(StateManager context)
    {
        Debug.Log("Entering LearningState");
        context.transform.gameObject.GetComponent<Renderer>().material.color = Color.red;
    }


    public override void OnStateUpdate(StateManager context)
    {

    }

    public override void OnStateExit(StateManager context)
    {
        Debug.Log("Exiting LearningState");
    }

    public override void OnTriggerEnter(StateManager context, Collider trigger)
    {

    }
}
