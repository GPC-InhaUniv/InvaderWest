using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragTest : MonoBehaviour {

    //public Canvas canvas;

    public GraphicRaycaster gr;
    PointerEventData ped;

    Vector3 prevPoint;
    public Image[] StageIcon;

    [Range(-1,1)]
    public int dirValue;

    private void Start()
    {
        //gr = canvas.GetComponent<GraphicRaycaster>();
        ped = new PointerEventData(null);
    }

    // Update is called once per frame
    void Update () {
        ped.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        gr.Raycast(ped, results);
        if (results.Count != 0 && results[0].gameObject.tag == "TouchAble")
        {
            Debug.Log(Input.mousePosition);
            Debug.Log("Raycast Hit");
            ClickDrag();
            //TouchSlide();
            ChangeStageInfo();
        }
    }

    void ChangeStageInfo()
    {
        float rotateValue = transform.rotation.z;
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

            // float dragValue = (prevPoint.x - Input.mousePosition.x) / 5 - (prevPoint.y - Input.mousePosition.y);
            float dragValue = (transform.localScale.x);
            // r^2 = x^2 + y^2
            Vector3 rotatePower = new Vector3(0, 0, dirValue * dragValue * 2 / 3);

            //Debug.Log(dragValue);
            transform.Rotate(rotatePower);
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
            float dragValue = (prevPoint.x - Input.GetTouch(0).position.x) - (prevPoint.y - Input.GetTouch(0).position.y);
            Vector3 rotatePower = new Vector3(0, 0, dragValue / 2);

            transform.Rotate(rotatePower * Time.deltaTime);
            prevPoint = Input.GetTouch(0).position;
        }
    }
}
