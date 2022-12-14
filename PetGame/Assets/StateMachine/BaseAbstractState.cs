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

}
