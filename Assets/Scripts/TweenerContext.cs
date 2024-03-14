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
        switch (contextType)
        {
            case eTweenContextType.Coroutine:
                {
                    GameObject go = new GameObject(t.name = "Tweener");
                    tweenToCreate = go.AddComponent<CoroutineTween>();
                    go.transform.SetParent(parent.transform);
                    break;
                }
            case eTweenContextType.Async:
                tweenToCreate = new AsyncTween();
                //var o =  t.gameObject as MonoBehaviour;
                //destroyCancellationToken.Register(() => { tweenToCreate.Stop(); });
                break;

            default:
                tweenToCreate = new AsyncTween();
                break;
        }
        TweenHandler.ComponentToTween[t] = tweenToCreate;
        return tweenToCreate;
    }
}