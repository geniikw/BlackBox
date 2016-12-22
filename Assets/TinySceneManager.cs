using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TinySceneManager : MonoBehaviour {

    static TinySceneManager m_ins;
    public static TinySceneManager instance { get { return m_ins; } }

    public List<TinyScene> tinyScenes = new List<TinyScene>();

    TinyScene current;
	// Use this for initialization
	void Start () {
        m_ins = this;
	}

    IEnumerator SwitchSeq(string nextSceneName)
    {
        SmallSceneManager.instance.input = false;

        var scene = tinyScenes.First(s => s.name == nextSceneName);
        current = scene;
        scene.gameObject.SetActive(true);
        yield return scene.EnableSceneInCode();

        SmallSceneManager.instance.input = true;
    }
    public void SetScene(string nextSceneName)
    {
        StartCoroutine(SwitchSeq(nextSceneName));
    }
    public IEnumerator ExitSceneSeq()
    {
        SmallSceneManager.instance.input = false;

        yield return current.DisableSceneInCode();
        current.gameObject.SetActive(false);
        current = null;

        SmallSceneManager.instance.input = true;
    }

    public void ExitScene()
    {
        StartCoroutine(ExitSceneSeq());
    }




}
