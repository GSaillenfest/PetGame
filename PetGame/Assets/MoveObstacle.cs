using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacle : MonoBehaviour
{

    Vector3 initPos;
    float t;
    int reverse = 1;
    float duration;

    // Start is called before the first frame update
    void Start()
    {
        initPos = transform.position;
        duration = Random.Range(2f, 4f);
    }

    // Update is called once per frame
    void Update()
    {
        if (t > 1)
        {
            t = 1;
            reverse = -1;
        }
        else if (t < 0)
        {
            t = 0;
            reverse = 1;
        }
        t += reverse * Time.deltaTime / duration;
        transform.transform.position = Vector3.Lerp(initPos, initPos + 10 * Vector3.right, t);
    }
}
