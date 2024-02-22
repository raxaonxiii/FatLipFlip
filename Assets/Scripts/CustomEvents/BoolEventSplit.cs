using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoolEventSplit : MonoBehaviour
{
	public UnityEvent True, False;
	public bool Event
	{
		set
		{
			UnityEvent target = value ? True : False;
			target.Invoke();
		}
	}
}
