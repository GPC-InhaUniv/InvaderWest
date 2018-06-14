using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Story : MonoBehaviour {
        
    [SerializeField]
    private Button btnEarth;
    [SerializeField]
    private GameObject SelectPanel;
    [SerializeField]
    private GameObject ConfirmPanel;

    [SerializeField]
    private GameObject[] btnStories;

    [SerializeField]
    private Button btnStory1;

    [SerializeField]
    private GameObject enemyship;

    [SerializeField]
    private Button easyBtn;
    [SerializeField]
    private Button normalBtn;
    [SerializeField]
    private Button hardBtn;

    int i = 0;
    
    public void NextPage()
    {
        btnStories[i].SetActive(false);
        i++;
        btnStories[i].SetActive(true);
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

    void SetScale(Button earth, Vector3 scale)
    {
        earth.transform.localScale = scale;
    }

    void SetPosition(GameObject invader, Vector3 position)
    {
        invader.transform.localPosition = position;
    }

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
        btnEarth.onClick.AddListener(OnClickEarthButton);

        //EventTrigger trigger = GetComponentInParent<EventTrigger>();
        //trigger.OnMove(EventTriggerType.Move, AxisEventData eventData); 
    }
    
    void Update()
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
    
    private void FixedUpdate()
    {
        if (btnEarth.transform.localScale.x > 2.8)
        {
            btnStory1.interactable = true;            
        }
    }

    private IEnumerator ZoomEarth()
    {
        WaitForSeconds waitsec = new WaitForSeconds(0.1f);
        btnEarth.interactable = false;
        float j = 1;

        while (j <= 3.0f)
        {
            SetScale(btnEarth, new Vector3(j, j, j));
            j += 0.1f;

            yield return waitsec;                    
            Debug.Log("지구 커져라");           
            
        }        

    }

    private IEnumerator MoveInvader()
    {
        WaitForSeconds waitsec = new WaitForSeconds(0.1f);

        int k = 879;

        while (k > 527)
        {
            SetPosition(enemyship, new Vector3(0, k, 0));
            k -= 30;

            yield return waitsec;
            Debug.Log("침략군 강림");
        }
    }

    public void OnClickEarthButton()
    {
        StartCoroutine(ZoomEarth());        
    }
 
    
}
