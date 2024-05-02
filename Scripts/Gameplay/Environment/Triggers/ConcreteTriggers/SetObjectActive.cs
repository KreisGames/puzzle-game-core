using UnityEngine;

namespace Gameplay.Environment.Triggers.ConcreteTriggers
{
    public class SetObjectActive : BaseTrigger
    {
        [SerializeField] private GameObject _targetObject;
        [SerializeField] private bool _targetState;
        
        public override void Trigger()
        {
            _targetObject.SetActive(_targetState);

            if (_targetObject.TryGetComponent(out Tile tile))
            {
                tile.SetState(TileState.Lighted);
            }
        }
    }
}