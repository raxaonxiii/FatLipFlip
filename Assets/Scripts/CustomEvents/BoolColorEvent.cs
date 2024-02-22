using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoolColorEvent : MonoBehaviour
{
	public Color True = Color.white, False = Color.black;
	public ColorEvent OnNewColor;
	public bool Event
	{
		set
		{
			OnNewColor?.Invoke(value ? True : False);
		}
	}
}
