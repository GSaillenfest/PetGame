using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearningState : BaseAbstractState
{
    public LearningState(StateManager _context) : base(_context) { }

    List<Vector3> path = new();
    float timerLearning;
    bool pathLearnt;

    public override void OnStateEnter()
    {
        Debug.Log("Entering LearningState");
        context.transform.gameObject.GetComponent<Renderer>().material.color = Color.red;
        timerLearning = 2f;
    }


    public override void OnStateUpdate()
    {
        if (timerLearning > 0f)
        {
            if (path.Count == 0)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Vector3 mousePos = Vector3.zero;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    Debug.DrawRay(ray.origin, ray.direction);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        mousePos = hit.point;
                    }
                    mousePos.y = context.transform.position.y;
                    LearningPath(mousePos);

                    Vector3 direction = mousePos - context.transform.position;
                    direction.y = 0.5f;
                    LookAt(direction);
                }
            }
            else
            {
                timerLearning -= Time.deltaTime;

                if (Input.GetMouseButtonDown(0))
                {
                    Vector3 mousePos = Vector3.zero;
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    Debug.DrawRay(ray.origin, ray.direction);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        mousePos = hit.point;
                    }
                    mousePos.y = context.transform.position.y;
                    LearningPath(mousePos);

                    Vector3 direction = mousePos - context.transform.position;
                    direction.y = 0.5f;
                    LookAt(direction);
                }
            }
        }
        else
        {
            if (path.Count < 2)
            {
                path.Clear();
                context.SwitchState(context.idle);
            }
            else DoPath();
        }
    }

    public override void OnStateFixedUpdate()
    {

    }

    private void DoPath()
    {
        Debug.Log("Switch to new state");
        context.SwitchState(context.idle);
    }

    public override void OnStateExit()
    {
        Debug.Log("Exiting LearningState");
    }

    public override void OnTriggerEnter(Collider trigger)
    {

    }

    void LearningPath(Vector3 newPathPoint)
    {
        path.Add(newPathPoint);
        timerLearning = 2f;
        Debug.Log(path[path.Count - 1]);
    }

    private void LookAt(Vector3 direction)
    {
        Quaternion lookRotation = Quaternion.LookRotation(direction, Vector3.up);
        //context.creatureRb.MoveRotation(lookRotation);
        context.transform.forward = direction.normalized;
    }
}
