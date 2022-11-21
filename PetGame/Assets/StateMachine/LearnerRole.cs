using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LearnerRole : BaseAbstractState
{
    public LearnerRole(StateManager _context) : base(_context) { }
    float timerLearning;
    bool isLearning;
    float timerFocus;


    public override void OnStateEnter()
    {
        timerFocus = 10f;
        timerLearning = 5f;
        _path = new();
        _pathPoints.Clear();
        _pathPoints.Add(_context.colonyEnter);
        SwitchState(State.waiting);
    }


    public override void OnStateUpdate()
    {
        timerFocus -= Time.deltaTime;
        if (timerFocus <= 0f)
        {
            SwitchRole(Role.unassigned);
            SwitchState(State.idle);
        }

        if (timerLearning > 0f)
        {
            if (Input.GetMouseButtonDown(0))
            {
                LearningPath();
                isLearning = true;
                SwitchState(State.waiting);
            }
            if (_pathPoints.Count > 1)
            {
                timerLearning -= Time.deltaTime;
            }
        }
        else
        {
            //if (_pathPoints.Count )
            //{
            //    _pathPoints.Clear();
            //    SwitchRole(Role.unassigned);
            //    SwitchState(State.idle);
            //}
            //else
            //{
            _context.pathPoints = _pathPoints;
            if (_context.navMeshAgent.remainingDistance >= 1f)
            {
                _context.navMeshAgent.CalculatePath(_pathPoints[0], _path);
                _context.navMeshAgent.SetPath(_path);
                SwitchState(State.following);
                SwitchRole(Role.explorer);
            }
            else
            {
            }
            //}
        }


    }

    void LearningPath()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 10000f, 128))
        {
            if (hit.collider.CompareTag("LearningZone"))
            {
                string name = hit.collider.gameObject.name;
                Vector3 pathPoint = new();
                switch (name)
                {
                    case "North":
                        pathPoint = Vector3.right * 40f;
                        break;
                    case "South":
                        pathPoint = -Vector3.right * 40f;
                        break;
                    case "West":
                        pathPoint = Vector3.forward * 40f;
                        break;
                    case "East":
                        pathPoint = -Vector3.forward * 40f;
                        break;
                    default:
                        break;
                }
                if (pathPoint != null)
                {
                    pathPoint += _pathPoints[_pathPoints.Count - 1];
                }
                _pathPoints.Add(pathPoint);
                _context.cameraController.SetCommandClickFeedback(Color.red, hit.point);
            }
        }

        //if (Physics.Raycast(ray, out hit, 10000f, 128))
        //{
        //    Vector3 mousePos = hit.point;
        //    mousePos.y = context.transform.position.y;
        //    pathPoints.Add(mousePos);
        //    Debug.DrawRay(ray.origin, ray.direction * (ray.origin - hit.point).magnitude, Color.green, 10f);
        //    timerLearning = 5f;

        //    Vector3 direction = mousePos - context.transform.position;
        //    LookAt(direction);
        //}

        Debug.Log(_pathPoints.Count);
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
        if (trigger.CompareTag("LearningZone"))
        {
            if (!isLearning)
            {
                SwitchRole(Role.unassigned);
            }
        }
    }
}
