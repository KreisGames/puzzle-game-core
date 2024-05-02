using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI.Core
{
    public class GUIRoot : MonoBehaviour
    {
        private static bool _initialized;

        private List<UIComponent> _components;
        
        private void Awake()
        {
            if (_initialized)
            {
                Destroy(gameObject);
            }
            
            _initialized = true;
            DontDestroyOnLoad(this);

            _components = GetComponentsInChildren<UIComponent>(true).ToList();
        }

        public T GetUI<T>() where T : UIComponent
        {
            var uiInstance = _components.FirstOrDefault(ui => ui.GetType() == typeof(T));
            if (uiInstance != null)
            {
                return (T)uiInstance;
            }
            Debug.LogError("UI instance not found");
            return null;
        }
    }
}