using UnityEngine;

namespace Core.UndoFeature
{
    public static class UndoExtensions
    {
        public static void Record(this ref UndoRecording undoRecording, ActorType actorType, Vector2 direction, GameObject obj = null)
        {
            undoRecording.Success = true;
            undoRecording.ActorType = actorType;
            undoRecording.Movement = direction;
            undoRecording.Object = obj;
        }
    }
}