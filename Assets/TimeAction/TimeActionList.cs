using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoodNightMypi.TimeAction
{
    public class TimeActionList : TimeBehaviour
    {
        public string memo;
        public List<TimeBehaviour> listAction = new List<TimeBehaviour>();
        public bool isParallel = false;


        public override IEnumerator TimeAction(System.Action callback)
        {
            var count = 0;
            foreach (var action in listAction)
            {
                if (isParallel)
                    action.PlayTimeAction(() => count++);
                else
                    yield return action.PlayTimeAction(() => count++);
            }

            if (isParallel)
                while (count < listAction.Count)
                    yield return null;

            if (callback != null)
                callback();
        }
    }
}
