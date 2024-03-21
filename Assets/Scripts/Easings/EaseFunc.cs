using UnityEngine;

public interface IEasingStrategy
{
    public float CalculateEasing(float t);
}

public class AnimationCurveEasing : IEasingStrategy
{
    private AnimationCurve curve;

    public AnimationCurveEasing(AnimationCurve curve)
    {
        this.curve = curve;
    }

    public float CalculateEasing(float t)
    {
        return curve.Evaluate(t);
    }
}

public class Easing : IEasingStrategy
{
    private eEaseType easing;

    public Easing(eEaseType easing)
    {
        this.easing = easing;
    }

    private float EaseValue(eEaseType type, float value)
    {
        switch (type)
        {
            case eEaseType.Linear: return EaseFunc.Linear(value);
            case eEaseType.InSine: return EaseFunc.InSine(value);
            case eEaseType.OutSine: return EaseFunc.OutSine(value);
            case eEaseType.InOutSine: return EaseFunc.InOutSine(value);
            case eEaseType.InQuad: return EaseFunc.InQuad(value);
            case eEaseType.OutQuad: return EaseFunc.OutQuad(value);
            case eEaseType.InOutQuad: return EaseFunc.InOutQuad(value);
            case eEaseType.InCubic: return EaseFunc.InCubic(value);
            case eEaseType.OutCubic: return EaseFunc.OutCubic(value);
            case eEaseType.InOutCubic: return EaseFunc.InOutCubic(value);
            case eEaseType.InQuart: return EaseFunc.InQuart(value);
            case eEaseType.OutQuart: return EaseFunc.OutQuart(value);
            case eEaseType.InOutQuart: return EaseFunc.InOutQuart(value);
            case eEaseType.InQuint: return EaseFunc.InQuint(value);
            case eEaseType.OutQuint: return EaseFunc.OutQuint(value);
            case eEaseType.InOutQuint: return EaseFunc.InOutQuint(value);
            case eEaseType.InBack: return EaseFunc.InBack(value);
            case eEaseType.OutBack: return EaseFunc.OutBack(value);
            case eEaseType.InOutBack: return EaseFunc.InOutBack(value);
            case eEaseType.InCirc: return EaseFunc.InCirc(value);
            case eEaseType.OutCirc: return EaseFunc.OutCirc(value);
            case eEaseType.InOutCirc: return EaseFunc.InOutCirc(value);
            case eEaseType.InElastic: return EaseFunc.InElastic(value);
            case eEaseType.OutElastic: return EaseFunc.OutElastic(value);
            case eEaseType.InOutElastic: return EaseFunc.InOutElastic(value);
            case eEaseType.InBounce: return EaseFunc.InBounce(value);
            case eEaseType.OutBounce: return EaseFunc.OutBounce(value);
            case eEaseType.InOutBounce: return EaseFunc.InOutBounce(value);
            case eEaseType.InExp: return EaseFunc.InExp(value);
            case eEaseType.OutExp: return EaseFunc.OutExp(value);
            case eEaseType.InOutExp: return EaseFunc.InOutExp(value);
            case eEaseType.Constant: return EaseFunc.Constant(value);
            default: return EaseFunc.Linear(value);
        }
    }

    public float CalculateEasing(float t)
    {
        return EaseValue(easing, t);
    }
}

internal class EaseFunc
{
    // https://github.com/pixelflag/EasingTest/blob/master/Assets/script/Easing.cs

    public static float Constant(float t)
    {
        return t;
    }

    public static float Linear(float t, float totaltime = 1, float min = 0, float max = 1)
    {
        return (max - min) * t / totaltime + min;
    }

    public static float InQuad(float t, float totaltime = 1, float min = 0, float max = 1)
    {
        max -= min;
        t /= totaltime;
        return max * t * t + min;
    }

    public static float OutQuad(float t, float totaltime = 1, float min = 0, float max = 1)
    {
        max -= min;
        t /= totaltime;
        return -max * t * (t - 2) + min;
    }

    public static float InOutQuad(float t, float totaltime = 1, float min = 0, float max = 1)
    {
        max -= min;
        t /= totaltime / 2;
        if (t < 1) return max / 2 * t * t + min;

        t = t - 1;
        return -max / 2 * (t * (t - 2) - 1) + min;
    }

    public static float InCubic(float t, float totaltime = 1, float min = 0, float max = 1)
    {
        max -= min;
        t /= totaltime;
        return max * t * t * t + min;
    }

