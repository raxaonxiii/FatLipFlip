using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenu : MonoBehaviour
{
    private float easyBestTime, medBestTime, hardBestTime, exHardBestTime;
    public TextMeshProUGUI easyDisplay, medDisplay, hardDisplay, exHardDisplay;
    public GameObject settingsMenu, clearSaveMenu;

    // Start is called before the first frame update
    public void SetBest(float easyValue, float medValue,float hardValue, float exHardValue)
    {
        easyBestTime = easyValue;
        medBestTime = medValue;
        hardBestTime = hardValue;
        exHardBestTime = exHardValue;

        easyDisplay.text = ConvertTime(easyBestTime);
        medDisplay.text = ConvertTime(medBestTime);
        hardDisplay.text = ConvertTime(hardBestTime);
        exHardDisplay.text = ConvertTime(exHardBestTime);
    }

    public string ConvertTime(float seconds)
    {
        int minutes = Mathf.FloorToInt((seconds / 60) % 60);
        int _seconds = Mathf.FloorToInt(seconds % 60);

        if (seconds == 0)
            return "";
        else
            return "BEST TIME     " + string.Format("{0:00}:{1:00}", minutes, _seconds);
    }

    public void ClearSave()
    {
        easyBestTime = medBestTime = hardBestTime = exHardBestTime = 0;
        easyDisplay.text = medDisplay.text = hardDisplay.text = exHardDisplay.text = "";
        GameManager.current.ClearTimes();
        SaveSystem.SaveTime(GameManager.current);
    }

    public void CloseSettings()
    {
        if (clearSaveMenu.activeSelf)
            clearSaveMenu.SetActive(false);
        else
            settingsMenu.SetActive(false);
    }
}
