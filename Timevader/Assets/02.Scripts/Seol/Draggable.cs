using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    Vector2 draggingOffset = new Vector2(0.0f, 40.0f);
    GameObject draggingObject;
    RectTransform canvasRectTransform;

    [SerializeField]
    GameObject enemyship;

    [SerializeField]
    GameObject playership;

    [SerializeField]
    Button btnStory;

    [SerializeField]
    Button btnStory6;

    // 드래그 제어, 드래그 중인 아이콘의 위치 설정
    public void UpdateDraggingObjectPos(PointerEventData eventData)
    {
        if(draggingOffset != null)
        {
            // 드래그 중인 아이콘의 스크린 좌표 계산
            Vector3 screenPos = eventData.position + draggingOffset;

            // 스크린 좌표를 월드 좌표로 변환
            Camera camera = eventData.pressEventCamera;
            Vector3 newPos;
            if(RectTransformUtility.ScreenPointToWorldPointInRectangle(canvasRectTransform, screenPos, camera, out newPos))
            {
                // 드래그 중인 아이콘의 위치를 월드 좌표로 설정
                draggingObject.transform.position = newPos;
                draggingObject.transform.rotation = canvasRectTransform.rotation;
            }
        }
        
    }    

    // 드래그가 시작될 때 호출 - 드래그로 이동시킬 아이콘 생성
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(draggingObject != null)
        {
            Destroy(draggingObject);
        }
        
        // 본래 아이콘의 이미지 컴포넌트를 가져옴
        Image sourceImage = GetComponent<Image>();

        // 드래그 중인 아이콘의 게임 오브젝트 생성
        draggingObject = new GameObject("Dragging Object");

        // 본래 아이콘의 캔버스와 자식 요소로 종속시켜 가장 바깥면에 표시
        draggingObject.transform.SetParent(sourceImage.canvas.transform);
        draggingObject.transform.SetAsLastSibling();
        draggingObject.transform.localScale = Vector3.one;

        // 레이캐스트가 블락되지 않도록 함
        CanvasGroup canvasGroup = draggingObject.AddComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = false;

        // 드래그 중인 아이콘의 게임 오브젝트에 이미지 컴포넌트 추가
        Image draggingImage = draggingObject.AddComponent<Image>();
        
        // 본래 아이콘과 같은 모습이 되도록 설정
        draggingImage.sprite = sourceImage.sprite;
        draggingImage.rectTransform.sizeDelta = sourceImage.rectTransform.sizeDelta;
        draggingImage.color = sourceImage.color;
        draggingImage.material = sourceImage.material;
        
        // 캔버스의 Rect Transform 저장
        canvasRectTransform = draggingImage.canvas.transform as RectTransform;
        
        // 드래그 중인 아이콘의 위치 갱신
        UpdateDraggingObjectPos(eventData);
    }

    // 드래그 도중에 호출 - 드래그 중인 아이콘의 위치 갱신
    public void OnDrag(PointerEventData eventData)
    {
        UpdateDraggingObjectPos(eventData);
    }

    // 드래그가 끝났을 때 호출 - 드래그 중인 아이콘 삭제
    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(draggingObject);


        if(enemyship != null)
            enemyship.SetActive(true);
        btnStory.interactable = true;
        if(btnStory6 != null)
            btnStory6.interactable = true;
    }
}
