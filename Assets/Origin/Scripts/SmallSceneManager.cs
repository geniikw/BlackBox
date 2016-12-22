using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;

#if UNITY_EDITOR
using UnityEditor;
#endif 

public class SmallSceneManager : MonoBehaviour {
    
    public static SmallSceneManager instance { 
        get
        {
            return m_instance;
        }
    }

    public static SmallSceneManager m_instance = null;
    public EventSystem es;
    public bool input {
        set
        {
            if (es == null)
                return;
            es.enabled = value;
        }
        get
        {
            return es.enabled;
        }
    }

    public List<SmallScene> scenes = new List<SmallScene>();
	public bool isDebug = false;
    public int debugStartScene;
    
    public SmallScene current = null;

    // Use this for initialization
	void Awake() {
        m_instance = this;
	}
    void Start()
    {
        var firstScene = isDebug ? scenes[debugStartScene] : scenes.First(scene => scene.isFirst == true);

        scenes.ForEach(scene => scene.SetActiveScene(false));
        SwitchScene(firstScene);
    }
    
    public void SwitchSceneInInspector(SmallScene next)
    {
        StartCoroutine(SwitchSequence(next));
    }

    public static void SwitchScene(SmallScene next)
    {
        instance.StartCoroutine(instance.SwitchSequence(next));
    }
    public static void SwitchScene(int nextSceneIndex)
    {
        SwitchScene(instance.scenes[nextSceneIndex]);
    }

    IEnumerator SwitchSequence(SmallScene next)
    {
        input = false;
        if(current != null)
        {
            yield return current.DisableSequence();
            current.SetActiveScene(false);
        }

        next.SetActiveScene(true);
        current = next;
        yield return next.EnableSequence();
        input = true;
    }

}

#if UNITY_EDITOR
[CustomEditor(typeof(SmallSceneManager))]
public class SceneManagerEditor : Editor
{

    string[] labels;
    private void OnEnable()
    {
        var owner = target as SmallSceneManager;
        labels = new string[owner.scenes.Count];
        int n = 0;
        foreach (var scene in owner.scenes)
        {
            labels[n++] = scene.name;
        }
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        var owner = target as SmallSceneManager;

        if (Application.isPlaying)
        {
            foreach (var scene in owner.scenes)
            {
                if (GUILayout.Button("scene move to : "+ scene.name))
                {
                    SmallSceneManager.SwitchScene(owner.scenes.IndexOf(scene));
                }
            }
        }
        else if(owner.isDebug)
        {
            owner.debugStartScene = GUILayout.SelectionGrid(owner.debugStartScene,labels,1);
        }
    }


}
#endif 