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

    [CustomEditor(typeof(TimeBehaviour))]
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

    [CustomEditor(typeof(MapAnimation))]
    public class KMapEditor : TimeEditor { }
    [CustomEditor(typeof(AnimatorWait))]
    public class AnimatorWaitEditor : TimeEditor { }
    [CustomEditor(typeof(TimeActionList))]
    public class TimeActionListEditor : TimeEditor { }
    [CustomEditor(typeof(TransformAction))]
    public class PositionMoveEditor : TimeEditor { }
    [CustomEditor(typeof(CreateMap))]
    public class CreateMapAnimationEditor : TimeEditor { }
    [CustomEditor(typeof(DestroyMap))]
    public class DestroyMapEditor : TimeEditor { }
    [CustomEditor(typeof(MoveToPosition))]
    public class MoveToPositionEditor : TimeEditor { }


#endif





}


