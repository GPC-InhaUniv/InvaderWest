using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DrageEarthRotation : MonoBehaviour {

    //지구를 돌리는
    public GameObject Earth;

    Vector3 prevPoint;


    private float ConditionRotation;
    
    [SerializeField]
    [Range(-0.5f,5f)]
    private float rotateSpeed;


    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.Log("클릭");
            
            if (Physics.Raycast(ray, out hit))
            {
                // 지구 회전
                Debug.Log("Raycast Hit");
                ClickDrag();
                //TouchSlide();
            }
        }
    }
    private void Start()
    {
        ConditionRotation = 0.90f;
    }
    void Update()
    {
        NextScene();
    }


    void ClickDrag()
    {
        /*    PC 조작   */
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("click");
            prevPoint = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            Debug.Log("Drag");
            float dragValue = (prevPoint.x - Input.mousePosition.x) / 5 - (prevPoint.y - Input.mousePosition.y);
            // r^2 = x^2 + y^2
            Vector3 rotatePower = new Vector3(0, 0, dragValue);

            //Debug.Log("roat" + dragValue);
            Earth.transform.Rotate(rotatePower / 2 * rotateSpeed);
            prevPoint = Input.mousePosition;
        }
    }

    void TouchSlide()
    {
        /*    폰 조작   */
        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Debug.Log("Touch");
            prevPoint = Input.mousePosition;
        }
        if (Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Debug.Log("Slide");
            float dragValue = (prevPoint.x - Input.GetTouch(0).position.x) / 5 - (prevPoint.y - Input.GetTouch(0).position.y);
            Vector3 rotatePower = new Vector3(0, 0, dragValue);

            Earth.transform.Rotate(rotatePower / 2 * rotateSpeed * Time.deltaTime);
            prevPoint = Input.GetTouch(0).position;
        }
    }

    void NextScene()
    {
        Debug.Log(Earth.transform.rotation.z);
        if(Earth.transform.rotation.z < -ConditionRotation || Earth.transform.rotation.z > ConditionRotation)
        {
            Debug.Log("다음 씬으로");
            SceneManager.LoadScene("Main");
        }
    }
}
