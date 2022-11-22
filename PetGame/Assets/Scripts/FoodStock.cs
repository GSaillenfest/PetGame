using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodStock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StackFood(GameObject crumb)
    {
        crumb.gameObject.GetComponent<Collider>().enabled = true;
        crumb.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        crumb.transform.position = transform.position;
        crumb.transform.SetParent(null);
    }
}
