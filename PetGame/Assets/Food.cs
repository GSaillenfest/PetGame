using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{

    float foodNumber = 100;
    Vector3 startSize;

    // Start is called before the first frame update
    void Start()
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
        foodNumber -= Time.deltaTime;
        transform.localScale = (foodNumber / 100) * startSize;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<StateManager>(out StateManager state))
        {
            ReduceFood();
        }
    }
}
