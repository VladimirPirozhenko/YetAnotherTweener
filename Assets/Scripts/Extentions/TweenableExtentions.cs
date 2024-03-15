#define AsyncTween

using Newtonsoft.Json.Serialization;
using System;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public interface ILerpable<T>
{
    public T Lerp(T from,T to,float t);
}

public class ColorWrapper : ILerpable<Color>
{
    public Color color;
    public ColorWrapper(Color color)
    {
        this.color = color;
    }
    public void Lerp(ColorWrapper from, ColorWrapper to, float t)
    {
        color =  Color.Lerp(from.color, to.color, t);
    }

    public Color Lerp(Color from, Color to, float t)
    {
        color = Color.Lerp(from, to, t);
        return color;   
    }
}


static class TweenableExtentions
{
    #region TweenColor

    #endregion

    #region TweenMove
    public static ITween TweenMove(this Component c, Vector3 from, Vector3 to, float duration, IEasingStrategy easingStrategy)
    {
        return TweenerContext.handler.TweenMove(c,from, to, duration, easingStrategy);
    }
    public static ITween TweenMove(this Component c, Vector3 to, float duration, IEasingStrategy easingStrategy)
    {
        return TweenerContext.handler.TweenMove(c, to, duration, easingStrategy);
    }

    public static ITween TweenMove(this Component c, Vector3 from, Vector3 to, float duration, AnimationCurve curve)
    {
        return TweenerContext.handler.TweenMove(c, from, to, duration, new AnimationCurveEasing(curve));
    }
    public static ITween TweenMove(this Component c, Vector3 to, float duration, AnimationCurve curve)
    {
        return TweenerContext.handler.TweenMove(c, to, duration, new AnimationCurveEasing(curve));
    }
    public static ITween TweenMove(this Component c, Vector3 from, Vector3 to, float duration, eEaseType easing = eEaseType.Linear)
    {
        return TweenerContext.handler.TweenMove(c, from, to, duration, new Easing(easing));
    }

    public static ITween TweenMove(this Component c, Vector3 to, float duration, eEaseType easing = eEaseType.Linear)
    {
        return TweenerContext.handler.TweenMove(c, to, duration, new Easing(easing));
    }
    #endregion

    #region TweenMoveLocal
    public static ITween TweenMoveLocal(this Component c, Vector3 from, Vector3 to, float duration, IEasingStrategy easingStrategy)
    {
        return TweenerContext.handler.TweenMoveLocal(c, from, to, duration, easingStrategy);
    }
    public static ITween TweenMoveLocal(this Component c, Vector3 to, float duration, IEasingStrategy easingStrategy)
    {
        return TweenerContext.handler.TweenMoveLocal(c, to, duration, easingStrategy);
    }

    public static ITween TweenMoveLocal(this Component c, Vector3 from, Vector3 to, float duration, AnimationCurve curve)
    {
        return TweenerContext.handler.TweenMoveLocal(c, from, to, duration, new AnimationCurveEasing(curve));
    }
    public static ITween TweenMoveLocal(this Component c, Vector3 to, float duration, AnimationCurve curve)
    {
        return TweenerContext.handler.TweenMoveLocal(c, to, duration, new AnimationCurveEasing(curve));
    }
    public static ITween TweenMoveLocal(this Component c, Vector3 from, Vector3 to, float duration, eEaseType easing = eEaseType.Linear)
    {
        return TweenerContext.handler.TweenMoveLocal(c, from, to, duration, new Easing(easing));
    }

    public static ITween TweenMoveLocal(this Component c, Vector3 to, float duration, eEaseType easing = eEaseType.Linear)
    {
        return TweenerContext.handler.TweenMoveLocal(c, to, duration, new Easing(easing));
    }
    #endregion

    #region TweenScale
    public static ITween TweenScale(this Component c, Vector3 from, Vector3 to, float duration, IEasingStrategy easingStrategy)
    {
        return TweenerContext.handler.TweenScale(c, from, to, duration, easingStrategy);
    }
    public static ITween TweenScale(this Component c, Vector3 to, float duration, IEasingStrategy easingStrategy)
    {
        return TweenerContext.handler.TweenScale(c, to, duration, easingStrategy);
    }

