using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LearningState : BaseAbstractState
{
    public LearningState(StateManager _context) : base(_context) { }

    List<Vector3> pathPoints = new();
    NavMeshPath path;
    float timerLearning;
    bool pathLearnt;
    int pathIndex;

    public override void OnStateEnter()
    {
        Debug.Log("Entering LearningState");
        //context.transform.gameObject.GetComponent<Renderer>().material.color = Color.red;
        timerLearning = 2f;
        context.animator.SetBool("Walking", false);
        path = new();
    }


    public override void OnStateUpdate()
    {
        if (timerLearning > 0f)
        {
            if (pathPoints.Count == 0)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    LearningPath();

                }
            }
            else
            {
                timerLearning -= Time.deltaTime;

                if (Input.GetMouseButtonDown(0))
                {
                    LearningPath();
                }
            }
        }
        else
        {
            if (pathPoints.Count < 2)
            {
                pathPoints.Clear();
                SwitchState(State.idle);
            }
            else
            {
                pathLearnt = true;
                context.navMeshAgent.CalculatePath(pathPoints[0], path);
                context.navMeshAgent.SetPath(path);
            }
        }

        if (pathLearnt)
        {
            MoveAlongPath();
        }
    }

    // bugged for now.
    private void MoveAlongPath()
    {
        if (context.navMeshAgent.remainingDistance <= 0.01f)
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

    //private void DoPath()
    //{
    //    Debug.Log("Switch to new state");

    //    context.navMeshAgent.SetPath(path);
    //    pathLearnt = true;
    //    //SwitchState(State.idle);
    //}

    public override void OnStateExit()
    {
        Debug.Log("Exiting LearningState");
    }

    public override void OnTriggerEnter(Collider trigger)
    {

    }

    void LearningPath()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 mousePos = hit.point;
            mousePos.y = context.transform.position.y;
            pathPoints.Add(mousePos);
            timerLearning = 2f;

            Vector3 direction = mousePos - context.transform.position;
            LookAt(direction);
        }

        Debug.Log(pathPoints[pathPoints.Count - 1]);
    }

    public override void OnTriggerExit(Collider trigger)
    {

    }
}
