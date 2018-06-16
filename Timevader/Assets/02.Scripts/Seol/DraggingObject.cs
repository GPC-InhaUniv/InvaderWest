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
    void Start()
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
