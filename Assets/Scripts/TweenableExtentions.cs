#define AsyncTween

using System.Threading;
using UnityEngine;

static class TweenableExtentions
{ 
    public static void Tween(this Transform t, Vector3 from, Vector3 to, float duration, AnimationCurve curve)
    {
        var tween = TweenerContext.CreateTween(ref t);
        tween.TweenValue((v) => t.transform.position = Vector3.Lerp(from, to, v), duration, curve);

        //#if AsyncTween//TO TweenManager or such
        //        ITween tween = new AsyncTween();
        //#else
        //        GameObject go = new GameObject("Tweener");
        //        ITween tween = go.AddComponent<CoroutineTween>();
        //#endif
        //        TweenHandler.ComponentToTween[t] = tween;
        //        tween.TweenValue((v) => t.transform.position = Vector3.Lerp(from, to, v), duration, curve);
    }

    public static void Tween(this Transform t, Vector3 from, Vector3 to, float duration, eEaseType easing = eEaseType.Linear)
    {
#if AsyncTween//TO TweenManager or such
        ITween tween = new AsyncTween();
#else
        GameObject go = new GameObject("Tweener");
        ITween tween = go.AddComponent<CoroutineTween>();
#endif
        TweenHandler.ComponentToTween[t] = tween;
        tween.TweenValue((v) => t.transform.position = Vector3.Lerp(from, to, v), duration, easing);
    }

    public static void StopTween(this Transform t)
    {
        if (TweenHandler.ComponentToTween.ContainsKey(t))
            TweenHandler.ComponentToTween[t].Stop();
        else
            Debug.LogError($"No tween for transform {t.name} exists");
    }

    public static bool TryGetTween(this Transform t, out ITween tween)
    {
        if (TweenHandler.ComponentToTween.ContainsKey(t))
        {
            tween = TweenHandler.ComponentToTween[t];
            return true;
        }
        Debug.LogError($"No tween for transform {t.name} exists");
        tween = null;
        return false;
    }
}