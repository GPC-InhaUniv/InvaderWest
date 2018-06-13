using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private Vector2 draggingOffset = new Vector2(0.0f, 40.0f);
    private GameObject draggingObject;
    private RectTransform canvasRectTransform;

    [SerializeField]
    private GameObject enemyship;

    [SerializeField]
    private GameObject playership;

    [SerializeField]
    private Button btnStory;

    [SerializeField]
    private Button btnStory6;

    public void UpdateDraggingObjectPos(PointerEventData eventData)
    {
        if(draggingOffset != null)
        {
            Vector3 screenPos = eventData.position + draggingOffset;

            Camera camera = eventData.pressEventCamera;
            Vector3 newPos;
            if(RectTransformUtility.ScreenPointToWorldPointInRectangle(canvasRectTransform, screenPos, camera, out newPos))
            {
                draggingObject.transform.position = newPos;
                draggingObject.transform.rotation = canvasRectTransform.rotation;
            }
        }
        
    }    

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(draggingObject != null)
        {
            Destroy(draggingObject);
        }
        
        Image sourceImage = GetComponent<Image>();

        draggingObject = new GameObject("Dragging Object");

        draggingObject.transform.SetParent(sourceImage.canvas.transform);
        draggingObject.transform.SetAsLastSibling();
        draggingObject.transform.localScale = Vector3.one;

        CanvasGroup canvasGroup = draggingObject.AddComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = false;

        Image draggingImage = draggingObject.AddComponent<Image>();
        
        draggingImage.sprite = sourceImage.sprite;
        draggingImage.rectTransform.sizeDelta = sourceImage.rectTransform.sizeDelta;
        draggingImage.color = sourceImage.color;
        draggingImage.material = sourceImage.material;
        
        canvasRectTransform = draggingImage.canvas.transform as RectTransform;
        
        UpdateDraggingObjectPos(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        UpdateDraggingObjectPos(eventData);
    }

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
