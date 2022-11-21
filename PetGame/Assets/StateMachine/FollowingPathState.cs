using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowingPathState : BaseAbstractState
{
    public FollowingPathState(StateManager _context) : base(_context) { }

    public override void OnStateEnter()
    {
        //context.transform.gameObject.GetComponent<Renderer>().material.color = Color.Yellow;
       // context.animator.SetBool("Walking", true);
        _pathPoints = _context.pathPoints;
        _pathIndex = 0;
        _context.navMeshAgent.CalculatePath(_pathPoints[_pathIndex], _path);
        _context.navMeshAgent.SetPath(_path);
    }


    public override void OnStateUpdate()
    {
            MoveAlongPath();
    }

    private void MoveAlongPath()
    {
        if (_context.navMeshAgent.remainingDistance <= 1f)
        {
            _pathPoints = _context.pathPoints;
            if (_pathIndex < _pathPoints.Count - 1)
            {
                _pathIndex++;
                _context.pathIndex = _pathIndex;
            }
            else
            {
                _pathPoints.Reverse();
                _pathIndex = 0;
                _context.pathIndex = _pathIndex;
            }
            if (_context.navMeshAgent.CalculatePath(_pathPoints[_pathIndex], _path))
            {
                _context.navMeshAgent.SetPath(_path);
            }
            else
            {
                if (_pathIndex < _pathPoints.Count - 1)
                {
                    _pathIndex++;
                    _context.pathIndex = _pathIndex;
                }
                else
                {
                    _pathIndex = 0;
                    _context.pathIndex = _pathIndex;
                }
            }
        }
        for (int i = 0; i < _path.corners.Length - 1; i++)
        Debug.DrawLine(_path.corners[i], _path.corners[i + 1], Color.red, 2f);
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
