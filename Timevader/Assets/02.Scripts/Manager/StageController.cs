using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageController : MonoBehaviour {

    [SerializeField]
    Sprite BoomImg = null, LockImg = null;

    [SerializeField]
    GameObject[] Imgs; // 각 스테이지의 서브 이미지

    [SerializeField]
    Text WarRateText = null;

    [SerializeField]
    int NextStage = 1;

    int AllStageCount = 3;

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
                Imgs[i].GetComponent<SpriteRenderer>().sprite = BoomImg;
            else if (i == NextStage - 1)
                Imgs[NextStage - 1].SetActive(false);
            else
                Imgs[i].GetComponent<SpriteRenderer>().sprite = LockImg;
        }
    }

    void SetWarRateText()
    {
        int sum = NextStage - 1;
        sum = 100 * sum / AllStageCount;
        WarRateText.text = sum.ToString() + " %";
    }

    public int GetNextStageInfo()
    {
        return NextStage;
    }
}
