namespace CarGame
{
    using UnityEngine;

    public class CarSwitcher : MonoBehaviour
    {
        [SerializeField] private CarMovement _movement;
        [SerializeField] private PlayerHealth _health;
        [SerializeField] private Car _default;
        [SerializeField] private Car _fast;
        [SerializeField] private Car _agile;
        [SerializeField] private Car _protective;

        private void Awake()
        {
            var type = Inventory.GetInstance().GetCurrentCar();
            switch (type)
            {
                case CarType.Default:
                    _movement.SetMoveSpeed(_default.MoveSpeed);
                    _movement.SetRotateSpeed(_default.RotateSpeed);
                    _default.Skin.enabled = true;
                    _health.SetHealth(1);
                    break;
                case CarType.Fast:
                    _movement.SetMoveSpeed(_fast.MoveSpeed);
                    _movement.SetRotateSpeed(_fast.RotateSpeed);
                    _fast.Skin.enabled = true;
                    _health.SetHealth(1);
                    break;
                case CarType.Agile:
                    _movement.SetMoveSpeed(_agile.MoveSpeed);
                    _movement.SetRotateSpeed(_agile.RotateSpeed);
                    _agile.Skin.enabled = true;
                    _health.SetHealth(1);
                    break;
                case CarType.Protective:
                    _movement.SetMoveSpeed(_protective.MoveSpeed);
                    _movement.SetRotateSpeed(_protective.RotateSpeed);
                    _protective.Skin.enabled = true;
                    _health.SetHealth(2);
                    break;
            }
        }
    }
}