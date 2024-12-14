namespace CarGame
{
    using UnityEngine;
    using UnityEngine.Purchasing;
    using Unity.Services.Core;

    public class DonateMenu : MonoBehaviour
    {
        [SerializeField] private string _smallDonateID;
        [SerializeField] private string _mediumDonateID;
        [SerializeField] private string _bigDonateID;
        [SerializeField] private ShopMenu _shop;

        private void Awake()
        {
            UnityServices.InitializeAsync();
        }

        public void OnPurchaseCompleted(Product product)
        {
            var inventory = Inventory.GetInstance();
            if (product.definition.id == _smallDonateID)
                inventory.SetMoney(inventory.GetMoney() + 1000);
            else if (product.definition.id == _mediumDonateID)
                inventory.SetMoney(inventory.GetMoney() + 2000);
            else if (product.definition.id == _bigDonateID)
                inventory.SetMoney(inventory.GetMoney() + 3000);
            _shop.Refresh();
        }
    }
}