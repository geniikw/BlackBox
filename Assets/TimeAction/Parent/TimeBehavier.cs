using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GoodNightMypi.TimeAction
{
    public abstract class TimeBehaviour : MonoBehaviour
    {
        public UnityEvent OnStart;
        public UnityEvent OnEnd;
        private IEnumerator TimeActionWrapper(System.Action callback=null)
        {
            OnStart.Invoke();
            yield return StartCoroutine(TimeAction(callback));
            OnEnd.Invoke();
        }

        public abstract IEnumerator TimeAction(System.Action callback = null);

        public virtual Coroutine PlayTimeAction(System.Action callback = null)
        {
            return StartCoroutine(TimeActionWrapper(callback));
        }
        public void Play()
        {
            StartCoroutine(TimeActionWrapper());
        }
    }

#if UNITY_EDITOR

    [CustomEditor(typeof(TimeBehaviour),true)]
    public class TimeEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("TimeAction"))
            {
                (target as TimeBehaviour).PlayTimeAction();
            }
        }
    }

#endif





}


