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
        //context.Speed = 100f;
        context.navMeshAgent.speed = 3.5f;
        //context.transform.gameObject.GetComponent<Renderer>().material.color = Color.green;
        RandomPosition();
        context.animator.SetBool("Walking", true);
    }


    public override void OnStateUpdate()
    {

    }

    public override void OnStateFixedUpdate()
    {
        MoveTo(randomPos);
    }

    public override void OnStateExit()
    {
        Debug.Log("Exiting IdleState");
    }

    void RandomPosition()
    {
        randomPos = context.transform.position + Random.insideUnitSphere * 10f;
        if ((randomPos - context.transform.position).magnitude < 5f) randomPos *= 2f;
        if (Mathf.Abs(randomPos.x) > 72) randomPos.x = 68f;
        if (Mathf.Abs(randomPos.z) > 72) randomPos.z = 68f;
        randomPos.y = 0;
    }

    private void MoveTo(Vector3 destination)
    {
        Vector3 direction = destination - context.transform.position;
        direction.y = context.transform.position.y;

        LookAt(direction);

        if (direction.magnitude > 0.5f)
        {
            context.navMeshAgent.SetDestination(destination);
        }
        else
        {
            RandomPosition();
            SwitchState(State.waiting);
        }
    }

    public override void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.CompareTag("Toy"))
        {
            context.toy = trigger.gameObject;
            SwitchState(State.playing);
        }
    }

    public override void OnTriggerExit(Collider trigger)
    {
        
    }
}
