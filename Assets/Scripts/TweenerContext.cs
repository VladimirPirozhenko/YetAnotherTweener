using System;
using UnityEngine;
using UnityEngine.UI;

enum eTweenContextType
{
    Coroutine = 0,
    Async = 1
}

class TweenerContext
{
    public static int tweenCounter = 0;
    private static GameObject parent;
    private static eTweenContextType contextType = eTweenContextType.Async;

    public TweenerContext(eTweenContextType contextType)
    {
        TweenerContext.contextType = contextType;
        parent = new GameObject("Tweeners");
    }

    public static ITween CreateTween(ref Transform t)
    {
        ITween tweenToCreate;
        Func<Transform,ITween> createAsyncTween = (Transform t) => 
        {
            ITween tween = new AsyncTween();
            MonoBehaviour mono = t.gameObject.AddComponent<EmptyMonoBehaviour>();
            mono.destroyCancellationToken.Register(() => { tween.Stop(); });
            return tween;
        };

        switch (contextType)
        {
            case eTweenContextType.Coroutine: 
                GameObject go = new GameObject(t.name + " Tweener");
                tweenToCreate = go.AddComponent<CoroutineTween>();
                go.transform.SetParent(parent.transform);
                break;     
            case eTweenContextType.Async:
                tweenToCreate = createAsyncTween(t);  
                break;
            default:
                tweenToCreate = createAsyncTween(t);
                break;
        }
        TweenHandler.ComponentToTween[t] = tweenToCreate;
        return tweenToCreate;
    }
}