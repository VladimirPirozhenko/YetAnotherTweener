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
        action?.Invoke(currentDelta01);
    }

    public void TweenValue(Action<float> action, float duration, eEaseType easing, float from = 0, float to = 1)
    {
        if (tweenRoutine == null)
        {
            easingStrategy = new Easing(easing);
            tweenRoutine = StartCoroutine(TweenCoroutine(action, duration, from, to));
        }
    }

    public void TweenValue(Action<float> action, float duration, AnimationCurve curve, float from = 0, float to = 1)
    {
        if (tweenRoutine == null)
        {
            easingStrategy = new AnimationCurveEasing(curve);
            tweenRoutine = StartCoroutine(TweenCoroutine(action, duration, from, to));
        }
    }

    public void TweenValue(Action<float> action, float duration, IEasingStrategy easingStrategy, float from = 0, float to = 1)
    {
        if (tweenRoutine == null)
        {
            this.easingStrategy = easingStrategy;
            tweenRoutine = StartCoroutine(TweenCoroutine(action, duration, from, to));
        }
    }

    public void TweenValue(Action<float> action, float duration, float from = 0, float to = 1)
    {
        if (tweenRoutine == null)
        {
            tweenRoutine = StartCoroutine(TweenCoroutine(action, duration, from, to));
        }
    }
}