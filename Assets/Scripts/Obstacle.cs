namespace CarGame
{
    using UnityEngine;

    public class Obstacle : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<PlayerHealth>(out var health))
            {
                health.Damage();
            }
        }
    }
}