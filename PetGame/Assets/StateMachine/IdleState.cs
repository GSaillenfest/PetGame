using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class IdleState : BaseAbstractState
{

    Vector3 randomPos;
    float timer = 3f;
    public IdleState(StateManager _context) : base(_context) { }


    public override void OnStateEnter()
    {
        Debug.Log("Entering IdleState");
        //context.Speed = 100f;
        context.navMeshAgent.speed = 5f;
        //context.transform.gameObject.GetComponent<Renderer>().material.color = Color.green;
        RandomPosition();
        //context.animator.SetBool("Walking", true);
        timer = 3f;
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
        Vector2 randomInsideCircle = (Random.insideUnitCircle * 5f);
        randomInsideCircle += randomInsideCircle.normalized * 5;
        randomPos = context.transform.position + new Vector3(randomInsideCircle.x, 0, randomInsideCircle.y);
        randomPos.y = 0;
    }

    private void MoveTo(Vector3 destination)
    {
        Vector3 direction = destination - context.transform.position;
        direction.y = context.transform.position.y;

        LookAt(direction);

        if (timer < 0f || direction.magnitude <= 0.1f)
        {
            timer = 3f;
            RandomPosition();
            SwitchState(State.waiting);
        }
        else if (direction.magnitude > 0.1f)
        {
            context.navMeshAgent.SetDestination(destination);
            timer -= Time.deltaTime;
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

    public override void OnTriggerStay(Collider trigger)
    {

    }

    public override void OnTriggerExit(Collider trigger)
    {

    }
}
