using UnityEngine;

class Easings
{
    // https://github.com/pixelflag/EasingTest/blob/master/Assets/script/Easing.cs
    public static float InSine(float t, float min = 0, float max = 1, float totaltime = 1)
    {
        max -= min;
        return -max * Mathf.Cos(t * (Mathf.PI * 90 / 180) / totaltime) + max + min;
    }

    public static float OutSine(float t, float min = 0, float max = 1, float totaltime = 1)
    {
        max -= min;
        return max * Mathf.Sin(t * (Mathf.PI * 90 / 180) / totaltime) + min;
    }

    public static float Linear(float t, float min = 0, float max = 1, float totaltime = 1)
    {
        return (max - min) * t / totaltime + min;
    }

    public static float OutBounce(float t)
    {
        if (t < (1f / 2.75f))
        {
            return 7.5625f * t * t;
        }
        else if (t < (2f / 2.75f))
        {
            return 7.5625f * (t -= (1.5f / 2.75f)) * t + 0.75f;
        }
        else if (t < (2.5f / 2.75f))
        {
            return 7.5625f * (t -= (2.25f / 2.75f)) * t + 0.9375f;
        }
        else
        {
            return 7.5625f * (t -= (2.625f / 2.75f)) * t + 0.984375f;
        }
    }
}