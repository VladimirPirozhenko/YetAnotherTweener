using System;
using UnityEngine;

public interface ITween
{
    public void Stop();

    public void TweenValue(Action<Color> action, float duration, IEasingStrategy easingStrategy, Color from, Color to);
    public void TweenValue(Action<float> action, float duration, IEasingStrategy easingStrategy, float from = 0, float to = 1);
    public void TweenValue(Action<float> action, float duration, eEaseType easing, float from = 0, float to = 1);

    public void TweenValue(Action<float> action, float duration, AnimationCurve curve, float from = 0, float to = 1);

    public void TweenValue(Action<float> action, float duration, float from = 0, float to = 1);
}