using System;
using System.Threading.Tasks;
using UnityEngine;

class AsyncTween : ITween
{
    private async Task TweenValueAsync(Action<float> action, float duration, eEaseType easing, float from = 0, float to = 1)
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
            await Task.Yield();
        }

        action?.Invoke(currentDelta01);
    }

    public void TweenValue(Action<float> action, float duration, eEaseType easing, float from, float to)
    {
        var Task = TweenValueAsync(action, duration, easing, from, to);
    }
}