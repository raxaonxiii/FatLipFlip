using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class BoolEvent : UnityEvent<bool> { }
//-----------------------------//
[Serializable]
public class FloatEvent : UnityEvent<float> { }
//-----------------------------//
[Serializable]
public class StringEvent : UnityEvent<string> { }
//-----------------------------//
[Serializable]
public class SpriteEvent : UnityEvent<Sprite> { }
//-----------------------------//
[Serializable]
public class ColorEvent : UnityEvent<Color> { }
