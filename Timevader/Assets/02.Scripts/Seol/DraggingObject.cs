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

    public Canvas TouchCanvas;
    GraphicRaycaster gr;
    PointerEventData ped;

    void Start()
    {
        gr = TouchCanvas.GetComponent<GraphicRaycaster>();
        ped = new PointerEventData(null);
    }

    private void Update()
    {
        ped.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>(); // 여기에 히트 된 개체 저장 
        gr.Raycast(ped, results);
        if (results.Count != 0)
        {
            GameObject obj = results[0].gameObject;
            if (obj.CompareTag("physicalDamage")) // 히트 된 오브젝트의 태그와 맞으면 실행 
            {
                Debug.Log("hit !");
            }
        }
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
