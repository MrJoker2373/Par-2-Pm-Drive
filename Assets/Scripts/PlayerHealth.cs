namespace CarGame
{
    using System.Threading.Tasks;
    using UnityEngine;
    using TMPro;
    using System.Collections;

    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private CarDeath _death;
        [SerializeField] private PlayerInput _input;
        [SerializeField] private InterfaceMenu _interface;
        [SerializeField] private TextMeshProUGUI _healthLabel;
        [SerializeField] private int _winRewardID;
        [SerializeField] private int _winReward10ID;
        [SerializeField] private int _winReward30ID;
        [SerializeField] private int _winReward50ID;
        [SerializeField] private int _loseRewardID;
        private int _health;
        private bool _isShield;
        private bool _isDamage;
        private bool _isDisable;

        public void SetHealth(int health)
        {
            if (health > 0)
            {
                _health = health;
                _healthLabel.text = health.ToString();
            }
        }

        public bool IsShield()
        {
            return _isShield;
        }

        public bool IsDisable()
        {
            return _isDisable;
        }

        public void SetShield()
        {
            _isShield = true;
        }

        public void Win()
        {
            var inventory = Inventory.GetInstance();
            if (inventory.GetWinCount() == 0)
                inventory.CompleteReward(_winRewardID);
            else if (inventory.GetWinCount() == 9)
                inventory.CompleteReward(_winReward10ID);
            else if (inventory.GetWinCount() == 29)
                inventory.CompleteReward(_winReward30ID);
            else if (inventory.GetWinCount() == 49)
                inventory.CompleteReward(_winReward50ID);
            inventory.IncreaseWinCount();
            _interface.Win();
            _input.Disable();
            _isDisable = true;
        }

        public void Damage()
        {
            if (_isShield == true || _isDamage == false)
                StartCoroutine(DamageAsync());
        }

        private IEnumerator DamageAsync()
        {
            if (_isShield == true)
            {
                yield return new WaitForSeconds(1f);
                _isShield = false;
            }
            else if (_health > 0 && _isDamage == false)
            {
                _isDamage = true;
                _health--;
                _healthLabel.text = _health.ToString();
                if (_health <= 0)
                {
                    var inventory = Inventory.GetInstance();
                    if (inventory.GetLoseCount() == 0)
                        inventory.CompleteReward(_loseRewardID);
                    inventory.IncreaseLoseCount();
                    _interface.Lose();
                    _input.Disable();
                    _death.Die();
                    _isDisable = true;
                }
                else
                    yield return new WaitForSeconds(1f);
                _isDamage = false;
            }
        }
    }
}