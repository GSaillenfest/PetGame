using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ExplorerRole : BaseAbstractState
{
    float hunger = 100f;
    bool hasFood;

    public ExplorerRole(StateManager _context) : base(_context) { }


    public override void OnStateEnter()
    {

    }


    public override void OnStateUpdate()
    {
        hunger -= Time.deltaTime;

        //if (hunger < 30f)
        //{
        //    _context.navMeshAgent.SetDestination(_context.colonyEnter);
        //}
        if (hunger < 0f)
        {
            Die();
        }
    }

    public override void OnStateFixedUpdate()
    {

    }

    public override void OnStateExit()
    {

    }

    public override void OnTriggerEnter(Collider trigger)
    {
        if (trigger.CompareTag("Food") && !hasFood)
        {
            Debug.Log("Food");
            hunger += 20f;
            BringBackFood(trigger.transform.position);
        }

        if (trigger.CompareTag("Enter") && _context.pathIndex != 0)
        {
            _context.pathPoints.Add(_context.foodStock);

        }

        if (trigger.CompareTag("FoodStock"))
        {
            if (!hasFood)
            {
                SwitchRole(Role.unassigned);
                SwitchState(State.idle);
            }
            else
            {
                hunger = 100f;
                trigger.gameObject.GetComponent<FoodStock>().StackFood(_context.loot);
                hasFood = false;
            }
        }
    }

    private void BringBackFood(Vector3 food)
    {
        _pathPoints = _context.pathPoints;
        _pathPoints.RemoveRange(_context.pathIndex, _pathPoints.Count - _context.pathIndex);
        _pathPoints.Add(food);
        _pathPoints.Reverse();
        _pathPoints.Add(_context.foodStock);
        _context.pathPoints = _pathPoints;
        SwitchState(State.waiting);
    }

    public override void OnTriggerStay(Collider trigger)
    {

    }

    public override void OnTriggerExit(Collider trigger)
    {

    }

    public bool TransportFood()
    {
        Debug.Log("Food");
        if (!hasFood)
        {
            _context.loot = GameObject.Instantiate(_context.crumbPrefab, _context.InstantiatePoint.transform.position, Quaternion.identity, _context.InstantiatePoint.transform);
            hasFood = true;
        }
        return hasFood;
    }

}
