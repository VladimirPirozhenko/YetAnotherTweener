using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public static class TaskUtil
{
    public static CancellationToken RefreshToken(ref CancellationTokenSource tokenSource)
    {
        tokenSource?.Cancel();
        tokenSource?.Dispose();
        tokenSource = new CancellationTokenSource();
        return tokenSource.Token;
    }
}

class AsyncTween : ITween
{
    private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    private IEasingStrategy easingStrategy = new PredefinedEasing(eEaseType.Linear);

    public void Stop()
    {
        cancellationTokenSource.Cancel();
        Debug.Log("Async will be stopped");
    }

    private async Task TweenValueAsync(Action<float> action, float duration, CancellationToken token, float from = 0, float to = 1)
    {
        float startValue = from;
        float currentValue = startValue;

        float startTime = Time.time;
        float elapsed = 0;
        float currentDelta01 = 0;
        if (duration <= 0)
            currentDelta01 = 1;
        try
        {
            while (elapsed < duration)
            {
                if (!Application.isPlaying)
                    Stop();
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

                token.ThrowIfCancellationRequested();
                await Task.Yield();
            }
        }
        catch (OperationCanceledException e)
        {
            Debug.Log($"[TweenValueAsync cancelled]: " + e);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
        finally
        {
            action?.Invoke(currentDelta01);
        }

        action?.Invoke(currentDelta01);
    }

    public void TweenValue(Action<float> action, float duration, eEaseType easing, float from, float to)
    {
        easingStrategy = new PredefinedEasing(easing);
        var task = TweenValueAsync(action, duration, TaskUtil.RefreshToken(ref cancellationTokenSource), from, to);
        task.ConfigureAwait(false);
    }

    public void TweenValue(Action<float> action, float duration, AnimationCurve curve, float from, float to)
    {
        easingStrategy = new AnimationCurveEasing(curve);
        var task = TweenValueAsync(action, duration, TaskUtil.RefreshToken(ref cancellationTokenSource), from, to);
        task.ConfigureAwait(false);
    }
}