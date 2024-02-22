using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Card : MonoBehaviour
{
    public Sprite back, front;
    public Image IMG;
    public Animator _anim;
    public int CardID;
    public Vector3 Pos;
    public ConnectedCard connectedCard;
    public void Init()
    {
        IMG = GetComponent<Image>();
        _anim = GetComponent<Animator>();
        IMG.sprite = back;

        Pos = transform.position;
    }

    private void Update()
    {
        Pos = transform.position;
    }

    public void FlipFaceDown()
    {
        _anim.SetBool("FaceUp", false);
    }

    public void FlipFaceUp()
    {
        _anim.SetBool("FaceUp", true);
    }

    public void FlipSound()
    {
        SFXManager.current.PlayFlip();
    }

    public void ChangeIMGDown()
    {
        IMG.sprite = back;
    }

    public void ChangeIMGUp()
    {
        IMG.sprite = front;
    }

    public int GetCardID()
    {
        return CardID;
    }

    public void Matched(int value)
    {
        connectedCard.IMG.sprite = connectedCard.front;
        connectedCard.gameObject.SetActive(true);
        gameObject.SetActive(false);
        connectedCard.transform.SetSiblingIndex(value);
        GameManager.current.GetCurrentState().BoardAnimMan.Match(this, GameManager.current.GetCurrentState().cardScale);
    }

    public void ConnectToDeckList(ConnectedCard card)
    {
        connectedCard = card;
        connectedCard.front = front;
    }
}
