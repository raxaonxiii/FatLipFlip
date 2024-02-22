using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public float bestEasyTime;
    public float bestMedTime;
    public float bestHardTime;
    public float bestExHardTime;
    public bool easyBestSet;
    public bool medBestSet;
    public bool hardBestSet;
    public bool exHardBestSet;

    public SaveData(GameManager gameMan)
    {
        bestEasyTime = gameMan.easyBestTime;
        bestMedTime = gameMan.medBestTime;
        bestHardTime = gameMan.hardBestTime;
        bestExHardTime = gameMan.exHardBestTime;
        easyBestSet = gameMan.easyBestSet;
        medBestSet = gameMan.medBestSet;
        hardBestSet = gameMan.hardBestSet;
        exHardBestSet = gameMan.exHardBestSet;
    }

    public SaveData()
    {
        bestEasyTime = 0;
        bestMedTime = 0;
        bestHardTime = 0;
        bestExHardTime = 0;
    }
}
