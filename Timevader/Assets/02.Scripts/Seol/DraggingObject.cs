using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DraggingObject : MonoBehaviour {

    float middleValue = 100.0f;
    float increaseValue = 1.0f;

    //지구를 돌리는
    [SerializeField]
    GameObject arrow;
    
    Vector3 prevPoint;
    Vector3 currentPoint;

    float ConditionRotation;    

    //[SerializeField]
    //[Range(-0.5f, 5f)]
    //float rotateSpeed;

    [SerializeField]
    Camera MainCamera;
    
    [SerializeField] //로그인 조건 만족시 사라짐
    GameObject[] otherObject;

    void FixedUpdate()
    {
        ActionClickOrDarg();
    }
    private void Start()
    {
        ConditionRotation = 0.90f;
    }

    void ActionClickOrDarg()
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
                ClickOrDrag();
                //TouchSlide();
            }
        }
    }

    void ClickOrDrag()
    {
        /*    PC 조작   */
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("click");
            prevPoint = currentPoint = Input.mousePosition;

        }
        if (Input.GetMouseButton(0))
        {
            Debug.Log("Drag");

            currentPoint = Input.mousePosition;
            Vector3 gap = currentPoint - prevPoint;
            arrow.transform.position += gap;
            prevPoint = currentPoint;


            //float dragValue = (prevPoint.x - Input.mousePosition.x) / 5 - (prevPoint.y - Input.mousePosition.y);
            //float dragValue = prevPoint.y - Input.mousePosition.y;
            //Vector3 rotatePower = new Vector3(0, 0, dragValue);

            //Debug.Log("roat" + dragValue);
            //ship.transform.Rotate(rotatePower / 2 * rotateSpeed);
            //arrow.transform.localPosition = 
            //prevPoint = Input.mousePosition;
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

            //arrow.transform.Rotate(rotatePower / 2 * rotateSpeed * Time.deltaTime);

            prevPoint = Input.GetTouch(0).position;
        }
    }    
    /*
    void HideObject()
    {
        for (int i = 0; i < otherObject.Length - 1; i++)
        {
            otherObject[i].SetActive(false);
        }
        arrow.SetActive(false);
    }    
    */
}
