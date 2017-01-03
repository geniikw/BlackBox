using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MapModeSwitch : MonoBehaviour {

    public Transform cubes;
    public Transform handler;

    public float time = 0.2f;
    public float divideLenght = 5f;
    public AnimationCurve curve = AnimationCurve.EaseInOut(0,0,1,1);
    
    public void Divide()
    {
        StartCoroutine(DivideRoutine());
    }
    IEnumerator DivideRoutine()
    {
        var vector = handler.localPosition;
        vector.Normalize();
        var cStart = cubes.localPosition;
        var hStart = handler.localPosition;

        var cEnd = vector * -divideLenght;
        var hEnd = vector * divideLenght;
        var t = 0f;
        while(t < 1f)
        {
            t += Time.deltaTime / time;

            cubes.localPosition = Vector3.Lerp(cStart, cEnd, curve.Evaluate(t));
            handler.localPosition = Vector3.Lerp(hStart, hEnd, curve.Evaluate(t));

            yield return null;
        }

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

        handler.GetComponentsInChildren<Cube>().ToList().ForEach(c => c.transform.SetParent(cubes));

    }
}
