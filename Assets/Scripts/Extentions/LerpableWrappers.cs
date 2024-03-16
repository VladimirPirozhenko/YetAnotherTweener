
using UnityEngine;

public interface ILerpable<T> where T : ILerpable<T>
{
    public T Lerp(T from, T to, float t);
}

public struct ColorWrapper : ILerpable<ColorWrapper>
{
    public Color color;
    public ColorWrapper(Color color)
    {
        this.color = color;
    }
    public ColorWrapper Lerp(ColorWrapper from, ColorWrapper to, float t) => new ColorWrapper(Color.Lerp(from.color, to.color, t));
}

public struct Vector2Wrapper : ILerpable<Vector2Wrapper>
{
    public Vector2 vec;
    public Vector2Wrapper(Vector2 vec)
    {
        this.vec = vec;
    }
    public Vector2Wrapper Lerp(Vector2Wrapper from, Vector2Wrapper to, float t) => new Vector2Wrapper(Vector2.Lerp(from.vec, to.vec, t));
}

public struct Vector3Wrapper : ILerpable<Vector3Wrapper>
{
    public Vector3 vec;
    public Vector3Wrapper(Vector3 vec)
    {
        this.vec = vec;
    }
    public Vector3Wrapper Lerp(Vector3Wrapper from, Vector3Wrapper to, float t) => new Vector3Wrapper(Vector3.Lerp(from.vec, to.vec, t));
}

public struct Vector4Wrapper : ILerpable<Vector4Wrapper>
{
    public Vector4 vec;
    public Vector4Wrapper(Vector4 vec)
    {
        this.vec = vec;
    }
    public Vector4Wrapper Lerp(Vector4Wrapper from, Vector4Wrapper to, float t) => new Vector4Wrapper(Vector4.Lerp(from.vec, to.vec, t));
}