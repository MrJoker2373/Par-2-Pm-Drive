namespace CarGame
{
    using UnityEngine;

    public class SafeArea : MonoBehaviour
    {
        [SerializeField] private RectTransform _rect;

        private void Awake()
        {
            var anchorMin = Screen.safeArea.position;
            var anchorMax = Screen.safeArea.position + Screen.safeArea.size;
            anchorMin.x /= Screen.width;
            anchorMax.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.y /= Screen.height;
            _rect.anchorMin = anchorMin;
            _rect.anchorMax = anchorMax;
        }
    }
}