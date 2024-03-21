# Yes, it's Yet Another Tweener! 

Supports animation curves and most kind of easings

Basic usage:
```
transform.TweenMove(fromVector, toVector, duration, new AnimationCurveEasing(curve));
transform.TweenRotate(toQuat, duration, eEaseType.InOutBounce);
transform.TweenScale(toVector, 5f, new Easing(eEaseType.InSine));
```
Usage with TweenHandler(faster)
```
//First param is Action with some of supported types (Vector2, Vector3, Vector4, Color)
TweenerContext.handler.TweenColor((color) => { colorToTween = color; }, from, to, 3,  new Easing(eEaseType.Linear));
TweenerContext.handler.TweenVector2((vec) => { vecToTween = vec; }, from, to, 3,  new Easing(eEaseType.Linear));
```
Usage with Cycles(faster)
```
TweenerContext.handler.Tween(t =>
{
  for (int i = 0; i < gameObjects.Count; i++)
  {
    gameObjects[i].position = Vector3.Lerp(new Vector3(0, 0, 0),new Vector3(10, 10, 10), EaseFunc.Linear(t));
  }
}, 50);
```
Tweener supports coroutines and async, 
You can change context in the runtime
```
TweenerContext.SetContextType(eTweenContextType.Async);
```
