using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Glitter : MonoBehaviour
{
    [SerializeField]
    private Text TipText;
    [SerializeField]
    private float waitSecond = 0.3f;
    [SerializeField]
    private int CountMaxValue = 15;

    private Color textWhiteColor;
    private Color textHideColor;
    
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