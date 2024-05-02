using Core;
using Gameplay.Actors;
using UnityEngine;

namespace Gameplay
{
    public class ControlsManager : MonoBehaviour
    {
        public ControlData ControlDataInstance;
        
        private Player _player;
        
        private void Start()
        {
            ControlDataInstance = new ControlData();
        }

        public void TriggerUpdate()
        {
            GetInputs();
        }

        private void GetInputs()
        {
            ControlDataInstance.Up = Input.GetKeyDown(KeyCode.UpArrow);
            ControlDataInstance.Down = Input.GetKeyDown(KeyCode.DownArrow);
            ControlDataInstance.Left = Input.GetKeyDown(KeyCode.LeftArrow);
            ControlDataInstance.Right = Input.GetKeyDown(KeyCode.RightArrow);
            ControlDataInstance.Action = Input.GetKeyDown(KeyCode.Space);
            ControlDataInstance.Undo = Input.GetKeyDown(KeyCode.Z);
            ControlDataInstance.Pause = Input.GetKeyDown(KeyCode.Escape);
        }

        private void ResetInputs()
        {
            ControlDataInstance.Up = false;
            ControlDataInstance.Down = false;
            ControlDataInstance.Left = false;
            ControlDataInstance.Right = false;
            ControlDataInstance.Action = false;
            ControlDataInstance.Undo = false;
            ControlDataInstance.Pause = false;
        }

        public struct ControlData
        {
            public bool Up;
            public bool Down;
            public bool Left;
            public bool Right;
            public bool Action;
            public bool Undo;
            public bool Pause;
        }
    }
}