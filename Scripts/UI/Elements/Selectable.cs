using UnityEngine;

namespace UI.Elements
{
    public class Selectable : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Sprite _selectedSprite;
        [SerializeField] private Sprite _deselectedSprite;
        
        public void SetSelected(bool setTo)
        {
            if (setTo)
            {
                _renderer.sprite = _selectedSprite;
            }
            else
            {
                _renderer.sprite = _deselectedSprite;
            }
        }
    }
}