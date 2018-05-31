using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour {

    //public RectTransform ScrollView;

    //public Image[] item;
    //public RectTransform ScrollCiewCenter;

    ////센터로부터 거리//
    //private float[] itemDistace_Center; 
    //private bool dragging = false;
    //private int itemDistace;
    //private int minImageNum;


    //public Image item1;
    //public Image item2;
    //public Image item3;
    //public Image item4;

    //private void Start()
    //{
    //    int itemLength = item.Length;
    //    itemDistace_Center
    //}



    public ScrollRect ItemScrollRect;

    public float RectHorz;

    public void ChangeValue()
    {
        if (ItemScrollRect.horizontalNormalizedPosition < 0.15f)
        {
            ItemScrollRect.horizontalNormalizedPosition = 0.0f;
        }
        if (ItemScrollRect.horizontalNormalizedPosition >= 0.15f && ItemScrollRect.horizontalNormalizedPosition < 0.5)
        {
            ItemScrollRect.horizontalNormalizedPosition = 0.33f;
        }
        if (ItemScrollRect.horizontalNormalizedPosition >= 0.5f && ItemScrollRect.horizontalNormalizedPosition < 0.85)
        {
            ItemScrollRect.horizontalNormalizedPosition = 0.66f;
        }
        if (ItemScrollRect.horizontalNormalizedPosition >= 0.85)
        {
            ItemScrollRect.horizontalNormalizedPosition = 1.0f;
        }
        //0.15
        //0.5
        //0.85

        Debug.Log(ItemScrollRect.horizontalNormalizedPosition);

    }



    private void Update()
    {
        RectHorz = ItemScrollRect.horizontalNormalizedPosition;

    }


}
