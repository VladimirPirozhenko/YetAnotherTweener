#define AsyncTween

using UnityEngine;

internal static class TweenableExtentions
{
    public static void Tween(this Transform t, Vector3 from, Vector3 to, float duration, eEaseType easing = eEaseType.Linear)
    {
#if AsyncTween//TO TweenManager or such 
         ITween tween = new AsyncTween();
#else
        GameObject go = new GameObject("Tweener");
        ITween tween = go.AddComponent<CoroutineTween>();
#endif
        tween.TweenValue((v) => t.transform.position = Vector3.Lerp(from, to, v), duration, easing);
    }
}