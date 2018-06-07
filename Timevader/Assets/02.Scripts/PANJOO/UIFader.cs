using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFader : MonoBehaviour {

    public CanvasGroup UIElement;
    float currentValue;
    float timeStartedLerping;
    float timeSinceStarted;
    float percentageComplete;

    public void FadeIn(CanvasGroup UIElement)
    {
        StartCoroutine(FadeCanvasGroup(UIElement, UIElement.alpha, 1));
    }

    public void FadeOut(CanvasGroup UIElement)
    {
        StartCoroutine(FadeCanvasGroup(UIElement, UIElement.alpha, 0));
    }

    public IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float StartFade, float EndFade, float lerpTime = 0.5f)
    {
        timeStartedLerping = Time.time;
        timeSinceStarted = Time.time - timeStartedLerping;
        percentageComplete = timeSinceStarted / lerpTime;

        while (true)
        {
           timeSinceStarted = Time.time - timeStartedLerping;
           percentageComplete = timeSinceStarted / lerpTime;

            currentValue = Mathf.Lerp(StartFade, EndFade, percentageComplete);

            canvasGroup.alpha = currentValue;

            if (percentageComplete >= 1) break;

            yield return new WaitForEndOfFrame();
        }

        Debug.Log("done");
    }
}
