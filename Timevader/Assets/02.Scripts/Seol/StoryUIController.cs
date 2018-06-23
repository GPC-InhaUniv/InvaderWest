using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryUIController : MonoBehaviour {

    [SerializeField]
    GameObject earthObj;
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
    float earthSize = 2.5f;

    int year = 0;
    int pageNum = 0;

    [SerializeField]
    int invaderHeight = 535;

    [SerializeField]
    float enemySpeed = 0.15f;

    int checkYear;
    int restTime;

    void Start()
    {
        restTime = int.Parse(AccountInfo.Instance.RestTime);
        SkipStory();
    }

    void FixedUpdate()
    {
        if(earthObj.activeInHierarchy == true)
        {
            ZoomEarth();
        }    
    }

    void SkipStory()
    {
        if (restTime == 0)
            return;

        if(restTime > 0)
        {            
            SceneManager.LoadScene("Main");
        }   
    }

    public void NextPage()
    {
        storyButtons[pageNum].SetActive(false);
        pageNum++;
        storyButtons[pageNum].SetActive(true);
    }

    public void OnDifficultyButtonClicked(int year)
    {        
        confirmDifficultyText.text = year.ToString() + "를 선택하셨습니다.";
        checkYear = year;
    }

    public void OnSelectOK()
    {        
        Debug.Log(year + "타임 저장값입니다.");
        AccountInfo.ChangeRestTimeData(checkYear);
        switch (checkYear)
        {
            case 5000:
                AccountInfo.ChangeLevelOfDifficulty(1);
                break;
            case 2500:
                AccountInfo.ChangeLevelOfDifficulty(2);
                break;
            case 1000:
                AccountInfo.ChangeLevelOfDifficulty(3);
                break;
        }
        StartCoroutine(WaitNextScene());

    }

    IEnumerator WaitNextScene()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("Intro");
    }

    void SelectCancel()
    {
        confirmPanel.SetActive(false);
    }

    void ZoomEarth()
    {
        if (earthObj.transform.localScale.x < earthSize)
        {
            earthObj.transform.localScale += Vector3.Lerp(new Vector3(0.02f, 0.02f, 0.02f), new Vector3(0.02f, 0.02f, 0.02f), Time.deltaTime);
            storyButton1.interactable = true;
        }
    }   

    public void MoveInvader()
    {
        if (enemyship.transform.localPosition.y > invaderHeight)
        {
            enemyship.transform.position -= Vector3.Lerp(new Vector3(0, enemySpeed, 0), new Vector3(0, enemySpeed, 0), Time.deltaTime);
            storyButton4.interactable = true;
        }
    }
    
    
}
