using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggingObject : MonoBehaviour
{
    // 드래그하는 스크립트  

    Vector3 prevPoint;

    float moveSpeed = 0.1f;

    [SerializeField]
    GameObject arrow;

    [SerializeField]
    Button StoryButton6;

    [SerializeField]
    GameObject enemyShip;
    [SerializeField]
    GameObject playerShip;

    public Canvas ArrowCanvas;
    public Canvas PlayerShipCanvas;

    GraphicRaycaster arrowGraphicRaycaster;
    PointerEventData arrowPointerEventData;

    GraphicRaycaster playershipGraphicRaycaster;
    PointerEventData playershipPointerEventData;

    StoryUIController storyUIController;

    List<RaycastResult> arrowResults;
    List<RaycastResult> playershipResults;

    void Start()
    {
        arrowResults = new List<RaycastResult>();
        playershipResults = new List<RaycastResult>();
        arrowGraphicRaycaster = ArrowCanvas.GetComponent<GraphicRaycaster>();
        playershipGraphicRaycaster = PlayerShipCanvas.GetComponent<GraphicRaycaster>();
        arrowPointerEventData = new PointerEventData(null);
        playershipPointerEventData = new PointerEventData(null);
        storyUIController = GameObject.Find("StorySceneController").GetComponent<StoryUIController>();

        StartCoroutine(HitArrow());
        StartCoroutine(HitPlayership());
    }

    // 화살표와의 충돌 검출
    IEnumerator HitArrow()
    {
        arrowPointerEventData.position = Input.mousePosition;

        arrowGraphicRaycaster.Raycast(arrowPointerEventData, arrowResults);

        if (arrowResults.Count != 0)
        {
            GameObject arrowObj = arrowResults[0].gameObject;

            if (arrowObj.CompareTag("Arrow")) // 히트된 오브젝트의 태그와 맞으면 실행 
            {
                ClickorDrag();
            }

            if (arrow.transform.position.y < -40)
            {
                arrow.SetActive(false);
                enemyShip.SetActive(true);
                storyUIController.MoveInvader();
            }
        }

        yield return null;
        StartCoroutine(HitArrow());
    }

    // 플레이어와의 충돌 검출
    IEnumerator HitPlayership()
    {
        playershipPointerEventData.position = Input.mousePosition;

        playershipGraphicRaycaster.Raycast(playershipPointerEventData, playershipResults);

        if (playershipResults.Count != 0)
        {
            GameObject playershipObj = playershipResults[0].gameObject;

            if (playershipObj.CompareTag("Player"))
            {
                ClickorDrag();
            }

            if (playerShip.transform.position.y > 35)
            {
                StoryButton6.interactable = true;
            }
        }

        yield return null;
        StartCoroutine(HitPlayership());
    }

    // 드래그
    void ClickorDrag()
    {
        /*    PC 조작   */
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("click");
            prevPoint = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            float dragValue = Input.mousePosition.y - prevPoint.y;

            arrow.transform.Translate(new Vector3(0, dragValue * moveSpeed, 0));


            if (playerShip.activeInHierarchy)
            {
                playerShip.transform.Translate(new Vector3(0, dragValue * moveSpeed, 0));
            }

        }
        else
            return;
    }
}
    /*
    void ActionClickOrDarg()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.Log("클릭");

            if (Physics.Raycast(ray, out hit))
            {
                //Vector3 movePower = new Vector3(0, Input.mousePosition.y - prevPoint.y, 0);
                //arrow.transform.Translate(movePower);

                Debug.Log("Raycast Hit");
                // TouchSlide();
            }
        }
    }
    */

    /* 모바일 터치슬라이드
    public void TouchSlide()
    {

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
    */
    

