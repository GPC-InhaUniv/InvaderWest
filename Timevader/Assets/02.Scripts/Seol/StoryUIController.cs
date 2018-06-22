using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryUIController : MonoBehaviour {

    [SerializeField]
    Button earthButton;
    [SerializeField]
    GameObject confirmPanel;

    [SerializeField]
    GameObject[] storyButtons;

    [SerializeField]
    Button storyButton1;
    [SerializeField]
    Button storyButton4;

    [SerializeField]
    GameObject enemyship;

    [SerializeField]
    Button easyButton;
    [SerializeField]
    Button normalButton;
    [SerializeField]
    Button hardButton;
    
    [SerializeField]
    Text confirmDifficultyText;

    [SerializeField]
    float earthSize = 2.2f;

    int year = 0;
    int pageNum = 0;

    int invaderHeight = 535;

    void FixedUpdate()
    {
        ZoomEarth();
    }

    public void NextPage()
    {
        storyButtons[pageNum].SetActive(false);
        pageNum++;
        storyButtons[pageNum].SetActive(true);
    }

    public void EasyButtonClicked()
    {        
        year = 50000;
        confirmDifficultyText.text = year.ToString() + "를 선택하셨습니다.";
        AccountInfo.ChangeRestTimeData(year);
        AccountInfo.ChangeLevelOfDifficulty(1);
    }

    public void NormalButtonClicked()
    {
        year = 25000;
        confirmDifficultyText.text = year.ToString() + "를 선택하셨습니다.";
        AccountInfo.ChangeRestTimeData(year);
        AccountInfo.ChangeLevelOfDifficulty(2);
    }

    public void HardButtonClicked()
    {
        year = 13000;
        confirmDifficultyText.text = year.ToString() + "를 선택하셨습니다.";
        AccountInfo.ChangeRestTimeData(year);
        AccountInfo.ChangeLevelOfDifficulty(3);
    }

    void SelectOK()
    {
        AccountInfo.ChangeRestTimeData(year);
        SceneManager.LoadScene("Main");
    }

    void SelectCancel()
    {
        confirmPanel.SetActive(false);
    }

    void ZoomEarth()
    {
        earthButton.transform.localScale += Vector3.Lerp(new Vector3(0.02f, 0.02f, 0.02f), new Vector3(0.02f, 0.02f, 0.02f), Time.deltaTime);

        if (earthButton.transform.localScale.x > earthSize)
        {
            earthButton.transform.localScale = new Vector3(earthSize, earthSize, earthSize);
            storyButton1.interactable = true;
            earthButton.interactable = false;
        }
    }   

    public void MoveInvader()
    {
        enemyship.transform.position -= Vector3.Lerp(new Vector3(0, 0.1f, 0), new Vector3(0, 0.1f, 0), Time.deltaTime);

        if (enemyship.transform.localPosition.y < invaderHeight)
        {
            enemyship.transform.localPosition = new Vector3(0, invaderHeight, 0);
            storyButton4.interactable = true;
        }
    }
    
}
