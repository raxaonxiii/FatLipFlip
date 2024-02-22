using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoolSpriteEvent : MonoBehaviour
{
    public Sprite True, False;
    public SpriteEvent OnNewSprite;
    public bool Event
    {
        set
        {
            OnNewSprite?.Invoke(value ? True : False);
        }
    }
}
