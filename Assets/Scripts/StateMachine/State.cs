using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected GameManager GameMan;
    public BoardAnimManager BoardAnimMan;
    public List<Card> cardsList, shuffledList, matchedList;
    public Vector3 cardSize, cardScale;
    public Vector3[] cardPos;
    public float[] XAnchor, YAnchor;
    public bool shuffled;
    public int matchedCount;
    public float bestTime;

    public State(GameManager gameMan)
    {
        GameMan = gameMan;
    }

    public virtual IEnumerator Start()
    {
        yield break;
    }

    public virtual void Update()
    {
        
    }

    public void SetCardScale()
    {
        cardScale = GameMan.canvas.GetComponent<RectTransform>().localScale;
        float xDec, yDec;
        if (cardScale.x > 1)
        {
            xDec = cardScale.x - 1;
            yDec = cardScale.y - 1;

            cardScale = new Vector3(1 - xDec, 1 - yDec, 1);
        }
        float w = Screen.width;
        float h = Screen.height;

        if (w / h < .69f)
            cardScale = Vector3.one;
    }

    public void SetGridLayoutGroupValues()
    {
        float w = Screen.width;
        float h = Screen.height;

        if (GameMan.GetCurrentState().cardsList.Count <= 12)
        {
            if (w / h > .69f)
                BoardAnimMan.gLayout.spacing = new Vector2(25, 25);
            else
                BoardAnimMan.gLayout.spacing = new Vector2(50, 50);
        }
        else
        {
            if (w / h > .69f)
                BoardAnimMan.gLayout.spacing = new Vector2(50, 50);
            else
                BoardAnimMan.gLayout.spacing = new Vector2(25, 25);
        }
    }

    public void Shuffle()
    {
        shuffled = false;
        List<Card> copy = new List<Card>();
        for (int i = 0; i < cardsList.Count; ++i)
        {
            copy.Add(cardsList[i]);
        }

        for (int i = 0; i < cardsList.Count; ++i)
        {
            Card ranCard = copy[Random.Range(0, copy.Count)];
            ranCard.GetComponent<RectTransform>().sizeDelta = cardSize;
            ranCard.GetComponent<RectTransform>().localScale = cardScale;
            copy.Remove(ranCard);
            ranCard.gameObject.transform.SetSiblingIndex(i);
            ranCard.ConnectToDeckList(BoardAnimMan.DeckList[i]);
            shuffledList.Add(ranCard);
        }

        copy.Clear();
        shuffled = true;
    }

    public void MoveMatched(Card first, Card second)
    {
        matchedCount++;
        first.Matched(matchedCount);
        matchedCount++;
        second.Matched(matchedCount);
        matchedList.Add(first);
        matchedList.Add(second);
        shuffledList.Remove(first);
        shuffledList.Remove(second);

        if (matchedList.Count == cardsList.Count)
            GameMan.Finish();
    }

    public virtual void SetBestTime(float value)
    { }

    public virtual Sprite GetFinishMenu()
    {
        return null;
    }
}
