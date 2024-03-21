
enum eTweenContextType
{
    Coroutine = 0,
    Async = 1
}

static class TweenerContext
{
    private static eTweenContextType contextType = eTweenContextType.Async;
    public static TweenHandler handler;
    static TweenerContext()
    {
        contextType = eTweenContextType.Async;
        handler = new TweenHandler(contextType);
    }

    public static void SetContextType(eTweenContextType contextType) 
    {
        TweenerContext.contextType = contextType;
        handler.ChangeContextType(contextType);
    }
}