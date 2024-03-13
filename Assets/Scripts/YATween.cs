using UnityEngine;

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
    [SerializeField] AnimationCurve curve;  
    // Start is called before the first frame update
    private void Start()
    {
        curve.ClearKeys();
        
        lineRenderer = GetComponent<LineRenderer>();
        obj.transform.position = transform.position;
        //var Task = TweenValue(0, 30, 3);
        //Tween(startV, endV, 3);

        obj.transform.Tween(target, startV, duration, eEaseType.OutBounce);
        startTime = Time.time;
        endTime = Time.time + duration;
        //Debug.Log(curV);
    }

    // Update is called once per frame
    private void Update()
    {
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