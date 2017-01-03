using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GoodNightMypi.StringLab;

public class MessageBox : TinyScene {

    private bool m_isInit = false;
    public static MessageBox instance;
    
    public override bool IsInitialize
    {
        get
        {
            if (!m_isInit)
            {
                Debug.LogWarning("use MessageBox.Open() instance TinySceneManager.SetScene()");
            }

            return m_isInit;
        }
    }

    public gString test;

    UnityEvent onYes;
   
    public void Initialize(string title, string content, UnityAction onYes)
    {
        this.onYes.AddListener(onYes);
    }

    public static void Open(string title, string content, UnityAction onYes)
    {
        instance.Initialize(title, content, onYes);
        TinySceneManager.instance.SetScene("MessageBox");
    }

    // Use this for initialization
    void Awake () {
        if (onYes == null)
            onYes = new UnityEvent();
        instance = this;
    }
}
