namespace CarGame
{
    using System.Collections;
    using UnityEngine;
    using TMPro;

    public class CarShielder : MonoBehaviour
    {
        [SerializeField] private PlayerHealth _health;
        [SerializeField] private CanvasGroup _group;
        [SerializeField] private TextMeshProUGUI _label;
        private bool _isShield;

        private void Start()
        {
            var count = Inventory.GetInstance().GetShields();
            if (count <= 0)
                _group.alpha = 0.25f;
            else
                _group.alpha = 1f;
            _label.text = count.ToString();
        }

        public void Shield()
        {
            if (_isShield == false)
                StartCoroutine(ShieldAsync());
        }

        private IEnumerator ShieldAsync()
        {
            var count = Inventory.GetInstance().GetShields();
            if (count > 0)
            {
                count -= 1;
                Inventory.GetInstance().SetShields(count);
                _isShield = true;
                _group.alpha = 0.5f;
                _label.text = count.ToString();
                _health.SetShield();
                while (_health.IsShield() == true)
                    yield return null;
                if (count <= 0)
                    _group.alpha = 0.25f;
                else
                    _group.alpha = 1f;
                _isShield = false;
            }
        }
    }
}