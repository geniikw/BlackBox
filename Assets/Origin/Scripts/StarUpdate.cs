using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[RequireComponent(typeof(SpriteRenderer))]
public class StarUpdate : MonoBehaviour {

    SpriteRenderer m_sr;
    public SpriteRenderer sr { get { return m_sr ?? (m_sr = GetComponent<SpriteRenderer>()); } }

    public float twinkleTime = 1f;

    public Sprite star1;
    public Sprite star2;
    
    void OnEnable()
    {
        StartCoroutine(Loop());
    }

    // Use this for initialization
    IEnumerator Loop () {
        while (true)
        {
            yield return StartCoroutine(AlphaTween(0f, 1f, twinkleTime));
            yield return StartCoroutine(AlphaTween(1f, 0f, twinkleTime));
            sr.sprite = sr.sprite == star1 ? star2 : star1;
            yield return null;
        }
         
	}

    IEnumerator AlphaTween(float start, float end, float time)
    {
        var c = sr.color;
        var sColor = new Color(c.r, c.b, c.g, start);
        var eColor = new Color(c.r, c.b, c.g, end);

        float t = 0;
        while(t < 1)
        {
            t += Time.deltaTime / time;
            sr.color = Color.Lerp(sColor, eColor, t);
            yield return null;
        }
    }

}
