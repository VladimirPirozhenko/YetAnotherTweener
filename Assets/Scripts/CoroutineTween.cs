using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class CoroutineTween : MonoBehaviour,ITween
{
    public IEnumerator TweenCoroutine(Action<float> action, float duration, eEaseType easing, float from = 0, float to = 1)
    {
        float startValue = from;
        float currentValue = startValue;

        float startTime = Time.time;
        float elapsed = 0;
        float currentDelta01 = 0;
        if (duration <= 0)
            currentDelta01 = 1;

        while (elapsed < duration)
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
            currentDelta01 = EaseApplier.Apply(easing, currentDelta01);//Easings.Linear(currentDelta01, from, to);
            action?.Invoke(currentDelta01);
            yield return null;  
        }

        action?.Invoke(currentDelta01);
    }
    public void TweenValue(Action<float> action, float duration, eEaseType easing, float from = 0, float to = 1)
    {
        StartCoroutine(TweenCoroutine(action, duration, easing, from, to));     
    }
}

