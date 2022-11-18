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
        Debug.Log("Entering LearningState");
        //context.transform.gameObject.GetComponent<Renderer>().material.color = Color.red;
        timerLearning = 5f;
        context.animator.SetBool("Walking", false);
        path = new();
    }


    public override void OnStateUpdate()
    {
        if (timerLearning > 0f)
        {
            if (Input.GetMouseButtonDown(0))
            {
                LearningPath();
            }
            if (pathPoints.Count != 0)
            {
                timerLearning -= Time.deltaTime;
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

        if (Physics.Raycast(ray, out hit, 10000f, 128))
        {
            Vector3 mousePos = hit.point;
            mousePos.y = context.transform.position.y;
            pathPoints.Add(mousePos);
            context.cameraController.SetCommandClickFeedback(Color.red, mousePos);
            Debug.DrawRay(ray.origin, ray.direction * (ray.origin - hit.point).magnitude, Color.green, 10f);
            timerLearning = 5f;

            Vector3 direction = mousePos - context.transform.position;
            LookAt(direction);
        }

        Debug.Log(pathPoints.Count);
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
