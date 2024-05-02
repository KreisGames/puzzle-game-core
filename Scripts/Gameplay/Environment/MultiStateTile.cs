using System;
using UnityEngine;

namespace Gameplay.Environment
{
    public class MultiStateTile : Tile
    {
        [SerializeField] private Sprite _activatedSpriteL;
        [SerializeField] private Sprite _activatedSpriteD;
        [SerializeField] private bool _forceUpdate;

        [SerializeField] private bool ActivatedState;
        
        public override void SetState(TileState newState)
        {
            _state = newState;
            switch (_state)
            {
                case TileState.NotDiscovered:
                    _tileImage.sprite = _notDiscoveredSprite;
                    break;
                case TileState.Lighted:
                    _tileImage.sprite = ActivatedState ? _activatedSpriteL : _lightedSprite;
                    break;
                case TileState.Dark:
                    _tileImage.sprite = ActivatedState ? _activatedSpriteD : _memorizedSprite;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void SetActivationState(bool activeState)
        {
            ActivatedState = activeState;
            if (_forceUpdate || _state == TileState.Lighted)
            {
                SetState(_state);
            }
        }
    }
}