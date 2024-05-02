using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.Environment
{
    public class Tile : MonoBehaviour
    {
        protected TileState _state;
        
        [FormerlySerializedAs("defaultState")] [SerializeField] protected TileState _defaultState;

        [FormerlySerializedAs("tileImage")] [SerializeField] protected SpriteRenderer _tileImage;
        [FormerlySerializedAs("notDiscoveredSprite")]
        [Header("States")]
        [SerializeField] protected Sprite _notDiscoveredSprite;
        [FormerlySerializedAs("lightedSprite")] [SerializeField] protected Sprite _lightedSprite;
        [FormerlySerializedAs("memorizedSprite")] [SerializeField] protected Sprite _memorizedSprite;

        [FormerlySerializedAs("tileTags")]
        [Header("Logic")] 
        public TileTags _tileTags;

        private void Awake()
        {
            SetState(_defaultState);
        }
        
        public TileState GetState()
        {
            return _state;
        }

        public virtual void SetState(TileState newState)
        {
            _state = newState;
            switch (_state)
            {
                case TileState.NotDiscovered:
                    _tileImage.sprite = _notDiscoveredSprite;
                    break;
                case TileState.Lighted:
                    _tileImage.sprite = _lightedSprite;
                    break;
                case TileState.Dark:
                    _tileImage.sprite = _memorizedSprite;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}