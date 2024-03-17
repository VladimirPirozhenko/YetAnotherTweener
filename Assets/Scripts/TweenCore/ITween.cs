using System;
using UnityEngine;

public interface ITween
{
    public void Stop();
    //public void Pause();
    //public void Loop();
    //public void PingPong(); // add to class?
    public void TweenValue(Action<Color> action, float duration, IEasingStrategy easingStrategy, Color from, Color to);
    public void TweenValue(Action<Vector2> action, float duration, IEasingStrategy easingStrategy, Vector2 from, Vector2 to);
    public void TweenValue(Action<Vector3> action, float duration, IEasingStrategy easingStrategy, Vector3 from, Vector3 to);
    public void TweenValue(Action<Vector4> action, float duration, IEasingStrategy easingStrategy, Vector4 from, Vector4 to);
    public void TweenValue(Action<float> action, float duration, IEasingStrategy easingStrategy, float from = 0, float to = 1);
    public void TweenValue(Action<float> action, float duration, eEaseType easing, float from = 0, float to = 1);

    public void TweenValue(Action<float> action, float duration, AnimationCurve curve, float from = 0, float to = 1);

    public void TweenValue(Action<float> action, float duration, float from = 0, float to = 1);
}