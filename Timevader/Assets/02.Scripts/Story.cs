using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Story : MonoBehaviour {
    [SerializeField]
    private Text easyText;
    [SerializeField]
    private Button easyBtn;
    [SerializeField]
    private GameObject SelectPanel;
    [SerializeField]
    private GameObject ConfirmPanel;

    [SerializeField]
    private GameObject[] btnStories;

    public void NextPage()
    {
        
    }

    public void SelectEasy()
    {
        easyText.text = "500000";
        easyBtn.interactable=false;
        
    }

    public void SelectNormal()
    {
        
    }

    public void SelectHard()
    {
        
    }

    public void SelectOK()
    {
        //AccountInfo.ChangeRestTimeData(50000);
        //SceneManager.LoadScene("Main");
    }

    public void SelectCancel()
    {
        ConfirmPanel.SetActive(false);
    }
}
