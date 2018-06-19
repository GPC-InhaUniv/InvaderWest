using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoController : MonoBehaviour {
    UIFader uIFader;

    [SerializeField]
    private CanvasGroup[] UIElement;

    private float waitSecond = 2.0f;
    private float fadeSecond = 1.5f;

    [SerializeField]
    private GameObject roginSet;

    float timeStartedLerping;
    float timeSinceStarted;
    float percenttageComplete;


    void Start () {
        uIFader = GetComponent<UIFader>();
        StartCoroutine(Showlogo());

    }

    IEnumerator Showlogo()
    {
        uIFader.CanvasFadeOut(UIElement[0]);
        yield return new WaitForSeconds(fadeSecond);

        uIFader.CanvasFadeIn(UIElement[1]);
        yield return new WaitForSeconds(waitSecond);
        uIFader.CanvasFadeOut(UIElement[1]);
        yield return new WaitForSeconds(waitSecond);
        uIFader.CanvasFadeIn(UIElement[2]);
        yield return new WaitForSeconds(waitSecond);
        uIFader.CanvasFadeOut(UIElement[2]);

        yield return new WaitForSeconds(fadeSecond);
        uIFader.CanvasFadeIn(UIElement[0]);
        yield return new WaitForSeconds(fadeSecond);
        uIFader.CanvasFadeOut(UIElement[0]);

        uIFader.CanvasFadeIn(UIElement[3]);
        roginSet.SetActive(true);
    }

}
