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
        //context.transform.gameObject.GetComponent<Renderer>().material.color = Color.green;
        RandomPosition();
        context.animator.SetBool("Walking", true);
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
        randomPos.y = 0;
        Debug.Log(randomPos);
    }

    private void MoveToRandomPosition(Vector3 destination)
    {
        Vector3 direction = destination - context.transform.position;
        direction.y = context.transform.position.y;

        LookAt(direction);

        Debug.Log(direction.magnitude);
        if (direction.magnitude > 0.5f)
        {
            context.creatureRb.velocity = context.Speed * Time.fixedDeltaTime * direction.normalized;
        }
        else
        {
            RandomPosition();
            SwitchState(context.waiting);
        }
    }

    public override void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.CompareTag("Toy"))
        {
            context.toy = trigger.gameObject;
            SwitchState(context.playing);
        }
    }
}
