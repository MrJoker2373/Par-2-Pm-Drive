namespace CarGame
{
    using UnityEngine;

    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private CarDeath _death;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<PlayerHealth>(out var player))
            {
                player.Damage();
                _death.Die();
            }
        }
    }
}