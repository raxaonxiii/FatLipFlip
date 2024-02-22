using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrayColorEvent : MonoBehaviour
{
    public Color[] colors;
    public ColorEvent OnNewColor;
    public int Event
    {
        set
        {
            OnNewColor?.Invoke(colors[value]);
        }
    }
}
