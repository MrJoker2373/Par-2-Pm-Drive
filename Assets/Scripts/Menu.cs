namespace CarGame
{
    using System.Collections;
    using UnityEngine;

    public class Menu : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _background;
        [SerializeField] private CanvasGroup _group;
        [SerializeField] private float _fadeSpeed;
        private bool _isOpen;

        public bool IsOpen()
        {
            return _isOpen;
        }

        public IEnumerator Open()
        {
            float t = 0f;
            while (t < 1f)
            {
                t = Mathf.MoveTowards(t, 1, _fadeSpeed);
                if (_background != null)
                    _background.alpha = t;
                _group.alpha = t;
                yield return null;
            }
            if (_background != null)
            {
                _background.blocksRaycasts = true;
                _background.interactable = true;
            }
            _group.blocksRaycasts = true;
            _group.interactable = true;
            _isOpen = true;
        }

        public IEnumerator Close()
        {
            _isOpen = false;
            float t = 1f;
            _group.blocksRaycasts = false;
            _group.interactable = false;
            if (_background != null)
            {
                _background.blocksRaycasts = false;
                _background.interactable = false;
            }
            while (t > 0f)
            {
                t = Mathf.MoveTowards(t, 0, _fadeSpeed);
                if (_background != null)
                    _background.alpha = t;
                _group.alpha = t;
                yield return null;
            }
        }
    }
}