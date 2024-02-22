using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BoolEventAnd : MonoBehaviour
{
	public BoolEvent OnBoolChanged;
	[SerializeField]
	private bool _left, _right;
	private bool _current;
	public bool Left
	{
		set
		{
			_left = value;
			Current = _left && _right;
		}
	}
	public bool Right
	{
		set
		{
			_right = value;
			Current = _left && _right;
		}
	}
	private bool Current
	{
		set
		{
			if (value != _current)
			{
				_current = value;
				OnBoolChanged?.Invoke(value);
			}
		}
	}
}