    public static ITween TweenScale(this Component c, Vector3 from, Vector3 to, float duration, AnimationCurve curve)
    {
        return TweenerContext.handler.TweenScale(c, from, to, duration, new AnimationCurveEasing(curve));
    }
    public static ITween TweenScale(this Component c, Vector3 to, float duration, AnimationCurve curve)
    {
        return TweenerContext.handler.TweenScale(c, to, duration, new AnimationCurveEasing(curve));
    }
    public static ITween TweenScale(this Component c, Vector3 from, Vector3 to, float duration, eEaseType easing = eEaseType.Linear)
    {
        return TweenerContext.handler.TweenScale(c, from, to, duration, new Easing(easing));
    }

    public static ITween TweenScale(this Component c, Vector3 to, float duration, eEaseType easing = eEaseType.Linear)
    {
        return TweenerContext.handler.TweenScale(c, to, duration, new Easing(easing));
    }
    #endregion

    #region TweenRotate
    public static ITween TweenRotate(this Component c, Quaternion from, Quaternion to, float duration, IEasingStrategy easingStrategy)
    {
        return TweenerContext.handler.TweenRotate(c, from, to, duration, easingStrategy);
    }
    public static ITween TweenRotate(this Component c, Quaternion to, float duration, IEasingStrategy easingStrategy)
    {
        return TweenerContext.handler.TweenRotate(c, to, duration, easingStrategy);
    }

    public static ITween TweenRotate(this Component c, Quaternion from, Quaternion to, float duration, AnimationCurve curve)
    {
        return TweenerContext.handler.TweenRotate(c, from, to, duration, new AnimationCurveEasing(curve));
    }
    public static ITween TweenRotate(this Component c, Quaternion to, float duration, AnimationCurve curve)
    {
        return TweenerContext.handler.TweenRotate(c, to, duration, new AnimationCurveEasing(curve));
    }
    public static ITween TweenRotate(this Component c, Quaternion from, Quaternion to, float duration, eEaseType easing = eEaseType.Linear)
    {
        return TweenerContext.handler.TweenRotate(c, from, to, duration, new Easing(easing));
    }

    public static ITween TweenRotate(this Component c, Quaternion to, float duration, eEaseType easing = eEaseType.Linear)
    {
        return TweenerContext.handler.TweenRotate(c, to, duration, new Easing(easing));
    }
    #endregion

    #region TweenRotateLocal
    public static ITween TweenRotateLocal(this Component c, Quaternion from, Quaternion to, float duration, IEasingStrategy easingStrategy)
    {
        return TweenerContext.handler.TweenRotateLocal(c, from, to, duration, easingStrategy);
    }
    public static ITween TweenRotateLocal(this Component c, Quaternion to, float duration, IEasingStrategy easingStrategy)
    {
        return TweenerContext.handler.TweenRotateLocal(c, to, duration, easingStrategy);
    }

    public static ITween TweenRotateLocal(this Component c, Quaternion from, Quaternion to, float duration, AnimationCurve curve)
    {
        return TweenerContext.handler.TweenRotateLocal(c, from, to, duration, new AnimationCurveEasing(curve));
    }
    public static ITween TweenRotateLocal(this Component c, Quaternion to, float duration, AnimationCurve curve)
    {
        return TweenerContext.handler.TweenRotateLocal(c, to, duration, new AnimationCurveEasing(curve));
    }
    public static ITween TweenRotateLocal(this Component c, Quaternion from, Quaternion to, float duration, eEaseType easing = eEaseType.Linear)
    {
        return TweenerContext.handler.TweenRotateLocal(c, from, to, duration, new Easing(easing));
    }

    public static ITween TweenRotateLocal(this Component c, Quaternion to, float duration, eEaseType easing = eEaseType.Linear)
    {
        return TweenerContext.handler.TweenRotateLocal(c, to, duration, new Easing(easing));
    }
    #endregion

    public static void StopTween(this Component c)
    {
        TweenerContext.handler.StopTween(c);
    }

    public static bool TryGetTween(this Component c, out ITween tween)
    {
        return TweenerContext.handler.TryGetTween(c,out tween);
    }
}