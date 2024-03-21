using System.Collections.Generic;
using UnityEngine;


//TODO:
//DELAYS
//CHANGING VALUES
//INVERT TWEENS
//SEQUENCING
//EXAMPLE USAGE

public class YATweeenExample : MonoBehaviour 
{
    [SerializeField] private Transform obj;
    [SerializeField] private UnityEngine.UI.Button button;
    [SerializeField] private UnityEngine.UI.Image img;
    [SerializeField] private int testObjectCount;
    [SerializeField] private AnimationCurve curve;

    private List<Transform> gameObjects = new List<Transform>();
    private Vector2 vecToTween = new Vector2(20, 0);

    private void Awake()
    {
        TweenerContext.SetContextType(eTweenContextType.Async);
    }

    private void Start()
    {
        obj.transform.position = transform.position;
        for (int i = 0; i < testObjectCount; i++)
        {
            var newObj = Instantiate(obj, transform.position + new Vector3(0, 0, 0), transform.rotation);
            gameObjects.Add(newObj);
        }

  
        obj.TweenMove(new Vector3(0, 5, 0), 5f, curve);
        obj.TweenRotate(Quaternion.Euler(0, 44.4567f, 0), 5f, eEaseType.InOutBounce);
        obj.TweenScale(new Vector3(4, 4, 4), 5f, new Easing(eEaseType.InSine));


        var from = Color.blue;
        var to = Color.red;

        TweenerContext.handler.TweenColor((color) => { img.color = color; }, from, to, 3, new AnimationCurveEasing(curve));
        TweenerContext.handler.TweenVector2((vec) => { vecToTween = vec; }, vecToTween, new Vector2(0, 5), 3, new AnimationCurveEasing(curve));

        bool isManyObjectsInOneTween = true;
        
        if (!isManyObjectsInOneTween)
        {
            foreach (var obj in gameObjects)
            {
                TweenerContext.handler.Tween(t => { obj.position = Vector3.Lerp(new Vector3(0, 0, 0), new Vector3(10, 10, 10), EaseFunc.Linear(t)); }, 50);
            }
        }
        else 
        {
            TweenerContext.handler.Tween(t =>
            {
                for (int i = 0; i < gameObjects.Count; i++)
                {
                    gameObjects[i].position = Vector3.Lerp(new Vector3(0, 0, 0),
                        new Vector3(10, 10, 10), EaseFunc.Linear(t));
                }
            }, 50);
        }

        button.TweenScale(new Vector3(3, 3, 3), 5f, new AnimationCurveEasing(curve));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TweenerContext.handler.StopAllTweens();
        }
    }
}