using UnityEngine;

namespace Gameplay.Environment.Triggers.ConcreteTriggers
{
    public class TriggerAnotherTile : BaseTrigger
    {
        [SerializeField] private CompositeTrigger _trigger;
        public override void Trigger()
        {
            _trigger.Trigger();
        }

        public override void DisableTrigger()
        {
            _trigger.DisableTrigger();
        }
    }
}