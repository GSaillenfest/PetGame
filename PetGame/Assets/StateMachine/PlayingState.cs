using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingState : BaseAbstractState
{

    GameObject toy;
    public PlayingState(StateManager _context) : base(_context) { }

    public override void OnStateEnter()
    {
        //Debug.Log("Entering PlayingState");
        //context.Speed = 20f;
        _context.navMeshAgent.speed = 7f;
        toy = _context.toy;
        //context.transform.gameObject.GetComponent<Renderer>().material.color = Color.blue;
        //context.animator.SetBool("Walking", true);
    }


    public override void OnStateUpdate()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Vector3.zero;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray.origin, ray.direction, out hit, 10000f, 128))
            {
                mousePos = hit.point;
                Debug.DrawRay(ray.origin, ray.direction * (ray.origin-hit.point).magnitude, Color.green, 10f);
                _context.cameraController.SetCommandClickFeedback(Color.green, mousePos);
            }


            //Debug.Log((mousePos - context.transform.position).magnitude);
            //Debug.Log(context.transform.position);
            //Debug.Log(mousePos);

            if ((mousePos - _context.transform.position).magnitude <= 10f)
            {
                //SwitchState(State.learning);
            }
        }
    }

    public override void OnStateFixedUpdate()
    {
        Vector3 destination = toy.transform.position;
        MoveTo(destination);
    }

    public override void OnStateExit()
    {
        //Debug.Log("Exiting PlayingState");
        _context.Speed = 300f;
    }

    public override void OnTriggerEnter(Collider trigger)
    {

    }

    public override void OnTriggerStay(Collider trigger)
    {

    }

    private void MoveTo(Vector3 destination)
    {
        Vector3 direction = destination - _context.creatureRb.position;
        LookAt(direction);
        if (direction.magnitude > 0.5f)
        {
            _context.navMeshAgent.SetDestination(destination);
        }

    }

    public override void OnTriggerExit(Collider trigger)
    {
        if (trigger.gameObject.Equals(_context.toy))
        {
            _context.toy = null;
            SwitchState(State.idle);
        }
    }
}
