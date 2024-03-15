#define AsyncTween

using UnityEngine;
using UnityEngine.EventSystems;


static class TweenableExtentions
{
    #region TweenMove
    public static ITween TweenMove(this Component c, Vector3 from, Vector3 to, float duration, AnimationCurve curve)
    {
        ITween tween = TweenerContext.handler.CreateTween(c);
        tween.TweenValue((v) => c.transform.position = Vector3.Lerp(from, to, v), duration, curve);
        return tween;
    }

    public static ITween TweenMove(this Component c, Vector3 from, Vector3 to, float duration, eEaseType easing = eEaseType.Linear)
    {
        ITween tween = TweenerContext.handler.CreateTween(c);
        tween.TweenValue((v) => c.transform.position = Vector3.Lerp(from, to, v), duration, easing);
        return tween;
    }

    public static ITween TweenMove(this Component c, Vector3 to, float duration, AnimationCurve curve)
    {
        ITween tween = TweenerContext.handler.CreateTween(c);
        Vector3 from = c.transform.position;
        tween.TweenValue((v) => c.transform.position = Vector3.Lerp(from, to, v), duration, curve);
        return tween;
    }

    public static ITween TweenMove(this Component c,  Vector3 to, float duration, eEaseType easing = eEaseType.Linear)
    {
        return TweenerContext.handler.TweenMove(c, to, duration, new Easing(easing));
    }
    public static ITween TweenMove(this Component c, Vector3 to, float duration, IEasingStrategy easingStrategy)
    {
        return TweenerContext.handler.TweenMove(c, to, duration, easingStrategy);
    }

    public static ITween TweenMove(this UIBehaviour ui, Vector3 to, float duration, eEaseType easing = eEaseType.Linear)
    {
        return TweenerContext.handler.TweenMoveLocal(ui, to, duration, new Easing(easing));
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