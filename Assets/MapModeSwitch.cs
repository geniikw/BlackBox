using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapModeSwitch : MonoBehaviour {

    public Transform cubes;
    public Transform handler;

    public float time = 0.2f;
    public AnimationCurve curve = AnimationCurve.EaseInOut(0,0,1,1);
    
    public void Divide(float handler, float cubes)
    {
        StartCoroutine(DivideRoutine(handler,cubes));
    }
    IEnumerator DivideRoutine(float handler, float cubes)
    {
        yield return null;
    }

    public void Union()
    {
        StartCoroutine(UnionRoutine());
    }
    IEnumerator UnionRoutine()
    {
        var t = 0f;
        var cStart = cubes.localPosition;
        var hStart = handler.localPosition;

        while (t < 1f)
        {
            t += Time.deltaTime /time;
            cubes.localPosition = Vector3.Lerp(cStart, Vector3.zero, curve.Evaluate(t));
            handler.localPosition = Vector3.Lerp(hStart, Vector3.zero, curve.Evaluate(t));
            
            yield return null;
        }
    }
}
