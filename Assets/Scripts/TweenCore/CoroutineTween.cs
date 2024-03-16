using System;
using System.Collections;
using System.Security.Cryptography;
using UnityEngine;

class CoroutineTween : MonoBehaviour, ITween
{
    private Coroutine tweenRoutine;
    private IEasingStrategy easingStrategy = new Easing(eEaseType.Linear);

    private void OnDestroy()
    {
        Debug.Log("Coroutine Destroyed");
    }

    public void Stop()
    {
        if (tweenRoutine != null)
        {
            StopCoroutine(tweenRoutine);
            tweenRoutine = null;
            Debug.Log("Coroutine Stopped");
        }
    }

    private IEnumerator TweenCoroutine(Action<float> action, float duration, float from = 0, float to = 1)
    {
        float startValue = from;
        float currentValue = startValue;

        float startTime = Time.time;
        float elapsed = 0;
        float currentDelta01 = 0;
        if (duration <= 0)
            currentDelta01 = 1;

        while (Application.isPlaying && elapsed < duration)
        {
            if (currentValue == to)
                break;

            elapsed = Time.time - startTime;
            currentDelta01 = elapsed / duration;

            if (currentDelta01 >= 1)
            {
                currentValue = to;
                currentDelta01 = 1;
                break;
            }
            currentDelta01 = easingStrategy.CalculateEasing(currentDelta01);
            action?.Invoke(currentDelta01);
            yield return null;
        }
       // Destroy(gameObject, 0);
        action?.Invoke(currentValue);
    }

    private IEnumerator TweenCoroutine<T>(Action<T> action, float duration, T from, T to) where T : ILerpable<T>
    {
        T startValue = from;
        T currentValue = startValue;

        float startTime = Time.time;
        float elapsed = 0;
        float currentDelta01 = 0;
        if (duration <= 0)
            currentDelta01 = 1;

        while (Application.isPlaying && elapsed < duration)
        {
            if (currentValue.Equals(to))
                break;

            elapsed = Time.time - startTime;
            currentDelta01 = elapsed / duration;

            if (currentDelta01 >= 1)
            {
                currentValue = to;
                currentDelta01 = 1;
                break;
            }
            currentDelta01 = easingStrategy.CalculateEasing(currentDelta01);
            currentValue = currentValue.Lerp(from, to, currentDelta01);
            action?.Invoke(currentValue);
            yield return null;
        }
        // Destroy(gameObject, 0);
        action?.Invoke(currentValue);
    }

    public void TweenValue(Action<float> action, float duration, eEaseType easing, float from = 0, float to = 1)
    {
        if (tweenRoutine != null)
            Stop();
        easingStrategy = new Easing(easing);
        tweenRoutine = StartCoroutine(TweenCoroutine(action, duration, from, to));
    }

    public void TweenValue(Action<float> action, float duration, AnimationCurve curve, float from = 0, float to = 1)
    {
        if (tweenRoutine != null)
            Stop();
        easingStrategy = new AnimationCurveEasing(curve);
        tweenRoutine = StartCoroutine(TweenCoroutine(action, duration, from, to));
        
    }

    public void TweenValue(Action<float> action, float duration, IEasingStrategy easingStrategy, float from = 0, float to = 1)
    {
        if (tweenRoutine != null)
            Stop();
        this.easingStrategy = easingStrategy;
        tweenRoutine = StartCoroutine(TweenCoroutine(action, duration, from, to));
        
    }

    public void TweenValue(Action<float> action, float duration, float from = 0, float to = 1)
    {
        if (tweenRoutine != null)
            Stop();
        tweenRoutine = StartCoroutine(TweenCoroutine(action, duration, from, to));
    }

    public void TweenValue(Action<Color> action, float duration, IEasingStrategy easingStrategy, Color from, Color to)
    {
        if (tweenRoutine != null)
            Stop();
        tweenRoutine = StartCoroutine(TweenCoroutine((c) => { action?.Invoke(c.color); }, duration, new ColorWrapper(from), new ColorWrapper(to)));
    }

    public void TweenValue(Action<Vector2> action, float duration, IEasingStrategy easingStrategy, Vector2 from, Vector2 to)
    {
        if (tweenRoutine != null)
            Stop();
        tweenRoutine = StartCoroutine(TweenCoroutine((v) => { action?.Invoke(v.vec); }, duration, new Vector2Wrapper(from), new Vector2Wrapper(to)));
    }

    public void TweenValue(Action<Vector3> action, float duration, IEasingStrategy easingStrategy, Vector3 from, Vector3 to)
    {
        if (tweenRoutine != null)
            Stop();
        tweenRoutine = StartCoroutine(TweenCoroutine((v) => { action?.Invoke(v.vec); }, duration, new Vector3Wrapper(from), new Vector3Wrapper(to)));
    }

    public void TweenValue(Action<Vector4> action, float duration, IEasingStrategy easingStrategy, Vector4 from, Vector4 to)
    {
        if (tweenRoutine != null)
            Stop();
        tweenRoutine = StartCoroutine(TweenCoroutine((v) => { action?.Invoke(v.vec); }, duration, new Vector4Wrapper(from), new Vector4Wrapper(to)));
    }
}