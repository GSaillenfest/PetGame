using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingState : BaseAbstractState
{

    GameObject toy;
    public PlayingState(StateManager _context) : base(_context) { }

    public override void OnStateEnter()
    {
        Debug.Log("Entering PlayingState");
        context.Speed = 50f;
        toy = context.toy;
        context.transform.gameObject.GetComponent<Renderer>().material.color = Color.blue;
    }
    

    public override void OnStateUpdate()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Vector3.zero;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray.origin, ray.direction, out hit, 10000f))
            {
                mousePos = hit.point;
                Debug.DrawRay(ray.origin, ray.direction * (ray.origin-hit.point).magnitude, Color.green, 10f);
            }

            //mousePos.y = context.transform.position.y;

            Debug.Log((mousePos - context.transform.position).magnitude);
            Debug.Log(context.transform.position);
            Debug.Log(mousePos);

            if ((mousePos - context.transform.position).magnitude <= 10f)
            {
                context.SwitchState(context.learning);
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
        Debug.Log("Exiting PlayingState");
        context.Speed = 300f;
    }

    public override void OnTriggerEnter(Collider trigger)
    {

    }

    private void MoveTo(Vector3 destination)
    {
        Vector3 direction = destination - context.creatureRb.position;
        direction.y = 0f;
        LookAt(direction);
        if (direction.magnitude > 0.5f)
        {
            context.creatureRb.velocity = direction.normalized * Time.deltaTime * context.Speed;
        }

    }

    private void LookAt(Vector3 direction)
    {
        Quaternion lookRotation = Quaternion.LookRotation(direction, Vector3.up);
        context.creatureRb.MoveRotation(lookRotation);
    }
}
