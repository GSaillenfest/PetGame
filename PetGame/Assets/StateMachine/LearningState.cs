using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LearningState : BaseAbstractState
{
    public LearningState(StateManager _context) : base(_context) { }
    float timerLearning;

    public override void OnStateEnter()
    {
        //Debug.Log("Entering LearningState");
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
                //Debug.Log(context.navMeshAgent.hasPath);
                pathPoints.Clear();
                SwitchState(State.previousState);
            }
            else
            {
                //Debug.Log(context.navMeshAgent.hasPath);
                context.pathPoints = pathPoints;
                SwitchState(State.following);
            }
        }
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

        //Debug.Log(pathPoints.Count);
    }

    public override void OnStateFixedUpdate()
    {

    }

    
    public override void OnStateExit()
    {
        //Debug.Log("Exiting LearningState");
    }

    public override void OnTriggerEnter(Collider trigger)
    {

    }

    public override void OnTriggerExit(Collider trigger)
    {

    }
}
