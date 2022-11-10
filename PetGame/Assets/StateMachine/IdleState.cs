using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class IdleState : BaseAbstractState
{

    Vector3 randomPos;
    public IdleState(StateManager _context) : base(_context) { }


    public override void OnStateEnter()
    {
        Debug.Log("Entering IdleState");
        context.Speed = 100f;
        context.transform.gameObject.GetComponent<Renderer>().material.color = Color.green;
        RandomPosition();
    }


    public override void OnStateUpdate()
    {

    }

    public override void OnStateFixedUpdate()
    {
        MoveToRandomPosition(randomPos);
    }

    public override void OnStateExit()
    {
        Debug.Log("Exiting IdleState");
    }

    void RandomPosition()
    {
        randomPos = context.transform.position + Random.insideUnitSphere * 5f;
        randomPos.y = 0.5f;
    }
    private void MoveToRandomPosition(Vector3 destination)
    {
        Vector3 direction = destination - context.creatureRb.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction, Vector3.up);
        context.creatureRb.MoveRotation(lookRotation);
        if (direction.magnitude > 0.2f)
        {
            context.creatureRb.velocity = direction.normalized * Time.fixedDeltaTime * context.Speed;
        }
        else RandomPosition();
    }

    public override void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.CompareTag("Toy"))
        {
            context.toy = trigger.gameObject;
            context.SwitchState(context.playing);
        }
    }
}
