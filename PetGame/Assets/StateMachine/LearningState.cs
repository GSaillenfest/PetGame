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
        //context.transform.gameObject.GetComponent<Renderer>().material.color = Color.red;
        timerLearning = 2f;
        context.animator.SetBool("Walking", false);

    }


    public override void OnStateUpdate()
    {
        if (timerLearning > 0f)
        {
            if (path.Count == 0)
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
            if (path.Count < 2)
            {
                path.Clear();
                SwitchState(State.idle);
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
        foreach (Vector3 points in path)
        {
            GameObject pathPoint = GameObject.CreatePrimitive(PrimitiveType.Cube);
            pathPoint.transform.position = points; 
            pathPoint.transform.localScale *= 0.2f;
            pathPoint.GetComponent<Renderer>().material.color = Color.red;
        }
        SwitchState(State.idle);
    }

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
            path.Add(mousePos);
            timerLearning = 2f;

            Vector3 direction = mousePos - context.transform.position;
            LookAt(direction);
        }

        Debug.Log(path[path.Count - 1]);
    }

    public override void OnTriggerExit(Collider trigger)
    {
    }
}
