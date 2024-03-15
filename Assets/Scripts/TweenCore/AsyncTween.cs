﻿using System;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting.Antlr3.Runtime;
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
    private IEasingStrategy easingStrategy = new Easing(eEaseType.Linear);


   ~AsyncTween() 
    {
        Debug.Log("Async tween deleted");
    }    

public void Stop()
    {
        if (cancellationTokenSource.IsCancellationRequested)
        {
            Debug.Log("Async stop already requested");
            return;
        }

        cancellationTokenSource?.Cancel();
        cancellationTokenSource?.Dispose();
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
    private async Task TweenValueAsync<ColorWrapper>(Action<ColorWrapper> action, float duration, CancellationToken token, ColorWrapper from, ColorWrapper to) where T :  ILerpable<T>
    {
        ColorWrapper startValue = from;
        ColorWrapper currentValue = startValue;

        float startTime = Time.time;
        float elapsed = 0;
        float currentDelta01 = 0;
        if (duration <= 0)
            currentDelta01 = 1;
        try
        {
            while (Application.isPlaying && elapsed < duration)
            {
                //if (currentValue == to)
                //    break;

                elapsed = Time.time - startTime;
                currentDelta01 = elapsed / duration;

                if (currentDelta01 >= 1)
                {
                    currentValue = to;
                    currentDelta01 = 1;
                    break;
                }
                currentDelta01 = easingStrategy.CalculateEasing(currentDelta01);
                currentValue = currentValue.Lerp(from.value, to.value, currentDelta01);  
                action?.Invoke(currentValue);

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
            action?.Invoke(currentValue);
        }

        action?.Invoke(currentValue);
    }
    public void TweenValue(Action<float> action, float duration, eEaseType easing, float from, float to)
    {
        easingStrategy = new Easing(easing);
        var task = TweenValueAsync(action, duration, TaskUtil.RefreshToken(ref cancellationTokenSource), from, to);
        task.ConfigureAwait(false);
    }

    public void TweenValue(Action<float> action, float duration, AnimationCurve curve, float from, float to)
    {
        easingStrategy = new AnimationCurveEasing(curve);
        var task = TweenValueAsync(action, duration, TaskUtil.RefreshToken(ref cancellationTokenSource), from, to);
        task.ConfigureAwait(false);
    }

    public void TweenValue(Action<float> action, float duration, IEasingStrategy easingStrategy, float from = 0, float to = 1)
    {
        this.easingStrategy = easingStrategy;
        var task = TweenValueAsync(action, duration, TaskUtil.RefreshToken(ref cancellationTokenSource), from, to);
        task.ConfigureAwait(false);
    }

    public void TweenValue(Action<float> action, float duration, float from = 0, float to = 1)
    {
        var task = TweenValueAsync(action, duration, TaskUtil.RefreshToken(ref cancellationTokenSource), from, to);
        task.ConfigureAwait(false);
    }

    public void TweenValue(Action<Color> action, float duration, IEasingStrategy easingStrategy, Color from, Color to)
    {
        ColorWrapper fromWrapper = new ColorWrapper(from);
        ColorWrapper toWrapper = new ColorWrapper(to);
        Action<ColorWrapper> colorWrapperAction = (color) => { action?.Invoke(color.color); };
        var task = TweenValueAsync<ColorWrapper>(colorWrapperAction, duration, TaskUtil.RefreshToken(ref cancellationTokenSource), fromWrapper, toWrapper);
        task.ConfigureAwait(false);
    }
}