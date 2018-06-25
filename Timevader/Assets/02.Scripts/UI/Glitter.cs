using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Glitter : MonoBehaviour
{
    [SerializeField]
    Text TipText;
    [SerializeField]
    float waitSecond = 0.3f;
    [SerializeField]
    int CountMaxValue = 15;

    Color textWhiteColor;
    Color textHideColor;
    
    void Start()
    {
        StartCoroutine(ShowReady());
        textWhiteColor = new Color(255, 255, 255, 255);
        textHideColor = new Color(255, 255, 255, 0);
    }

    IEnumerator ShowReady()
    {
        int count = 0;
        while (count < CountMaxValue)
        {
            TipText.color = textWhiteColor;
            yield return new WaitForSeconds(waitSecond);
            TipText.color = textHideColor;
            yield return new WaitForSeconds(waitSecond);
            count++;
        }

        TipText.color = textWhiteColor;
    }
}