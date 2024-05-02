using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Environment.Triggers
{
    public class CompositeTrigger : MonoBehaviour
    {
        public List<BaseTrigger> _triggers;

        public void Trigger()
        {
            foreach (var trigger in _triggers)
            {
                trigger.Trigger();
            }
        }

        public void DisableTrigger()
        {
            foreach (var trigger in _triggers)
            {
                trigger.DisableTrigger();
            }
        }
    }
}