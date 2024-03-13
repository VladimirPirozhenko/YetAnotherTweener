using System;

public interface ITween
{
    public void TweenValue(Action<float> action, float duration, eEaseType easing, float from = 0, float to = 1);
}