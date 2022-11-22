using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{

    float foodNumber = 100;
    Vector3 startSize;

    // Start is called before the first frame update
    void Awake()
    {
        startSize = transform.localScale;
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
        Debug.Log(foodNumber);
        Debug.Log(startSize);
        Debug.Log((foodNumber / 100));
        transform.localScale = (foodNumber / 100) * startSize;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Insect"))
        {
            Debug.Log("Transport");
            if (collision.gameObject.GetComponent<StateManager>().explorer.TransportFood())
            {
                ReduceFood();
            }
        }
    }
}
