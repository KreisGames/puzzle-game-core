using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.Environment
{
    public class TileTags : MonoBehaviour
    {
        [FormerlySerializedAs("pushable")] public bool _pushable;
        [FormerlySerializedAs("stop")] public bool _stop;
        [FormerlySerializedAs("interactable")] public bool _interactable;
        [FormerlySerializedAs("floor")] public bool _floor;
        [FormerlySerializedAs("_activating")] public bool _trigger;
        public bool _pressurable;
    }

    public enum Tags
    {
        Pushable,
        Stop,
        Interactable,
        Floor,
        Trigger,
        Pressurable,
    }
}