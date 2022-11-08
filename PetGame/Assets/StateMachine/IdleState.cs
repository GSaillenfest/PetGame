using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class IdleState : BaseAbstractState
{

    Vector3 randomPos;

    public override void OnStateEnter(StateManager context)
    {
        speed = 300f;
        Debug.Log("Entering IdleState");
        context.transform.gameObject.GetComponent<Renderer>().material.color = Color.green;
        RandomPosition(context);
    }


    public override void OnStateUpdate(StateManager context)
    {
        MoveToRandomPosition(context, randomPos);
    }

    public override void OnStateExit(StateManager context)
    {
        Debug.Log("Exiting IdleState");
    }

    void RandomPosition(StateManager context)
    {
        randomPos = context.transform.position + Random.insideUnitSphere * 5f;
        randomPos.y = 0.5f;
        Debug.Log(randomPos);
    }
    private void MoveToRandomPosition(StateManager context, Vector3 destination)
    {
        Vector3 direction = destination - context.creatureRb.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction, Vector3.up);
        context.creatureRb.MoveRotation(lookRotation);
        if (direction.magnitude > 0.2f)
        {
            context.creatureRb.velocity = direction.normalized * Time.deltaTime * speed;
        }
        else RandomPosition(context);
    }

    public override void OnTriggerEnter(StateManager context, Collider trigger)
    {
        if (trigger.gameObject.CompareTag("Toy"))
        {
            context.toy = trigger.gameObject;
            context.SwitchState(context.playing);
        }
    }
}
