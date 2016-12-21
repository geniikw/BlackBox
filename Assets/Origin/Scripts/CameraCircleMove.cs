using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCircleMove : MonoBehaviour {

    public Transform center;
    public float currentAngle = 0;
    public float length = 10f;
    public float time = 0.2f;
    public float height = 10f;
    float initAngle;
    public AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    void Start()
    {
        initAngle = currentAngle;
        length = Vector3.Distance(center.position, transform.position);
    }

    private void FixedUpdate()
    {
        transform.position = center.position + new Vector3(length * Mathf.Cos(currentAngle/ 180f * Mathf.PI), height, length * Mathf.Sin(currentAngle/180f * Mathf.PI));
        transform.LookAt(center);
    }


    public void Right()
    {
        StartCoroutine(AngleTween(currentAngle - 90f, time));
    }
    public void Left()
    {
        StartCoroutine(AngleTween(currentAngle + 90f, time));
    }
    public void Init()
    {
        StartCoroutine(AngleTween(initAngle, time));
    }

    IEnumerator AngleTween(float end , float time)
    {
        SmallSceneManager.instance.input = false;
        var t = 0f;
        var start = currentAngle;
        while (t < 1f)
        {
            t += Time.deltaTime / time;
            currentAngle = Mathf.Lerp(start, end, curve.Evaluate(t));
            yield return null;
        }
        currentAngle = currentAngle % 360;

        SmallSceneManager.instance.input = true;
    }

    public AnimationCurve heightTweenCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    public float heightTweenTime = 0.2f;

    public IEnumerator HeightTween (float end)
    {
        var t = 0f;
        var start = height;
        while (t < 1f)
        {
            t += Time.deltaTime / heightTweenTime;
            height = Mathf.Lerp(start, end, heightTweenCurve.Evaluate(t));
            yield return null;
        }
    }

    public void SetHeight(float target)
    {
        StartCoroutine(HeightTween(target));
    }



    //Coroutine.
    //public void MoveRight()
    //{
    //    StartCoroutine(MoveCircle(3.1415f / 2f, 0.5f));
    //}
    //public void MoveLeft()
    //{
    //    StartCoroutine(MoveCircle(-3.1415f / 2f, 0.5f));
    //}
    //public void MoveZero()
    //{
    //    StartCoroutine(MoveCircle(initAngle/180f*Mathf.PI, 0.5f, true));
    //}

    //private void FixedUpdate()
    //{

    //}
    //IEnumerator MoveCircle(float angle, float time, bool isNotRational= false)
    //{
    //    SmallSceneManager.instance.input = false;
    //    float t = 0;
    //    float s = currentAngle / 180f * Mathf.PI;
    //    float e = isNotRational? angle:(currentAngle / 180f * Mathf.PI + angle )%360;
    //    while(t < 1f)
    //    {
    //        t += Time.deltaTime/time;

    //        transform.position = center.position + new Vector3(length * Mathf.Cos(Mathf.Lerp(s, e, t)), 0, length * Mathf.Sin(Mathf.Lerp(s, e, t)));
    //        transform.LookAt(center);
    //        yield return null;
    //    }
    //    currentAngle = e / Mathf.PI * 180f ;
    //    SmallSceneManager.instance.input = true;
    //}
}
