using UnityEngine;

namespace Core.UndoFeature
{
    public struct UndoRecording
    {
        public bool Success;
        public ActorType ActorType;
        public Vector2 Movement;
        public GameObject Object;
    }
}