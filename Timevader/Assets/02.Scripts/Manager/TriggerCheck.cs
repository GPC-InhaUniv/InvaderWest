using UnityEngine;
using UnityEngine.UI;

public class TriggerCheck : MonoBehaviour {
    public Text StageLv, StageInfo;
    public StageLoad stageLoad;
    StageInfoData info;

    void Start()
    {
        StageLv.text = string.Empty;
        StageInfo.text = string.Empty;
        stageLoad = GameObject.FindGameObjectWithTag("GameController").GetComponent<StageLoad>();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("enter");
        if (other.tag == "StageSelect")
        {
            info = other.gameObject.GetComponent<StageInfoData>();
            other.gameObject.GetComponent<Animator>().enabled = true;
            other.gameObject.GetComponent<Animator>().SetTrigger("Checked");

            int StageNumber;
            int.TryParse(info.GetStageLv(), out StageNumber);

            stageLoad.StageNumber = StageNumber;
            StageLv.text = "Stage" + info.GetStageLv();
            StageInfo.text = info.GetStageInfo();
        }

        int stageNumber;
        if (!int.TryParse(info.GetStageLv(), out stageNumber))
            Debug.Log("Stage Lv을 받아오는데 실패");
        stageLoad.StageNumber = stageNumber;
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("exit");
        other.gameObject.GetComponent<Animator>().enabled = false;
        StageLv.text = string.Empty;
        StageInfo.text = string.Empty;
        stageLoad.StageNumber = null;
    }
}
