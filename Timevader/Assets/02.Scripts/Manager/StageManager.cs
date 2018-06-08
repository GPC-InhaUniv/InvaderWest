using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour {

    public Sprite BoomImg, LockImg;
    public Image[] Imgs; // 각 스테이지의 서브 이미지
    public Text WarRateText;

    const int AllStageCount = 3;
    public int NextStage = 1;

    private void Start()
    {
        StageEffect();
        SetWarRateText();
    }

    void StageEffect()
    {
        for (int i = 0; i < AllStageCount; i++)
        {
            if (i < NextStage - 1)
                Imgs[i].sprite = BoomImg;
            else if (i == NextStage - 1)
                Imgs[NextStage - 1].gameObject.SetActive(false);
            else
                Imgs[i].sprite = LockImg;
        }
    }

    void SetWarRateText()
    {
        int sum = NextStage - 1;
        sum = 100 * sum / AllStageCount;
        WarRateText.text = sum.ToString() + " %";
    }
}
