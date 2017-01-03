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
        if (!scene.IsInitialize)
            throw new System.Exception("초기화가 필요한 Scene입니다. scene.name : " + scene.name);

        current = scene;
        scene.gameObject.SetActive(true);
        yield return scene.EnableSceneInCode();

        SmallSceneManager.instance.input = true;
    }
    public void SetScene(string nextSceneName)
    {
        if (current != null)
            throw new System.Exception("TinyScene은 동시에 1개만 열수 있습니다. current.name : " + current.name);
        
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
