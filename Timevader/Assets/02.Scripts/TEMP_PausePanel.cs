using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMP_PausePanel : MonoBehaviour {
    public GameObject PausePanel;
    public void OpenPausePanel()
    {
        PausePanel.SetActive(true);
    }

    public void ClosePausePanel()
    {
        PausePanel.SetActive(false);
    }
}
