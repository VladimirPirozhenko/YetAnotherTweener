using System;
using System.Collections.Generic;
using UnityEngine;


class TweenHandler
{
    private Dictionary<Component, ITween> componentToTween = new Dictionary<Component, ITween>();
    private List<ITween> unattachedTweens = new List<ITween>();
    private eTweenContextType contextType = eTweenContextType.Async;
    private GameObject parent;
    public TweenHandler(eTweenContextType contextType)
    {
        this.contextType = contextType; 
        parent = new GameObject("TweenHandler");
    }

    public void ChangeContextType(eTweenContextType contextType)
    {
        this.contextType = contextType;
    }

    public ITween CreateTween(Component c)
    {
        ITween tweenToCreate;
        Func<Component, ITween> createAsyncTween = (Component c) =>
        {
            ITween tween = new AsyncTween();
            MonoBehaviour mono = c.gameObject.AddComponent<EmptyMonoBehaviour>();
            mono.destroyCancellationToken.Register(() => { tween.Stop(); });
            return tween;
        };

        switch (contextType)
        {
            case eTweenContextType.Coroutine:
                GameObject go = new GameObject(c.name + " Tweener");
                tweenToCreate = go.AddComponent<CoroutineTween>();
                go.transform.SetParent(parent.transform);
                break;
            case eTweenContextType.Async:
                tweenToCreate = createAsyncTween(c);
                break;
            default:
                tweenToCreate = createAsyncTween(c);
                break;
        }
        RegisterTween(c, tweenToCreate);
        return tweenToCreate;
    }

    public ITween CreateTween()
    {
        ITween tweenToCreate;
        Func<ITween> createAsyncTween = () =>
        {
            ITween tween = new AsyncTween();
            return tween;
        };

        switch (contextType)
        {
            case eTweenContextType.Coroutine:
                GameObject go = new GameObject("Tweener");
                tweenToCreate = go.AddComponent<CoroutineTween>();
                go.transform.SetParent(parent.transform);
                break;
            case eTweenContextType.Async:
                tweenToCreate = createAsyncTween();
                break;
            default:
                tweenToCreate = createAsyncTween();
                break;
        }
        RegisterTween(tweenToCreate);
        return tweenToCreate;
    }

    public void RegisterTween(Component c,ITween tween)
    {
        if (componentToTween.ContainsKey(c))
            Debug.LogError($"Tween already exitsts, overridding tween for {c.name}");
        componentToTween[c] = tween;
    }

    public void RegisterTween(ITween tween)
    {
        unattachedTweens.Add(tween);    
    }

    public void StopAllTweens()
    {
        foreach (var entry in componentToTween)
            entry.Value.Stop();
        foreach (var tween in unattachedTweens)
            tween.Stop();
    }

    public ITween GetOrCreateTween(Component c)
    {
        ITween tween;
        if (componentToTween.ContainsKey(c))
            tween = componentToTween[c];
        else
            tween = CreateTween(c);
        return tween;
    }
    #region Tween
    public ITween Tween(Action<float> action, float duration)
    {
        ITween tween = CreateTween();
        tween.TweenValue(action, duration);
        return tween;
    }
    public ITween Tween(Action<float> action, float duration, IEasingStrategy strategy)
    {
        ITween tween = CreateTween();
        tween.TweenValue(action, duration, strategy);
        return tween;
    }
    public ITween Tween(Action<float> action, float duration, AnimationCurve curve)
    {
        ITween tween = CreateTween();
        tween.TweenValue(action, duration, curve);
        return tween;
    }
    public ITween Tween(Action<float> action, float duration,eEaseType easeType)
    {
        ITween tween = CreateTween();
        tween.TweenValue(action, duration, easeType);
        return tween;
    }
    #endregion

    #region TweenMove
    public ITween TweenMove(Component c, Vector3 to, float duration, IEasingStrategy strategy)
    {
        ITween tween = GetOrCreateTween(c);
        Vector3 from = c.transform.position;
        tween.TweenValue((v) => c.transform.position = Vector3.Lerp(from, to, v), duration, strategy);
        return tween;
    }

    public ITween TweenMove(Component c, Vector3 from, Vector3 to, float duration, IEasingStrategy strategy)
    {
        ITween tween = GetOrCreateTween(c);
        tween.TweenValue((v) => c.transform.position = Vector3.Lerp(from, to, v), duration, strategy);
        return tween;
    }

    #endregion

