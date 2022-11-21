using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{

    int foodNumber = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (foodNumber == 0)
        {
            Destroy(gameObject);
        }
    }

    public void ReduceFood()
    {
        foodNumber--;
    }
}
