using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowingPathState : BaseAbstractState
{
    public FollowingPathState(StateManager _context) : base(_context) { }
    int pathIndex;

    public override void OnStateEnter()
    {
        Debug.Log("Entering FollowingPathState");
        //context.transform.gameObject.GetComponent<Renderer>().material.color = Color.Yellow;
        context.animator.SetBool("Walking", true);
        pathPoints = context.pathPoints;
        pathIndex = 0;
        context.navMeshAgent.CalculatePath(pathPoints[pathIndex], path);
        context.navMeshAgent.SetPath(path);
    }


    public override void OnStateUpdate()
    {
            MoveAlongPath();
    }

    // bugged for now.
    private void MoveAlongPath()
    {
        if (context.navMeshAgent.remainingDistance <= 5f)
        {
            if (pathIndex < pathPoints.Count - 1) pathIndex++;
            else pathIndex = 0;
            context.navMeshAgent.CalculatePath(pathPoints[pathIndex], path);
            context.navMeshAgent.SetPath(path);
        }
        for (int i = 0; i < path.corners.Length - 1; i++)
        Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red, 2f);

    }

    public override void OnStateFixedUpdate()
    {

    }

    public override void OnStateExit()
    {
        //Debug.Log("Exiting FollowingPathState");
    }

    public override void OnTriggerEnter(Collider trigger)
    {

    }

    public override void OnTriggerExit(Collider trigger)
    {
    }
}
