using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragRotation : MonoBehaviour {

    public GameObject Earth, Cloud;
    public Text StageInfo;
    public GameObject[] Invaders;

    public GraphicRaycaster gr;
    PointerEventData ped;
    Vector3 prevPoint;

    public float RotateSpeed = 1f;

    // Update is called once per frame
    void FixedUpdate() {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && hit.collider.tag == "TouchAble")
            {
                Debug.Log(hit.collider.tag);
                // 지구 회전
                Debug.Log("Raycast Hit");
                ClickDrag();
                //TouchSlide();
            }
        }
        if (Input.GetMouseButtonUp(0))
            prevPoint = Input.mousePosition;
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
            Vector3 rotatePower = new Vector3(0, 0, dragValue );

            //Debug.Log("roat" + dragValue);
            Earth.transform.Rotate(rotatePower / 2 * RotateSpeed);
            Cloud.transform.Rotate(rotatePower / 2 * RotateSpeed / 2); // 느리게 회전
            for (int i = 0; i < Invaders.Length; i++)
            {
                Invaders[i].transform.Rotate(-1 * rotatePower / 2 * RotateSpeed); // 반대로 회전
            }
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

            Earth.transform.Rotate(rotatePower / 2 * RotateSpeed * Time.deltaTime);
            for (int i = 0; i < Invaders.Length; i++)
            {
                Invaders[i].transform.Rotate(-1 * rotatePower / 2 * RotateSpeed * Time.deltaTime); // 반대로 회전
            }
            prevPoint = Input.GetTouch(0).position;
        }
    }
}
