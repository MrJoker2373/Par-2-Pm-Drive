namespace CarGame
{
    using System.Threading.Tasks;
    using UnityEngine;

    public class CarDeath : MonoBehaviour
    {
        [SerializeField] private GameObject _root;
        [SerializeField] private GameObject _model;
        [SerializeField] private Collider _collider;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private ParticleSystem _particle;
        [SerializeField] private AudioSource _audio;
        [SerializeField] private CarMovement _movement;
        private bool _isDead;

        public bool IsDead()
        {
            return _isDead;
        }

        public async void Die()
        {
            if (_isDead == false)
            {
                _isDead = true;
                _particle.Play();
                _audio.Play();
                _model.SetActive(false);
                _collider.enabled = false;
                _rigidbody.isKinematic = true;
                _movement.Disable();
                while (_particle != null && _particle.IsAlive() == true)
                    await Task.Yield();
                if (_particle != null)
                    Destroy(_root);
            }
        }
    }
}