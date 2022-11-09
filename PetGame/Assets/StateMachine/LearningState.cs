using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearningState : BaseAbstractState
{

    List<Vector3> path = new();
    float timerLearning;

    public override void OnStateEnter(StateManager context)
    {
        Debug.Log("Entering LearningState");
        context.transform.gameObject.GetComponent<Renderer>().material.color = Color.red;
    }


    public override void OnStateUpdate(StateManager context)
    {
        if (path.Count == 0)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                LookAt();
            }
        }
    }

    public override void OnStateExit(StateManager context)
    {
        Debug.Log("Exiting LearningState");
    }

    public override void OnTriggerEnter(StateManager context, Collider trigger)
    {

    }

    void LearningPath()
    {

    }

    void LookAt()
    {
        context.
    }
}
