using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LearnerRole : BaseAbstractState
{
    public LearnerRole(StateManager _context) : base(_context) { }
    float timerLearning;


    public override void OnStateEnter()
    {
        Debug.Log("Entering LearnerRole");
        timerLearning = 5f;
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
                SwitchRole(Role.unassigned);
            }
            else
            {
                //Debug.Log(context.navMeshAgent.hasPath);
                context.pathPoints = pathPoints;
                SwitchState(State.following);
                SwitchRole(Role.explorer);
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

    }

    public override void OnTriggerEnter(Collider trigger)
    {

    }

    public override void OnTriggerStay(Collider trigger)
    {

    }

    public override void OnTriggerExit(Collider trigger)
    {

    }
}
