using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAbstractState
{


    public abstract void OnStateEnter(StateManager context);

    public abstract void OnStateUpdate(StateManager context);

    public abstract void OnStateExit(StateManager context);

    public abstract void OnTriggerEnter(StateManager context, Collider trigger);

}
