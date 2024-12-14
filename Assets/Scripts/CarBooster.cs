namespace CarGame
{
    using System.Threading.Tasks;
    using UnityEngine;
    using TMPro;

    public class CarBooster : MonoBehaviour
    {
        [SerializeField] private CarMovement _movement;
        [SerializeField] private CanvasGroup _group;
        [SerializeField] private TextMeshProUGUI _label;
        [SerializeField] private int _duration;
        [SerializeField] private float _multiplier;
        private bool _isBoost;
       
        private void Start()
        {
            var count = Inventory.GetInstance().GetBoosts();
            if (count <= 0)
                _group.alpha = 0.25f;
            else
                _group.alpha = 1f;
            _label.text = count.ToString();
        }

        public async void Boost()
        {
            var count = Inventory.GetInstance().GetBoosts();
            if (count <= 0)
                return;
            if(_isBoost == false)
            {
                _isBoost = true;
                _group.alpha = 0.5f;
                count -= 1;
                Inventory.GetInstance().SetBoosts(count);
                var lastSpeed = _movement.GetMoveSpeed();
                _movement.SetMoveSpeed(lastSpeed * _multiplier);
                await Task.Delay(_duration);
                _movement.SetMoveSpeed(lastSpeed);
                _label.text = count.ToString();
                if(count <= 0)
                    _group.alpha = 0.25f;
                else
                    _group.alpha = 1f;
                _isBoost = false;
            }
        }
    }
}