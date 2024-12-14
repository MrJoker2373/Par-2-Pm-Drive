namespace CarGame
{
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;

    public class RewardMenu : MonoBehaviour
    {
        [SerializeField] private Reward[] _rewards;
        [SerializeField] private Button _takeButton;
        [SerializeField] private TextMeshProUGUI _titleLabel;
        [SerializeField] private TextMeshProUGUI _priceLabel;
        [SerializeField] private TextMeshProUGUI _moneyLabel;
        private int _currentReward;

        public void Forward()
        {
            if(_currentReward == _rewards.Length - 1)
                _currentReward = 0;
            else
                _currentReward++;
            Refresh();
        }

        public void Backward()
        {
            if (_currentReward == 0)
                _currentReward = _rewards.Length - 1;
            else
                _currentReward--;
            Refresh();
        }

        public void TakeReward()
        {
            var inventory = Inventory.GetInstance();
            var reward = _rewards[_currentReward];
            inventory.TakeReward(reward.ID);
            inventory.SetMoney(inventory.GetMoney() + reward.Price);
            Refresh();
        }

        public void Refresh()
        {
            var inventory = Inventory.GetInstance();
            var reward = _rewards[_currentReward];
            _titleLabel.text = reward.Title;
            _priceLabel.text = reward.Price.ToString();
            _moneyLabel.text = inventory.GetMoney().ToString();
            if (inventory.IsRewardCompleted(reward.ID) == true && inventory.IsRewardTaken(reward.ID) == false)
                _takeButton.interactable = true;
            else
                _takeButton.interactable = false;
        }
    }
}