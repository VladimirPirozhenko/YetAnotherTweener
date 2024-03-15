using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

//TODO:
//ANIMATION CURVES
//REST OF THE EASINGS
//DELAYS
//CHANGING VALUES
//INVERT TWEENS
//SEQUENCING

public class YATweeen : MonoBehaviour
{
    [SerializeField] private Transform obj;
    [SerializeField] private UnityEngine.UI.Button button;
    [SerializeField] private UnityEngine.UI.Image img;
    private Vector3 startV = Vector3.zero;
    private Vector3 endV = Vector3.one;
    private Vector3 curV = Vector3.zero;

    private LineRenderer lineRenderer;
    private Vector3 target = new Vector3(0, 5, 0);
    private float startTime;
    private float endTime;
    private float duration = 4.3f;
    [SerializeField] private AnimationCurve curve;

    // Start is called before the first frame update
    private Transform[] gameObjects = new Transform[50000];

    private TweenerContext tweenerCtx;

    private void Awake()
    {
        tweenerCtx = new TweenerContext(eTweenContextType.Async);
    }

    private void Start()
    {
        //  curve.ClearKeys();

        lineRenderer = GetComponent<LineRenderer>();
        obj.transform.position = transform.position;
        //var Task = TweenValue(0, 30, 3);
        //Tween(startV, endV, 3);

        //obj.transform.Tween(target, startV, duration, eEaseType.OutBounce);
        //obj.transform.Tween(target, startV, duration, curve);
        int rows = 100;
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                var newObj = Instantiate(obj, transform.position + new Vector3(0, 0, 0), transform.rotation);
               // newObj.GetComponent<Renderer>().material.enableInstancing = true;
                newObj.transform.TweenMove( new Vector3(0, 0, 0), transform.position + new Vector3(i, i, j) + new Vector3(0 + 10, 0 + 10, 0 + 10), 5, new AnimationCurveEasing(curve));
                newObj.transform.TweenRotate(Quaternion.Euler(0, 0, 0), Quaternion.Euler(i, j, i), 5, new AnimationCurveEasing(curve));
                //newObj.transform.TweenScale(new Vector3(0, 0, 0), transform.position + new Vector3(i, i, j) + new Vector3(0 + 10, 0 + 10, 0 + 10), 5, new AnimationCurveEasing(curve));
                gameObjects[rows * i + j] = newObj; 
            }
        }
       
        //ITween tween = TweenerContext.handler.Tween(t =>
        //{
        //    var start = Color.blue;
        //    var end = Color.red;
        //    //var vectorCol = Vector4.Lerp(start, end, EaseFunc.Linear(t));
        //    img.color = Vector4.Lerp(start, end, EaseFunc.Linear(t));
          
            
        //},5f);
        var start = Color.blue;
        var end = Color.red;
        //img.color.TweenColor(start, end, 5f, new AnimationCurveEasing(curve));
        //(color) => { img.color = color; }
        TweenerContext.handler.TweenColor((color) => { img.color = color; }, start, end,3,new AnimationCurveEasing(curve));

        // button.transform.localPosition = new Vector3(0, 5, 0);
        //   button.GetComponent<RectTransform>().localPosition = new Vector3(0, 5, 0);
        // button.TweenMove(new Vector3(0,200,0), 5, eEaseType.OutSine);    
        // button.TweenMove(new Vector3(0, 200, 0), 5, new PredefinedEasing(eEaseType.InSine));
        //var start = button.transform.localPosition;
        //ITween tween = TweenerContext.handler.Tween(t =>
        //{

        //    for (int i = 0; i < 500; i++)
        //    {
        //        for (int j = 0; j < rows; j++)
        //        {
        //           // Action<float> action = (t) =>
        //           // {
        //           // gameObjects[rows * i + j].transform.position = Vector3.Lerp(new Vector3(0, 0, 0), new Vector3(i, i, j) + new Vector3(0 + 10, 0 + 10, 0 + 10), Easings.Linear(t));

        //           // };
        //           // tween.TweenValue(action, 5);
        //        }
        //    }
            //  obj.transform.localScale = Vector3.Lerp(new Vector3(0.5f, 0.5f, 0.5f), endV, Easings.Linear(t));

            // button.transform.localPosition = Vector3.Lerp(start, new Vector3(0.5f, 300f, 0.5f), Easings.Linear(t));
            // button.transform.localScale = Vector3.Lerp(new Vector3(0.5f, 0.5f, 0.5f), endV, curve.Evaluate(t));
       // }, 5f);
        //ITween tween = new AsyncTween();
        //for (int i = 0; i < 500; i++)
        //{
        //    for (int j = 0; j < rows; j++)
        //    {
        //        Action<float> action = (t) =>
        //        {
        //            gameObjects[rows * i + j].transform.position = Vector3.Lerp(new Vector3(0, 0, 0), new Vector3(i, i, j) + new Vector3(0 + 10, 0 + 10, 0 + 10), Easings.Linear(t));

        //        };
        //        tween.TweenValue(action, 5);
        //    }
        //}

        startTime = Time.time;
        endTime = Time.time + duration;

        //Debug.Log(curV);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //obj.transform.StopTween();
            TweenerContext.handler.StopAllTweens(); 
        }
        foreach (var obj in gameObjects)
        {
            //obj.Translate(new Vector3(0,0.1f,0));
        }
        //Debug.Log(TweenerContext.tweenCounter);
        //lineRenderer.startWidth = 0.1f;
        //lineRenderer.endWidth = 0.1f;
        //lineRenderer.positionCount++;
        //lineRenderer.SetPosition(lineRenderer.positionCount-1, target - obj.transform.position * (endTime-Time.time));
        // curV = Vector3.Lerp(from, to, tweenable.CurrentDelta01);
        // Debug.Log(obj.transform.position);
        // Debug.Log(v.Tween(startV, endV, 30));
        //Debug.Log(tweenable.CurrentDelta01);
        // Debug.Log(currentValue);
    }
}