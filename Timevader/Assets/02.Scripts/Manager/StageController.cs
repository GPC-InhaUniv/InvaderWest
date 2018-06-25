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
    int NextStage;

    int AllStageCount = 3;

    private void Start()
    {
        NextStage = GetNextStageInfo();
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
        //다음스테이지 데이터에서 불러오기//
        NextStage = int.Parse(AccountInfo.Instance.StageData);
        Debug.Log(NextStage);
        return NextStage;
    }
    
}
