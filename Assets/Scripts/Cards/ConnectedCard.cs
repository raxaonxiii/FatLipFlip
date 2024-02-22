using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConnectedCard : Card
{
    void Start()
    {
        IMG = GetComponent<Image>();
        IMG.sprite = back;
    }
}
