using System;
using Core;
using UnityEngine;

namespace Gameplay.Environment.Triggers
{
    public class BaseTrigger : MonoBehaviour
    {
        protected GameManager GameManager;

        private void Awake()
        {
            GameManager ??= FindObjectOfType<GameManager>();
        }

        public virtual void Trigger() { }

        public virtual void DisableTrigger() { }
    }
}