using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageInfoData : MonoBehaviour {
    [SerializeField]
    string stageLv = "1";

    [SerializeField]
    string stageInfo = "Data 파일에서 스테이지 설명 받아올 것";

    public string GetStageLv()
    {
        return stageLv;
    }

    public string GetStageInfo()
    {
        return stageInfo;
    }
}
