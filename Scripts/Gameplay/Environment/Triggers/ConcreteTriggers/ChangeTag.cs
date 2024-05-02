using System;
using UnityEngine;

namespace Gameplay.Environment.Triggers.ConcreteTriggers
{
    public class ChangeTag : BaseTrigger
    {
        [SerializeField] private TileTags _tileTags;
        [SerializeField] private Tags _tag;
        [SerializeField] private bool _changeTo;
        
        public override void Trigger()
        {
            switch (_tag)
            {
                case Tags.Pushable:
                    _tileTags._pushable = _changeTo;
                    break;
                case Tags.Stop:
                    _tileTags._stop = _changeTo;
                    break;
                case Tags.Interactable:
                    _tileTags._interactable = _changeTo;
                    break;
                case Tags.Floor:
                    _tileTags._floor = _changeTo;
                    break;
                case Tags.Trigger:
                    _tileTags._trigger = _changeTo;
                    break;
                case Tags.Pressurable:
                    _tileTags._pressurable = _changeTo;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        public override void DisableTrigger()
        {
            switch (_tag)
            {
                case Tags.Pushable:
                    _tileTags._pushable = !_changeTo;
                    break;
                case Tags.Stop:
                    _tileTags._stop = !_changeTo;
                    break;
                case Tags.Interactable:
                    _tileTags._interactable = !_changeTo;
                    break;
                case Tags.Floor:
                    _tileTags._floor = !_changeTo;
                    break;
                case Tags.Trigger:
                    _tileTags._trigger = !_changeTo;
                    break;
                case Tags.Pressurable:
                    _tileTags._pressurable = !_changeTo;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}