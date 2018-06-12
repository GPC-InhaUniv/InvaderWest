using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextEffect : MonoBehaviour {
    [SerializeField]
    private GameObject TipText;
    [SerializeField]
    private float waitSecond = 0.4f;
    [SerializeField]
    private int CountMaxValue = 15;


    void Start () {
        TipText.SetActive(false);
        StartCoroutine(ShowReady());
	}

    IEnumerator ShowReady()
    {
        int count = 0;
        while (count < CountMaxValue)
        {
            TipText.SetActive(true);
            yield return new WaitForSeconds(waitSecond);
            TipText.SetActive(false);
            yield return new WaitForSeconds(waitSecond);
            count++;
        }
        TipText.SetActive(true);
    }
}
