using System;
using System.Collections.Generic;

public enum eEaseType
{
    Linear = 1,
    InSine = 2,
    OutSine = 3,
    OutBounce = 4
}

internal class EaseApplier
{
    private static Dictionary<eEaseType, Func<float, float, float, float, float>> easeTypeToFunction = new Dictionary<eEaseType, Func<float, float, float, float, float>>()
    {
        [eEaseType.Linear] = Easings.Linear,
        [eEaseType.InSine] = Easings.InSine,
        [eEaseType.OutSine] = Easings.OutSine
    };

    public static float Apply(eEaseType type, float value)
    {
        switch (type)
        {
            case eEaseType.Linear: return Easings.Linear(value);
            case eEaseType.InSine: return Easings.InSine(value);
            case eEaseType.OutSine: return Easings.OutSine(value);
            case eEaseType.OutBounce: return Easings.OutBounce(value);
            default: return Easings.Linear(value);
        }
    }

    //static Func<float, float> GetEaseAction(EzEaseType type)
    //{
    //    switch (type)
    //    {
    //        case EzEaseType.SineIn: return (v) => Easing.SineIn(v);
    //        case EzEaseType.SineOut: return (v) => Easing.SineOut(v);
    //        case EzEaseType.SineInOut: return (v) => Easing.SineInOut(v);
    //        case EzEaseType.QuadIn: return (v) => Easing.QuadIn(v);
    //        case EzEaseType.QuadOut: return (v) => Easing.QuadOut(v);
    //        case EzEaseType.QuadInOut: return (v) => Easing.QuadInOut(v);
    //        case EzEaseType.CubicIn: return (v) => Easing.CubicIn(v);
    //        case EzEaseType.CubicOut: return (v) => Easing.CubicOut(v);
    //        case EzEaseType.CubicInOut: return (v) => Easing.CubicInOut(v);
    //        case EzEaseType.QuartIn: return (v) => Easing.QuartIn(v);
    //        case EzEaseType.QuartOut: return (v) => Easing.QuartOut(v);
    //        case EzEaseType.QuartInOut: return (v) => Easing.QuartInOut(v);
    //        case EzEaseType.ExpIn: return (v) => Easing.ExpIn(v);
    //        case EzEaseType.ExpOut: return (v) => Easing.ExpOut(v);
    //        case EzEaseType.ExpInOut: return (v) => Easing.ExpInOut(v);
    //        case EzEaseType.CircIn: return (v) => Easing.CircIn(v);
    //        case EzEaseType.CircOut: return (v) => Easing.CircOut(v);
    //        case EzEaseType.CircInOut: return (v) => Easing.CircInOut(v);
    //        case EzEaseType.ElasticIn: return (v) => Easing.ElasticIn(v);
    //        case EzEaseType.ElasticOut: return (v) => Easing.ElasticOut(v);
    //        case EzEaseType.ElasticInOut: return (v) => Easing.ElasticInOut(v);
    //        case EzEaseType.BackIn: return (v) => Easing.BackIn(v);
    //        case EzEaseType.BackOut: return (v) => Easing.BackOut(v);
    //        case EzEaseType.BackInOut: return (v) => Easing.BackInOut(v);
    //        case EzEaseType.BounceIn: return (v) => Easing.BounceIn(v);
    //        case EzEaseType.BounceOut: return (v) => Easing.BounceOut(v);
    //        case EzEaseType.BounceInOut: return (v) => Easing.BounceInOut(v);
    //        case EzEaseType.Linear: return (v) => Easing.Linear(v);
    //        default: return (v) => Easing.Linear(v);
    //    }
    //}
}