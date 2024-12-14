namespace CarGame
{
    using UnityEngine;

    public class MovingObstacle : MonoBehaviour
    {
        [SerializeField] private float _offset;
        [SerializeField] private float _speed;
        private Vector3 _positionLeft;
        private Vector3 _positionRight;
        private bool _isRight;

        private void Awake()
        {
            _positionLeft = transform.position - Vector3.right * _offset;
            _positionRight = transform.position + Vector3.right * _offset;
        }

        private void Update()
        {
            if (_isRight == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, _positionRight, _speed);
                if (transform.position == _positionRight)
                    _isRight = false;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, _positionLeft, _speed);
                if (transform.position == _positionLeft)
                    _isRight = true;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<PlayerHealth>(out var health))
            {
                health.Damage();
            }
        }
    }
}