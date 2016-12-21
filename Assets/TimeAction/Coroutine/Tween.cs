using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Tween
{
    readonly static AnimationCurve defaultCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    public static IEnumerator TweenColor(Graphic target, Color end, float time, AnimationCurve curve = null)
    {
        var t = 0f;
        var start = target.color;

        if (curve == null)
            curve = defaultCurve;

        while (t < 1f)
        {
            t += Time.deltaTime / time;
            target.color = Color.Lerp(start, end, time);
            yield return null;
        } 
    }
    public static IEnumerator TweenAlpha(Graphic target, float alphaEnd, float time, AnimationCurve curve= null)
    {
        var t = 0f;
        var color = target.color;

        var start = color;
        var end = color;
        end.a = alphaEnd;

        if (curve == null)
            curve = defaultCurve;

        while (t < 1f)
        {
            t += Time.deltaTime / time;
            target.color = Color.Lerp(start, end, curve.Evaluate(t));
            yield return null;
        }
    }

}

public static class TweenTransform {
    readonly static AnimationCurve defaultCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    public delegate void TweenFunc<T>(Transform target, T a, T b, float t);

    public static IEnumerator Tween(Transform target, Vector3 positionEnd, Vector3 scaleEnd, Vector3 eulerEnd, float time
                                    ,AnimationCurve curve = null)
    {
        var t = 0f;
        var positionStart = target.position;
        var scaleStart = target.localScale;

        var rotationStart = target.rotation;
        var rotationEnd = Quaternion.Euler(eulerEnd);

        if (curve == null)
            curve = defaultCurve;


        while (t < 1f)
        {
            t += Time.deltaTime / time;
            target.position = Vector3.Lerp(positionStart, positionEnd, t);
            target.localScale = Vector3.Lerp(scaleStart, scaleEnd, t);
            target.rotation = Quaternion.Lerp(rotationStart, rotationEnd, t);

            yield return null;
        }
    }

    public static IEnumerator Position(Transform target, Vector3 positionEnd, float time, AnimationCurve curve = null)
    {
        var t = 0f;
        var positionStart = target.position;

        if (curve == null)
            curve = defaultCurve;

        while (t < 1f)
        {
            t += Time.deltaTime / time;
            target.position = Vector3.Lerp(positionStart, positionEnd, t);
            yield return null;
        }
    }

    public static IEnumerator Rotation(Transform target, Quaternion end, float time, AnimationCurve curve=null)
    {
        var t = 0f;
        var rotationStart = target.rotation;
        
        if (curve == null)
            curve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

        while (t < 1f)
        {
            t += Time.deltaTime / time;
            target.rotation = Quaternion.Lerp(rotationStart, end, curve.Evaluate( t));
            yield return null;
        }
    }
}