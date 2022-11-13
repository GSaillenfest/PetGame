using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAbstractState
{
    protected StateManager context;
    public BaseAbstractState(StateManager _context)
    {
        context = _context;
        
    }

    public abstract void OnStateEnter();

    public abstract void OnStateUpdate();

    public abstract void OnStateFixedUpdate();

    public abstract void OnStateExit();

    public abstract void OnTriggerEnter(Collider trigger);
    public abstract void OnTriggerExit(Collider trigger);

    public void SwitchState(State state)
    {
        context.SwitchState(state);
    }

    public void LookAt(Vector3 direction)
    {
        Quaternion lookRotation = Quaternion.LookRotation(direction.normalized, Vector3.up);
        context.creatureRb.transform.rotation = lookRotation;
    }

}


