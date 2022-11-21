using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseAbstractState
{
    protected StateManager _context;
    protected List<Vector3> _pathPoints = new();
    protected int _pathIndex;
    protected NavMeshPath _path = new();

    public BaseAbstractState(StateManager context)
    {
        this._context = context;
    }

    public abstract void OnStateEnter();

    public abstract void OnStateUpdate();

    public abstract void OnStateFixedUpdate();

    public abstract void OnStateExit();

    public abstract void OnTriggerEnter(Collider trigger);
    public abstract void OnTriggerStay(Collider trigger);
    public abstract void OnTriggerExit(Collider trigger);

    public void SwitchState(State state)
    {
        _context.SwitchState(state);
    }

    public void SwitchRole(Role role)
    {
        _context.SwitchRole(role);
    }

    public void LookAt(Vector3 direction)
    {
        Quaternion lookRotation = Quaternion.LookRotation(direction.normalized, Vector3.up);
        _context.creatureRb.transform.rotation = lookRotation;
    }

    public void Die()
    {
        GameObject.Destroy(_context.gameObject);
    }

}


