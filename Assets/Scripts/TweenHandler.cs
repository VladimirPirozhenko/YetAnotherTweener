using System.Collections.Generic;
using UnityEngine;

internal class TweenHandler
{
    public static Dictionary<Component, ITween> ComponentToTween = new Dictionary<Component, ITween>();
}