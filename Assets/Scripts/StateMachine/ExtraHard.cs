﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraHard : State
{
    public ExtraHard(GameManager gameMan) : base(gameMan)
    {
    }

    // Start is called before the first frame update
    public override IEnumerator Start()
    {
        GameMan.pauseButton.interactable = false;
        shuffledList = new List<Card>();
        matchedList = new List<Card>();
        shuffledList.Clear();
        matchedList.Clear();

        cardSize = GameMan.cardSize[3];
        cardsList = GameMan.GetCardsExtraHard();
        BoardAnimMan = GameMan.GetExHardAnimMan();
        BoardAnimMan.EnableCards(true);
        BoardAnimMan.SetCards(cardsList);

        yield return new WaitForEndOfFrame();
        
        SetCardScale();
        SetGridLayoutGroupValues();
        Shuffle();
        matchedCount = 0;
        yield return new WaitUntil(() => shuffled == true);

        yield return new WaitForEndOfFrame();
        foreach (Card card in shuffledList)
            card.gameObject.SetActive(false);

        BoardAnimMan.DealAnim(shuffledList, cardScale);
        yield return new WaitUntil(() => BoardAnimMan.dealt == true);

        BoardAnimMan.gLayout.enabled = false;
        GameMan.SetTimerStarted(true);
        GameMan.pauseButton.interactable = true;
    }

    public override void SetBestTime(float value)
    {
        if (!GameMan.exHardBestSet)
        {
            GameMan.SetExHardBestTime(value);
            GameMan.exHardBestSet = true;
        }
        else if (value < GameMan.exHardBestTime)
        {
            GameMan.SetExHardBestTime(value);
            GameMan.exHardBestSet = true;
        }
    }

    public override Sprite GetFinishMenu()
    {
        return BoardAnimMan.finishMenu;
    }
}
