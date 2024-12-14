namespace CarGame
{
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using System.Collections;

    public class ShopMenu : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _titleLabel;
        [SerializeField] private TextMeshProUGUI _descriptionLabel;
        [SerializeField] private TextMeshProUGUI _priceLabel;
        [SerializeField] private Image _icon;
        [SerializeField] private Button _equipButton;
        [SerializeField] private TextMeshProUGUI _equipButtonLabel;
        [SerializeField] private Button _buyButton;
        [SerializeField] private TextMeshProUGUI _buyButtonLabel;
        [SerializeField] private TextMeshProUGUI _moneyLabel;
        [SerializeField] private TextMeshProUGUI _boostsLabel;
        [SerializeField] private TextMeshProUGUI _punchesLabel;
        [SerializeField] private TextMeshProUGUI _shieldsLabel;
        [SerializeField] private Menu _donateMenu;
        [SerializeField] private Menu _shopMenu;
        [SerializeField] private int _purchase1RewardID;
        [SerializeField] private int _purchase3RewardID;
        [SerializeField] private Item[] _items;
        private int _currentItem;

        private void Start()
        {
            _currentItem = 0;
            Refresh();
        }

        public void OpenDonate()
        {
            StartCoroutine(OpenDonateAsync());
        }

        public void CloseDonate()
        {
            StartCoroutine(CloseDonateAsync());
        }

        private IEnumerator OpenDonateAsync()
        {
            yield return _shopMenu.Close();
            yield return _donateMenu.Open();
        }

        private IEnumerator CloseDonateAsync()
        {
            yield return _donateMenu.Close();
            yield return _shopMenu.Open();
        }

        public void Backward()
        {
            if (_currentItem == 0)
                _currentItem = _items.Length - 1;
            else
                _currentItem--;
            Refresh();
        }

        public void Forward()
        {
            if (_currentItem == _items.Length - 1)
                _currentItem = 0;
            else
                _currentItem++;
            Refresh();
        }

        public void Buy()
        {
            var inventory = Inventory.GetInstance();
            var item = _items[_currentItem];
            inventory.SetMoney(inventory.GetMoney() - item.Price);
            switch (item.Type)
            {
                case Item.ItemType.Boost:
                    inventory.SetBoosts(inventory.GetBoosts() + 1);
                    break;
                case Item.ItemType.Punch:
                    inventory.SetPunches(inventory.GetPunches() + 1);
                    break;
                case Item.ItemType.Shield:
                    inventory.SetShields(inventory.GetShields() + 1);
                    break;
                case Item.ItemType.Car:
                    if (inventory.GetCarCount() == 1)
                        inventory.CompleteReward(_purchase1RewardID);
                    else if (inventory.GetCarCount() == 3)
                        inventory.CompleteReward(_purchase3RewardID);
                    inventory.PurchaseCar(item.CarType);
                    break;
            }
            Refresh();
        }

        public void Equip()
        {
            var inventory = Inventory.GetInstance();
            var item = _items[_currentItem];
            inventory.SetCar(item.CarType);
            Refresh();
        }

        public void Refresh()
        {
            var inventory = Inventory.GetInstance();
            var item = _items[_currentItem];
            _titleLabel.text = item.Title;
            _descriptionLabel.text = item.Description;
            _priceLabel.text = item.Price.ToString();
            _icon.sprite = item.Sprite;

            _moneyLabel.text = inventory.GetMoney().ToString();
            _boostsLabel.text = inventory.GetBoosts().ToString();
            _punchesLabel.text = inventory.GetPunches().ToString();
            _shieldsLabel.text = inventory.GetShields().ToString();

            _equipButton.interactable = true;
            _equipButtonLabel.text = "Equip";
            _buyButton.interactable = true;
            _buyButtonLabel.text = "Buy";

            if (item.Type == Item.ItemType.Car)
            {
                if (inventory.IsPurchasedCar(item.CarType))
                {
                    _buyButton.interactable = false;
                    _buyButtonLabel.text = "Purchased";
                    if (inventory.GetCurrentCar() == item.CarType)
                    {
                        _equipButton.interactable = false;
                        _equipButtonLabel.text = "Equiped";
                    }
                }
                else
                {
                    _equipButton.interactable = false;
                    _equipButtonLabel.text = "Can not equip";
                }
            }
            else
            {
                _equipButton.interactable = false;
                _equipButtonLabel.text = "Can not equip";
            }
            if (item.Price > inventory.GetMoney())
            {
                _buyButton.interactable = false;
                _buyButtonLabel.text = "Not enugh money";
            }
        }
    }
}