    #region TweenMoveLocal
    public ITween TweenMoveLocal(Component c, Vector3 to, float duration, IEasingStrategy strategy)
    {
        ITween tween = GetOrCreateTween(c);
        Vector3 from = c.transform.localPosition;
        tween.TweenValue((v) => c.transform.localPosition = Vector3.Lerp(from, to, v), duration, strategy);
        return tween;
    }
    public ITween TweenMoveLocal(Component c, Vector3 from, Vector3 to, float duration, IEasingStrategy strategy)
    {
        ITween tween = GetOrCreateTween(c);
        tween.TweenValue((v) => c.transform.localPosition = Vector3.Lerp(from, to, v), duration, strategy);
        return tween;
    }

    //public ITween TweenMoveLocal(Component c, Vector3 from, Vector3 to, float duration, eEaseType easing = eEaseType.Linear)
    //{
    //    ITween tween = GetOrCreateTween(c);
    //    tween.TweenValue((v) => c.transform.localPosition = Vector3.Lerp(from, to, v), duration, easing);
    //    return tween;
    //}

    //public ITween TweenMoveLocal(Component c, Vector3 to, float duration, AnimationCurve curve)
    //{
    //    ITween tween = GetOrCreateTween(c);
    //    Vector3 from = c.transform.localPosition;
    //    tween.TweenValue((v) => c.transform.localPosition = Vector3.Lerp(from, to, v), duration, curve);
    //    return tween;
    //}

    //public ITween TweenMoveLocal(Component c, Vector3 from, Vector3 to, float duration, AnimationCurve curve)
    //{
    //    ITween tween = GetOrCreateTween(c);
    //    tween.TweenValue((v) => c.transform.localPosition = Vector3.Lerp(from, to, v), duration, curve);
    //    return tween;
    //}

    #endregion

    #region TweenScale
    public ITween TweenScale(Component c, Vector3 to, float duration, IEasingStrategy strategy)
    {
        ITween tween = GetOrCreateTween(c);
        Vector3 from = c.transform.localScale;
        tween.TweenValue((v) => c.transform.localScale = Vector3.Lerp(from, to, v), duration, strategy);
        return tween;
    }

    public ITween TweenScale(Component c, Vector3 from, Vector3 to, float duration, IEasingStrategy strategy)
    {
        ITween tween = GetOrCreateTween(c);
        tween.TweenValue((v) => c.transform.localScale = Vector3.Lerp(from, to, v), duration, strategy);
        return tween;
    }

    #endregion

    #region TweenRotate
    public ITween TweenRotate(Component c, Quaternion to, float duration, IEasingStrategy strategy)
    {
        ITween tween = GetOrCreateTween(c);
        Quaternion from = c.transform.rotation;
        tween.TweenValue((v) => c.transform.rotation = Quaternion.Slerp(from, to, v), duration, strategy);
        return tween;
    }

    public ITween TweenRotate(Component c, Quaternion from, Quaternion to, float duration, IEasingStrategy strategy)
    {
        ITween tween = GetOrCreateTween(c);
        tween.TweenValue((v) => c.transform.rotation = Quaternion.Slerp(from, to, v), duration, strategy);
        return tween;
    }
    public ITween TweenRotateLocal(Component c, Quaternion to, float duration, IEasingStrategy strategy)
    {
        ITween tween = GetOrCreateTween(c);
        Quaternion from = c.transform.localRotation;
        tween.TweenValue((v) => c.transform.localRotation = Quaternion.Slerp(from, to, v), duration, strategy);
        return tween;
    }

    public ITween TweenRotateLocal(Component c, Quaternion from, Quaternion to, float duration, IEasingStrategy strategy)
    {
        ITween tween = GetOrCreateTween(c);
        tween.TweenValue((v) => c.transform.localRotation = Quaternion.Slerp(from, to, v), duration, strategy);
        return tween;
    }
    #endregion

    #region TweenColor
    public ITween TweenColor(Action<Color> action,Color from, Color to, float duration, IEasingStrategy strategy)
    {
        ITween tween = CreateTween();
        Color color = from;
        
        tween.TweenValue(action, duration, strategy,from,to);
        //action?.Invoke(color);
        return tween;
    }

    #endregion
    public void StopTween(Component c)
    {
        if (componentToTween.ContainsKey(c))
            componentToTween[c].Stop();
        else
            Debug.LogError($"No tween for transform {c.name} exists");
    }

    public bool TryGetTween(Component c, out ITween tween)
    {
        if (componentToTween.ContainsKey(c))
        {
            tween = componentToTween[c];
            return true;
        }
        Debug.LogError($"No tween for transform {c.name} exists");
        tween = null;
        return false;
    }

    public void ClearTweens(Component c)
    {
       // if (ComponentToTween.ContainsKey(c))
        //    GameObject.Destroy(ComponentToTween[c] as ,0); 
        //    ComponentToTween[c] = null;
    }
}