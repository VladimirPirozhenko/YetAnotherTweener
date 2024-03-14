using System.Collections.Generic;
using UnityEngine;

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

    private float startValue = 0;
    private float endValue = 0;
    private float currentValue = 0;

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
    private List<Transform> gameObjects = new List<Transform>();

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
        obj.transform.Tween(target, startV, duration, curve);
        for (int i = 0; i < 1; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                var newObj = Instantiate(obj, transform.position + new Vector3(i, i, j), transform.rotation);
                newObj.transform.Tween(transform.position + new Vector3(i, i, 0), transform.position + new Vector3(0, 0, j) + new Vector3(0 + 10, 0 + 10, 0 + 10), 10, curve);
                gameObjects.Add(newObj);
            }
        }
        startTime = Time.time;
        endTime = Time.time + duration;
        //Debug.Log(curV);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            obj.transform.StopTween();
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