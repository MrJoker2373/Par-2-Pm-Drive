namespace CarGame
{
    using UnityEngine;
    using TMPro;

    public class CarPuncher : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _group;
        [SerializeField] private TextMeshProUGUI _label;
        [SerializeField] private float _force;
        private bool _isPunch;

        private void Start()
        {
            var count = Inventory.GetInstance().GetPunches();
            if (count <= 0)
                _group.alpha = 0.25f;
            else
                _group.alpha = 1f;
            _label.text = count.ToString();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_isPunch == true)
            {
                if (collision.gameObject.GetComponent<Ball>() == true)
                {
                    var count = Inventory.GetInstance().GetPunches();
                    _isPunch = false;
                    if (count <= 0)
                        _group.alpha = 0.25f;
                    else
                        _group.alpha = 1f;
                    collision.rigidbody.velocity += -collision.GetContact(0).normal * _force;
                }
            }
        }

        public void Punch()
        {
            var count = Inventory.GetInstance().GetPunches();
            if (count <= 0)
                return;
            if (_isPunch == false)
            {
                _isPunch = true;
                count -= 1;
                Inventory.GetInstance().SetPunches(count);
                _label.text = count.ToString();
                _group.alpha = 0.5f;
            }
        }
    }
}