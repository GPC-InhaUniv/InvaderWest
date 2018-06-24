using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoController : MonoBehaviour {
    UIFader uIFader;

    [SerializeField]
    CanvasGroup[] uIElement;

    [SerializeField]
    float waitSecond = 2.0f;
    [SerializeField]
    float fadeSecond = 1.5f;

    [SerializeField]
    GameObject roginSet;

    float timeStartedLerping;
    float timeSinceStarted;
    float percenttageComplete;


    void Start ()
    {
        uIFader = GetComponent<UIFader>();
        StartCoroutine(Showlogo());
    }

    IEnumerator Showlogo()
    {
        uIFader.CanvasFadeOut(uIElement[0]);
        yield return new WaitForSeconds(fadeSecond);

        uIFader.CanvasFadeIn(uIElement[1]);
        yield return new WaitForSeconds(waitSecond);
        uIFader.CanvasFadeOut(uIElement[1]);
        yield return new WaitForSeconds(waitSecond);

        uIFader.CanvasFadeIn(uIElement[2]);
        yield return new WaitForSeconds(waitSecond);
        uIFader.CanvasFadeOut(uIElement[2]);

        yield return new WaitForSeconds(fadeSecond);
        uIFader.CanvasFadeIn(uIElement[0]);
        yield return new WaitForSeconds(fadeSecond);
        uIFader.CanvasFadeOut(uIElement[0]);

        uIFader.CanvasFadeIn(uIElement[3]);
        roginSet.SetActive(true);
    }

}
