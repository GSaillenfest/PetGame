using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float speed = 20f;
    [SerializeField] float rotSpeed = 2f;
    [SerializeField] GameObject pivotPointGameObject;
    [SerializeField] ParticleSystem ringOnClick;
    Camera cam;
    Vector3 pivotPoint;
    bool pivotPointSet;
    Vector2 initMousePos;
    Vector2 mousePos;
    float horizontalInput;
    float verticalInput;
    float rotateInput;
    float zoomInput;
    Vector3 clickPos;
    Color feedbackColor;
    bool doFeedback;


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        rotateInput = Input.GetAxisRaw("Rotate");
        zoomInput = Input.GetAxisRaw("Zoom");
        TranslateCamera();
        RotateCamera();
        Zoom();

        if (doFeedback) CommandClickFeedback();
    }

    private void TranslateCamera()
    {
        Vector3 mousePosOnScreen = cam.ScreenToViewportPoint(Input.mousePosition);

        if (mousePosOnScreen.x > 0.95f && mousePosOnScreen.x <= 1f)
        {
            Vector3 translateVector = transform.right;
            translateVector.y = 0;
            translateVector = (1 - Mathf.Clamp((1 - mousePosOnScreen.x), 0f, 0.05f) / 0.05f) * speed * Time.deltaTime * translateVector.normalized;
            cam.transform.Translate(translateVector, Space.World);
        }
        else if (mousePosOnScreen.x < 0.05f && mousePosOnScreen.x >= 0f)
        {
            Vector3 translateVector = transform.right;
            translateVector.y = 0;
            translateVector = (1 - Mathf.Clamp((0 - mousePosOnScreen.x), 0f, 0.05f) / 0.05f) * speed * Time.deltaTime * -translateVector.normalized;
            cam.transform.Translate(translateVector, Space.World);
        }
        if (mousePosOnScreen.y > 0.95f && mousePosOnScreen.y <= 1f)
        {
            Vector3 translateVector = transform.forward;
            translateVector.y = 0;
            translateVector = (1 - Mathf.Clamp((1 - mousePosOnScreen.y), 0f, 0.05f) / 0.05f) * speed * Time.deltaTime * translateVector.normalized;
            cam.transform.Translate(translateVector, Space.World);
        }
        else if (mousePosOnScreen.y < 0.05f && mousePosOnScreen.y >= 0f)
        {
            Vector3 translateVector = transform.forward;
            translateVector.y = 0;
            translateVector = (1 - Mathf.Clamp((0 - mousePosOnScreen.y), 0f, 0.05f) / 0.05f) * speed * Time.deltaTime * -translateVector.normalized;
            cam.transform.Translate(translateVector, Space.World);
        }

        if (horizontalInput != 0)
        {
            Vector3 translateVector = transform.right * horizontalInput;
            translateVector.y = 0;
            translateVector = speed * Time.deltaTime * translateVector.normalized;
            cam.transform.Translate(translateVector, Space.World);
        }
        if (verticalInput != 0)
        {
            Vector3 translateVector = transform.forward * verticalInput;
            translateVector.y = 0;
            translateVector = speed * Time.deltaTime * translateVector.normalized;
            cam.transform.Translate(translateVector, Space.World);
        }
    }

    private void RotateCamera()
    {
        mousePos = Input.mousePosition;
        if (Input.GetMouseButton(1))
        {
            if (pivotPointSet == false)
            {
                Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
                RaycastHit hit;

                if (Physics.Raycast(ray.origin, ray.direction, out hit, 10000f))
                {
                    pivotPoint = hit.point;
                    pivotPointGameObject.transform.position = pivotPoint;
                    initMousePos = Input.mousePosition;
                    pivotPointGameObject.transform.right = transform.position - pivotPointGameObject.transform.position;
                    pivotPointGameObject.transform.up = Vector3.up;
                    transform.SetParent(pivotPointGameObject.transform);
                    pivotPointSet = true;
                }
            }
            Debug.Log((mousePos - initMousePos).magnitude);
            pivotPointGameObject.transform.rotation = Quaternion.Euler(Vector3.up * (mousePos.x - initMousePos.x) * rotSpeed);
        }
        if (Input.GetMouseButtonUp(1))
        {
            pivotPointSet = false;
            transform.SetParent(null);
        }

        if (rotateInput != 0)
        {
            if (pivotPointSet == false)
            {
                Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
                RaycastHit hit;

                if (Physics.Raycast(ray.origin, ray.direction, out hit, 10000f))
                {
                    pivotPoint = hit.point;
                    pivotPointSet = true;

                }
            }
            transform.RotateAround(pivotPoint, Vector3.up, rotateInput * rotSpeed * 20f * Time.deltaTime);
        }
    }

    void Zoom()
    {
        if (zoomInput != 0)
        {
            Debug.Log("zooming");
            Vector3 translateVector = transform.forward;
            translateVector = zoomInput * speed * Time.deltaTime * translateVector.normalized;
            cam.transform.Translate(translateVector, Space.World);
        }
    }

    public void SetCommandClickFeedback(Color color, Vector3 pos)
    {
        if (!doFeedback)
        {
            feedbackColor = color;
            clickPos = pos;
            doFeedback = true;
        }
    }

    public void CommandClickFeedback()
    {
        ParticleSystem particle = GameObject.Instantiate(ringOnClick, clickPos + (Vector3.up * 0.1f), transform.rotation);
        var main = particle.main;
        main.startColor = feedbackColor;
        doFeedback = false;
    }
}
