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

    private static eTweenContextType contextType = eTweenContextType.Async;
    public static TweenHandler handler;
    public TweenerContext(eTweenContextType contextType)
    {
        TweenerContext.contextType = contextType;
        handler = new TweenHandler(contextType);
    }

    void setContextType(eTweenContextType contextType) 
    {
        TweenerContext.contextType = contextType;
        handler.ChangeContextType(contextType);
    }
}