using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DraggingObject : MonoBehaviour {

    float middleValue = 100.0f;
    float increaseValue = 1.0f;
    
    [SerializeField]
    GameObject arrow;
    
    Vector3 prevPoint;
    Vector3 currentPoint;

    float conditionMoving;

    [SerializeField]
    [Range(-0.5f, 5f)]
    float moveSpeed;
    
    [SerializeField]
    Camera MainCamera;
    
    [SerializeField] //로그인 조건 만족시 사라짐
    GameObject[] otherObject;
    
    void Start()
    {
        //conditionMoving = 0.90f;
    }    

    public void TouchSlide()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("click");
            prevPoint = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            Debug.Log("Drag");
            
            Vector3 movePower = new Vector3(0, Input.mousePosition.y - prevPoint.y, 0);

            //Debug.Log("roat" + dragValue);
            arrow.transform.Translate(movePower);
            
            prevPoint = Input.mousePosition;
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
