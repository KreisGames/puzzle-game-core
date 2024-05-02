using System.Collections.Generic;

namespace Core.UndoFeature
{
    public static class UndoManager
    {
        private static Stack<UndoRecording> _recordings = new();
        
        public static void AddRecording(UndoRecording newRecording)
        {
            _recordings.Push(newRecording);
        }

        public static (bool, UndoRecording) GetLastUndo()
        {
            var haveRecording = _recordings.TryPeek(out var lastRecording);
            if (haveRecording)
            {
                _recordings.Pop();
            }
            
            return (haveRecording, lastRecording);
        }

        public static void ResetRecordings()
        {
            _recordings.Clear();
        }
    }
}