    public static float OutCubic(float t, float totaltime = 1, float min = 0, float max = 1)
    {
        max -= min;
        t = t / totaltime - 1;
        return max * (t * t * t + 1) + min;
    }

    public static float InOutCubic(float t, float totaltime = 1, float min = 0, float max = 1)
    {
        max -= min;
        t /= totaltime / 2;
        if (t < 1) return max / 2 * t * t * t + min;

        t = t - 2;
        return max / 2 * (t * t * t + 2) + min;
    }

    public static float InQuart(float t, float totaltime = 1, float min = 0, float max = 1)
    {
        max -= min;
        t /= totaltime;
        return max * t * t * t * t + min;
    }

    public static float OutQuart(float t, float totaltime = 1, float min = 0, float max = 1)
    {
        max -= min;
        t = t / totaltime - 1;
        return -max * (t * t * t * t - 1) + min;
    }

    public static float InOutQuart(float t, float totaltime = 1, float min = 0, float max = 1)
    {
        max -= min;
        t /= totaltime / 2;
        if (t < 1) return max / 2 * t * t * t * t + min;

        t = t - 2;
        return -max / 2 * (t * t * t * t - 2) + min;
    }

    public static float InQuint(float t, float totaltime = 1, float min = 0, float max = 1)
    {
        max -= min;
        t /= totaltime;
        return max * t * t * t * t * t + min;
    }

    public static float OutQuint(float t, float totaltime = 1, float min = 0, float max = 1)
    {
        max -= min;
        t = t / totaltime - 1;
        return max * (t * t * t * t * t + 1) + min;
    }

    public static float InOutQuint(float t, float totaltime = 1, float min = 0, float max = 1)
    {
        max -= min;
        t /= totaltime / 2;
        if (t < 1) return max / 2 * t * t * t * t * t + min;

        t = t - 2;
        return max / 2 * (t * t * t * t * t + 2) + min;
    }

    public static float InSine(float t, float totaltime = 1, float min = 0, float max = 1)
    {
        max -= min;
        return -max * Mathf.Cos(t * (Mathf.PI * 90 / 180) / totaltime) + max + min;
    }

    public static float OutSine(float t, float totaltime = 1, float min = 0, float max = 1)
    {
        max -= min;
        return max * Mathf.Sin(t * (Mathf.PI * 90 / 180) / totaltime) + min;
    }

    public static float InOutSine(float t, float totaltime = 1, float min = 0, float max = 1)
    {
        max -= min;
        return -max / 2 * (Mathf.Cos(t * Mathf.PI / totaltime) - 1) + min;
    }

    public static float InExp(float t, float totaltime = 1, float min = 0, float max = 1)
    {
        max -= min;
        return t == 0.0 ? min : max * Mathf.Pow(2, 10 * (t / totaltime - 1)) + min;
    }

    public static float OutExp(float t, float totaltime = 1, float min = 0, float max = 1)
    {
        max -= min;
        return t == totaltime ? max + min : max * (-Mathf.Pow(2, -10 * t / totaltime) + 1) + min;
    }

    public static float InOutExp(float t, float totaltime = 1, float min = 0, float max = 1)
    {
        if (t == 0.0f) return min;
        if (t == totaltime) return max;
        max -= min;
        t /= totaltime / 2;

        if (t < 1) return max / 2 * Mathf.Pow(2, 10 * (t - 1)) + min;

        t = t - 1;
        return max / 2 * (-Mathf.Pow(2, -10 * t) + 2) + min;
    }

    public static float InCirc(float t, float totaltime = 1, float min = 0, float max = 1)
    {
        max -= min;
        t /= totaltime;
        return -max * (Mathf.Sqrt(1 - t * t) - 1) + min;
    }

    public static float OutCirc(float t, float totaltime = 1, float min = 0, float max = 1)
    {
        max -= min;
        t = t / totaltime - 1;
        return max * Mathf.Sqrt(1 - t * t) + min;
    }

    public static float InOutCirc(float t, float totaltime = 1, float min = 0, float max = 1)
    {
        max -= min;
        t /= totaltime / 2;
        if (t < 1) return -max / 2 * (Mathf.Sqrt(1 - t * t) - 1) + min;

        t = t - 2;
        return max / 2 * (Mathf.Sqrt(1 - t * t) + 1) + min;
    }

