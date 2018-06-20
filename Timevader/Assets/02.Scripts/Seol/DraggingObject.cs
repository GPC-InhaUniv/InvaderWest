using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggingObject : MonoBehaviour {

    float middleValue = 100.0f;
    float increaseValue = 1.0f;
    
    [SerializeField]
    GameObject arrow;
    
    Vector3 prevPoint;

    float conditionMoving;
    
    float moveSpeed = 2;
    
    [SerializeField]
    Camera MainCamera;
    
    [SerializeField] //로그인 조건 만족시 사라짐
    GameObject[] otherObject;

    PointerEventData ped;

    void Start()
    {
        ped = new PointerEventData(null);
    }
    /*
    public void ActionClickOrDarg()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.Log("클릭");

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log("Raycast Hit");
                TouchSlide();
            }
        }
    }
    */
    public void TouchSlide()
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
            Vector3 movePower = new Vector3(0, Input.mousePosition.y - prevPoint.y, 0);

            arrow.transform.Translate(movePower);
            
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
