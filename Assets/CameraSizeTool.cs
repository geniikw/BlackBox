using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSizeTool : MonoBehaviour {
    public Transform backGround;//같이 커져야함.
    public float adjust = 5f;
    Camera m_cam;
    public AnimationCurve curve = AnimationCurve.EaseInOut(0,0,1,1);
    
    private void Start()
    {
        m_cam = GetComponent<Camera>();
    }

    public float size
    {
        set
        {
            m_cam.orthographicSize = value;

        }
        get
        {
            return m_cam.orthographicSize;
        }
    }


    public Coroutine SetCamSize(float end, float time)
    {
        return StartCoroutine(SetCamRoutine(end, time));
    }

    IEnumerator SetCamRoutine(float end, float time)
    {
        float t = 0f;
        var start = size;

        while (t < 1f) {
            t += Time.deltaTime/time;
            size = Mathf.Lerp(start, end, curve.Evaluate(t));
            backGround.localScale = Vector3.one * size / 5f;
            yield return null;
        }

    }
}
