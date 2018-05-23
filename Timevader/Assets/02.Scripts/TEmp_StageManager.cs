using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TEmp_StageManager : MonoBehaviour {

    public Sprite BoomImg, LockImg;
    public bool[] stageClearInfos; // 해당 스테이지를 클리어하면 true로 값을 변경
    public Image[] Imgs; // 각 스테이지의 서브 이미지
    public Text WarRateText;

    const int AllStageCount = 3;
    public int NextStage;

    private void Start()
    {
        StageEffect();
        SetWarRateText();
    }

    void StageEffect()
    {
        for (int i = 0; i < stageClearInfos.Length; i++)
        {
            Imgs[NextStage - 1].color = new Color(255, 255, 255, 255); // 투명
            if (stageClearInfos[i])
                Imgs[i].sprite = BoomImg;
            else // stageClearInfos[i] = false
                Imgs[i].sprite = LockImg;
        }
        Imgs[NextStage - 1].color = new Color(0, 0, 0, 0); // 투명
    }

    void SetWarRateText()
    {
        int sum = 0;
        for (int i = 0; i < stageClearInfos.Length; i++)
        {
            if (stageClearInfos[i])
                sum++;
        }
        sum = 100 * sum / AllStageCount;
        WarRateText.text = sum.ToString() + " %";
    }
}
