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
    GameObject enemyship;

    [SerializeField]
    Button easyButton;
    [SerializeField]
    Button normalButton;
    [SerializeField]
    Button hardButton;
    
    [SerializeField]
    Text confirmDifficultyText;

    int year = 0;

    int i = 0;

    public void NextPage()
    {
        storyButtons[i].SetActive(false);
        i++;
        storyButtons[i].SetActive(true);
    }

    public void EasyButtonClicked()
    {        
        year = 50000;
        confirmDifficultyText.text = year.ToString() + "를 선택하셨습니다.";
    }

    public void NormalButtonClicked()
    {
        year = 25000;
        confirmDifficultyText.text = year.ToString() + "를 선택하셨습니다.";
    }

    public void HardButtonClicked()
    {
        year = 13000;
        confirmDifficultyText.text = year.ToString() + "를 선택하셨습니다.";
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

        if (earthButton.transform.localScale.x > 1.8)
        {
            earthButton.transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
            storyButton1.interactable = true;
            earthButton.interactable = false;
        }
    }

    void MoveInvader()
    {
        enemyship.transform.position -= Vector3.Lerp(new Vector3(0, 0.1f, 0), new Vector3(0, 0.1f, 0), Time.deltaTime);
    }    

    void FixedUpdate()
    {
        ZoomEarth();        
    }

}
