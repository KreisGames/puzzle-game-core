using UnityEngine;

namespace Gameplay.Environment.Triggers.ConcreteTriggers
{
    public class ChangeState : BaseTrigger
    {
        [SerializeField] private MultiStateTile _multiStateTile;
        [SerializeField] private bool _invert;
        public override void Trigger()
        {
            _multiStateTile.SetActivationState(!_invert);
        }

        public override void DisableTrigger()
        {
            _multiStateTile.SetActivationState(_invert);
        }
    }
}