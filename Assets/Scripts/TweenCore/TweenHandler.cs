using System;
using System.Collections.Generic;
using UnityEngine;


class TweenHandler
{
    private Dictionary<Component, List<ITween>> componentToTween = new Dictionary<Component, List<ITween>>();

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

    #region TweenCreation
    public ITween CreateTween(Component c)
    {
        ITween tweenToCreate;
        Func<Component, ITween> createAsyncTween = (Component c) =>
        {
            ITween tween = new AsyncTween();
            EmptyMonoBehaviour emptyMono = null;
            if (!c.gameObject.TryGetComponent(out emptyMono))
                emptyMono = c.gameObject.AddComponent<EmptyMonoBehaviour>();

            emptyMono.destroyCancellationToken.Register(() =>
            {
                if (componentToTween.ContainsKey(c))
                {
                    List<ITween> existingTweens = componentToTween[c];    
                    for (int i = 0; i < existingTweens.Count;i++)
                    {
                        existingTweens[i].Stop();   
                        //existingTweens[i] = null;
                    }
                    //componentToTween[c].Clear();
                }
            });
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

    private void RegisterTween(Component c,ITween tween)
    {
        //if (componentToTween.ContainsKey(c))
        //    Debug.LogError($"Tween already exitsts, overridding tween for {c.name}");
        if (!componentToTween.ContainsKey(c))
            componentToTween.Add(c,new List<ITween>());

        componentToTween[c].Add(tween);
    }

    private void RegisterTween(ITween tween)
    {
        unattachedTweens.Add(tween);    
    }

    #endregion

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
        ITween tween = CreateTween(c);
        Vector3 from = c.transform.position;
        tween.TweenValue((v) => c.transform.position = Vector3.Lerp(from, to, v), duration, strategy);
        return tween;
    }

    public ITween TweenMove(Component c, Vector3 from, Vector3 to, float duration, IEasingStrategy strategy)
    {
        ITween tween = CreateTween(c);
        tween.TweenValue((v) => c.transform.position = Vector3.Lerp(from, to, v), duration, strategy);
        return tween;
    }

    #endregion

    #region TweenMoveLocal
    public ITween TweenMoveLocal(Component c, Vector3 to, float duration, IEasingStrategy strategy)
    {
        ITween tween = CreateTween(c);
        Vector3 from = c.transform.localPosition;
        tween.TweenValue((v) => c.transform.localPosition = Vector3.Lerp(from, to, v), duration, strategy);
        return tween;
    }
    public ITween TweenMoveLocal(Component c, Vector3 from, Vector3 to, float duration, IEasingStrategy strategy)
    {
        ITween tween = CreateTween(c);
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
        ITween tween = CreateTween(c);
        Vector3 from = c.transform.localScale;
        tween.TweenValue((v) => c.transform.localScale = Vector3.Lerp(from, to, v), duration, strategy);
        return tween;
    }

    public ITween TweenScale(Component c, Vector3 from, Vector3 to, float duration, IEasingStrategy strategy)
    {
        ITween tween = CreateTween(c);
        tween.TweenValue((v) => c.transform.localScale = Vector3.Lerp(from, to, v), duration, strategy);
        return tween;
    }

    #endregion

    #region TweenRotate
    public ITween TweenRotate(Component c, Quaternion to, float duration, IEasingStrategy strategy)
    {
        ITween tween = CreateTween(c);
        Quaternion from = c.transform.rotation;
        tween.TweenValue((v) => c.transform.rotation = Quaternion.Slerp(from, to, v), duration, strategy);
        return tween;
    }

    public ITween TweenRotate(Component c, Quaternion from, Quaternion to, float duration, IEasingStrategy strategy)
    {
        ITween tween = CreateTween(c);
        tween.TweenValue((v) => c.transform.rotation = Quaternion.Slerp(from, to, v), duration, strategy);
        return tween;
    }
    public ITween TweenRotateLocal(Component c, Quaternion to, float duration, IEasingStrategy strategy)
    {
        ITween tween = CreateTween(c);
        Quaternion from = c.transform.localRotation;
        tween.TweenValue((v) => c.transform.localRotation = Quaternion.Slerp(from, to, v), duration, strategy);
        return tween;
    }

    public ITween TweenRotateLocal(Component c, Quaternion from, Quaternion to, float duration, IEasingStrategy strategy)
    {
        ITween tween = CreateTween(c);
        tween.TweenValue((v) => c.transform.localRotation = Quaternion.Slerp(from, to, v), duration, strategy);
        return tween;
    }
    #endregion

    #region TweenColor
    public ITween TweenColor(Action<Color> action,Color from, Color to, float duration, IEasingStrategy strategy)
    {
        ITween tween = CreateTween();
        tween.TweenValue(action, duration, strategy,from,to);
        return tween;
    }
    #endregion

    #region TweenVector
    public ITween TweenVector2(Action<Vector2> action, Vector2 from, Vector2 to, float duration, IEasingStrategy strategy)
    {
        ITween tween = CreateTween();
        tween.TweenValue(action, duration, strategy, from, to);
        return tween;
    }
    public ITween TweenVector3(Action<Vector3> action, Vector3 from, Vector3 to, float duration, IEasingStrategy strategy)
    {
        ITween tween = CreateTween();
        tween.TweenValue(action, duration, strategy, from, to);
        return tween;
    }
    public ITween TweenVector4(Action<Vector4> action, Vector4 from, Vector4 to, float duration, IEasingStrategy strategy)
    {
        ITween tween = CreateTween();
        tween.TweenValue(action, duration, strategy, from, to);
        return tween;
    }
    #endregion

    public void StopAllTweens()
    {
        foreach (var entry in componentToTween)
        {
            List<ITween> tweens = entry.Value; 
            for (var i = 0; i < tweens.Count;i++)
            {
                tweens[i].Stop();   
            }
        }
            
        foreach (var tween in unattachedTweens)
            tween.Stop();
    }

    public void StopTweens(Component c)
    {
        if (componentToTween.ContainsKey(c))
        {
            List<ITween> tweens = componentToTween[c];
            for (var i = 0; i < tweens.Count; i++)
            {
                tweens[i].Stop();
            }
        }
        else
            Debug.LogError($"No tween for transform {c.name} exists");
    }

    public void StopTween(Component c)
    {
        if (componentToTween.ContainsKey(c))
            componentToTween[c][0].Stop();
        else
            Debug.LogError($"No tween for transform {c.name} exists");
    }

    public bool TryGetTween(Component c, out ITween tween)//Get first tween of that component
    {
        if (componentToTween.ContainsKey(c))
        {
            tween = componentToTween[c][0];
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