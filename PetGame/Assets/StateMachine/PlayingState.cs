using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingState : BaseAbstractState
{

    GameObject toy;

    public override void OnStateEnter(StateManager context)
    {
        speed = 600f;
        toy = context.toy;
        Debug.Log("Entering PlayingState");
        context.transform.gameObject.GetComponent<Renderer>().material.color = Color.blue;
    }
    

    public override void OnStateUpdate(StateManager context)
    {
        Vector3 destination = toy.transform.position;
        MoveTo(context, destination);
    }

    public override void OnStateExit(StateManager context)
    {
        Debug.Log("Exiting PlayingState");
    }

    public override void OnTriggerEnter(StateManager context, Collider trigger)
    {

    }

    private void MoveTo(StateManager context, Vector3 destination)
    {
        Vector3 direction = destination - context.creatureRb.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction, Vector3.up);
        context.creatureRb.MoveRotation(lookRotation);
        if (direction.magnitude > 0.2f)
        {
            context.creatureRb.velocity = direction.normalized * Time.deltaTime * speed;
        }
        else ;
    }
}