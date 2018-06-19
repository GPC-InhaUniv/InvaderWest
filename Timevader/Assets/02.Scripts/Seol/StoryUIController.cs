using System.Collections;
using UnityEngine;
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

    int i = 0;

    public void NextPage()
    {
        storyButtons[i].SetActive(false);
        i++;
        storyButtons[i].SetActive(true);
    }

    void SelectOK()
    {
        //AccountInfo.ChangeRestTimeData(50000);
        //SceneManager.LoadScene("Main");
    }

    void SelectCancel()
    {
        confirmPanel.SetActive(false);
    }

    bool isZoomed()
    {
        if (earthButton.transform.localScale.x > 2.7)
        {
            storyButton1.interactable = true;
            earthButton.interactable = false;
            return false;
        }

        return true;
    }

    /*
    void SetScale(Button earth, Vector3 scale)
    {
        earth.transform.localScale = scale;
    }

    void SetPosition(GameObject invader, Vector3 position)
    {
        invader.transform.localPosition = position;
    }
    */
    /*
    public static void AddEventTriggerListener(EventTrigger trigger,
                                            EventTriggerType eventType,
                                            System.Action<BaseEventData> callback)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = eventType;
        entry.callback = new EventTrigger.TriggerEvent();
        entry.callback.AddListener(new UnityEngine.Events.UnityAction<BaseEventData>(callback));
        trigger.triggers.Add(entry);
    }
    */

    void Start()
    {
        earthButton.onClick.AddListener(OnClickEarthButton);

        //EventTrigger trigger = GetComponentInParent<EventTrigger>();
        //trigger.OnMove(EventTriggerType.Move, AxisEventData eventData); 
    }
    
    void FixedUpdate()
    {       
        if (enemyship != null)
        {
            if (enemyship.activeSelf == true)
            {
                StartCoroutine(MoveInvader());
                Debug.Log("적");
            }
        }
        else
            return;
    }

    IEnumerator ZoomEarth()
    {
        WaitForSeconds waitsec = new WaitForSeconds(0.04f);

        while (isZoomed())
        {
            earthButton.transform.localScale += Vector3.Lerp(transform.localScale, transform.localScale * 0.01f, Time.deltaTime / 20);

            yield return waitsec;
            Debug.Log("지구 커져라");
        }

        /*
        float j = 1;

        while (j <= 3.0f)
        {
            earthButton.transform.localScale = new Vector3(j, j, j);
            j += 0.1f;

            yield return waitsec;
            Debug.Log("지구 커져라");
        }
        */
    }

    IEnumerator MoveInvader()
    {
        WaitForSeconds waitsec = new WaitForSeconds(0.1f);

        int height = 879;

        while (height > 527)
        {
            enemyship.transform.localPosition = new Vector3(0, height, 0);
            //SetPosition(enemyship, new Vector3(0, k, 0));
            height -= 1;

            yield return waitsec;
            Debug.Log("침략군 강림");

        }

    }
    
    void OnClickEarthButton()
    {
        StartCoroutine(ZoomEarth());
    }
    
}