    public static float InElastic(float t, float totaltime = 1, float min = 0, float max = 1)
    {
        max -= min;
        t /= totaltime;

        float s = 1.70158f;
        float p = totaltime * 0.3f;
        float a = max;

        if (t == 0) return min;
        if (t == 1) return min + max;

        if (a < Mathf.Abs(max))
        {
            a = max;
            s = p / 4;
        }
        else
        {
            s = p / (2 * Mathf.PI) * Mathf.Asin(max / a);
        }

        t = t - 1;
        return -(a * Mathf.Pow(2, 10 * t) * Mathf.Sin((t * totaltime - s) * (2 * Mathf.PI) / p)) + min;
    }

    public static float OutElastic(float t, float totaltime = 1, float min = 0, float max = 1)
    {
        max -= min;
        t /= totaltime;

        float s = 0;//1.70158f;
        float p = totaltime * 0.3f; ;
        float a = max;

        if (t == 0) return min;
        if (t == 1) return min + max;

        if (a < Mathf.Abs(max))
        {
            a = max;
            s = p / 4;
        }
        else
        {
            s = p / (2 * Mathf.PI) * Mathf.Asin(max / a);
        }

        return a * Mathf.Pow(2, -10 * t) * Mathf.Sin((t * totaltime - s) * (2 * Mathf.PI) / p) + max + min;
    }

    public static float InOutElastic(float t, float totaltime = 1, float min = 0, float max = 1)
    {
        max -= min;
        t /= totaltime / 2;

        float s = 1.70158f;
        float p = totaltime * (0.3f * 1.5f);
        float a = max;

        if (t == 0) return min;
        if (t == 2) return min + max;

        if (a < Mathf.Abs(max))
        {
            a = max;
            s = p / 4;
        }
        else
        {
            s = p / (2 * Mathf.PI) * Mathf.Asin(max / a);
        }

        if (t < 1)
        {
            return -0.5f * (a * Mathf.Pow(2, 10 * (t -= 1)) * Mathf.Sin((t * totaltime - s) * (2 * Mathf.PI) / p)) + min;
        }

        t = t - 1;
        return a * Mathf.Pow(2, -10 * t) * Mathf.Sin((t * totaltime - s) * (2 * Mathf.PI) / p) * 0.5f + max + min;
    }

    public static float InBack(float t, float s = 1, float totaltime = 1, float min = 0, float max = 1)
    {
        max -= min;
        t /= totaltime;
        return max * t * t * ((s + 1) * t - s) + min;
    }

    public static float OutBack(float t, float s = 1, float totaltime = 1, float min = 0, float max = 1)
    {
        max -= min;
        t = t / totaltime - 1;
        return max * (t * t * ((s + 1) * t + s) + 1) + min;
    }

    public static float InOutBack(float t, float s = 1, float totaltime = 1, float min = 0, float max = 1)
    {
        max -= min;
        s *= 1.525f;
        t /= totaltime / 2;
        if (t < 1) return max / 2 * (t * t * ((s + 1) * t - s)) + min;

        t = t - 2;
        return max / 2 * (t * t * ((s + 1) * t + s) + 2) + min;
    }

    public static float InBounce(float t, float totaltime = 1, float min = 0, float max = 1)
    {
        max -= min;
        return max - OutBounce(totaltime - t, totaltime, 0, max) + min;
    }

    public static float OutBounce(float t, float totaltime = 1, float min = 0, float max = 1)
    {
        max -= min;
        t /= totaltime;

        if (t < 1.0f / 2.75f)
        {
            return max * (7.5625f * t * t) + min;
        }
        else if (t < 2.0f / 2.75f)
        {
            t -= 1.5f / 2.75f;
            return max * (7.5625f * t * t + 0.75f) + min;
        }
        else if (t < 2.5f / 2.75f)
        {
            t -= 2.25f / 2.75f;
            return max * (7.5625f * t * t + 0.9375f) + min;
        }
        else
        {
            t -= 2.625f / 2.75f;
            return max * (7.5625f * t * t + 0.984375f) + min;
        }
    }

    public static float InOutBounce(float t, float totaltime = 1, float min = 0, float max = 1)
    {
        if (t < totaltime / 2)
        {
            return InBounce(t * 2, totaltime, 0, max - min) * 0.5f + min;
        }
        else
        {
            return OutBounce(t * 2 - totaltime, totaltime, 0, max - min) * 0.5f + min + (max - min) * 0.5f;
        }
    }
}