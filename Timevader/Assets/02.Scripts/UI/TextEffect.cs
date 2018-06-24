using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextEffect : MonoBehaviour {
    [SerializeField]
    GameObject TipText;
    [SerializeField]
    float waitSecond = 0.5f;
    [SerializeField]
    int countMaxValue = 5;


    void Start () {
        TipText.SetActive(false);
        StartCoroutine(ShowReady());
	}

    IEnumerator ShowReady()
    {
        int count = 0;
        while (count < countMaxValue)
        {
            TipText.SetActive(true);
            yield return new WaitForSeconds(waitSecond);
            TipText.SetActive(false);
            yield return new WaitForSeconds(waitSecond);
            count++;
        }
        TipText.SetActive(false);
    }
}
