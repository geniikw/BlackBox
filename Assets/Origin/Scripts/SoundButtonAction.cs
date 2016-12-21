using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoundButtonAction : MonoBehaviour {
    public AudioListener al;
    Image m_image;

    public float waitTime = 4f;

    public bool isActive = false;

    public Sprite on;
    public Sprite off;

    void Awake () {
        m_image = GetComponent<Image>();
    }

    public void ButtonListener()
    {
        var offSprite = Resources.Load<Sprite>("main/sound_off");
        var onSprite = Resources.Load<Sprite>("main/sound_on");

        m_image.sprite = m_image.sprite == offSprite ? onSprite : offSprite;

        if(m_image.sprite == offSprite)
        {
            al.enabled = false;
        }
        else
        {
            al.enabled = true;
        }
    }
}