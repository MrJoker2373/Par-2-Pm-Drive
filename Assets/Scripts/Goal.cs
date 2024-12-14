namespace CarGame
{
    using UnityEngine;

    public class Goal : MonoBehaviour
    {
        [SerializeField] private PlayerHealth _health;
        private bool _isWin;

        private void OnTriggerEnter(Collider other)
        {
            if (_isWin == true)
                return;
            if (other.GetComponent<Ball>() == true)
            {
                _isWin = true;
                _health.Win();
            }
        }
    }
}