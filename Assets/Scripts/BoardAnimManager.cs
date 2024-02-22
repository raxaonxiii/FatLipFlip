using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardAnimManager : MonoBehaviour
{
    public List<Card> Cards = new List<Card>();
    public List<ConnectedCard> DeckList = new List<ConnectedCard>();
    public GridLayoutGroup gLayout;
    public Vector3 startPos, startSize, targetSize;
    public GameObject deckOutline;
    public bool dealt;
    public Sprite finishMenu;

    public void EnableCards(bool value)
    {
        for (int i = 0; i < Cards.Count; ++i)
        {
            Cards[i].gameObject.SetActive(value);
        }
    }

    public void EnableConnectedCard(bool value)
    {
        for (int i = 0; i < Cards.Count; ++i)
        {
            DeckList[i].gameObject.SetActive(value);
        }
    }

    public void ResetCards()
    {
        gLayout.enabled = true;
        for (int i = 0; i < Cards.Count; ++i)
        {
            DeckList[i].GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
            DeckList[i].GetComponent<RectTransform>().sizeDelta = new Vector3(350, 350);
        }
    }

    public void FlipCardsDown()
    {
        foreach (ConnectedCard card in DeckList)
            card.ChangeIMGDown();
    }

    public void TurnOnCards()
    {
        EnableCards(true);
    }

    public void SetCards(List<Card> CardList)
    {
        Cards = CardList;
    }

    public void DealAnim(List<Card> shuffledList, Vector3 cardScale)
    {
        StartCoroutine(Deal(shuffledList, cardScale));
    }

    private IEnumerator Deal(List<Card> shuffledList, Vector3 cardScale)
    {
        dealt = false;
        Vector3 newTargetSize = new Vector3(targetSize.x * cardScale.x, targetSize.y * cardScale.y);
        for (int i = 0; i < Cards.Count; ++i)
        {
            Vector3 Gotoposition = new Vector3(shuffledList[i].transform.position.x, shuffledList[i].transform.position.y, shuffledList[i].transform.position.z);
            float elapsedTime = 0;
            float waitTime = .1667f;
            Vector3 currentPos = DeckList[i].transform.position;

            SFXManager.current.PlayDeal();
            while (elapsedTime < waitTime)
            {
                DeckList[i].transform.position = Vector3.Lerp(currentPos, Gotoposition, (elapsedTime / waitTime));
                DeckList[i].GetComponent<RectTransform>().sizeDelta = Vector3.Lerp(startSize, newTargetSize, (elapsedTime / waitTime));
                elapsedTime += Time.deltaTime;

                // Yield here
                yield return null;
            }
            // Make sure we got there
            DeckList[i].transform.position = Gotoposition;
            DeckList[i].GetComponent<RectTransform>().sizeDelta = newTargetSize;
            yield return null;
        }
        dealt = true;
        for (int i = 0; i < Cards.Count; ++i)
        {
            shuffledList[i].gameObject.SetActive(true);
            DeckList[i].gameObject.SetActive(false);
        }
    }

    public void Match(Card card, Vector3 cardScale)
    {
        StartCoroutine(Matched(card, cardScale));
    }

    private IEnumerator Matched(Card card, Vector3 cardScale)
    {
        Vector3 deckSize = new Vector3(350, 350);
        Vector3 Gotoposition = Vector3.zero;
        float elapsedTime = 0;
        float waitTime = .5f;
        Vector3 currentPos = card.connectedCard.GetComponent<RectTransform>().anchoredPosition;

        card.connectedCard.gameObject.SetActive(true);
        card.gameObject.SetActive(false);

        while (elapsedTime < waitTime)
        {
            card.connectedCard.GetComponent<RectTransform>().anchoredPosition = Vector3.Lerp(currentPos, Gotoposition, (elapsedTime / waitTime));
            card.connectedCard.GetComponent<RectTransform>().sizeDelta = Vector3.Lerp(startSize, deckSize, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;

            // Yield here
            yield return null;
        }
        // Make sure we got there
        card.connectedCard.GetComponent<RectTransform>().anchoredPosition = Gotoposition;
        card.connectedCard.GetComponent<RectTransform>().sizeDelta = deckSize;
        yield return null;
    }
}
