namespace CarGame
{
    using UnityEngine;

    public class CarMovement : MonoBehaviour
    {
        [SerializeField] private GroundTracker _ground;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotateSpeed;
        [SerializeField] private AudioSource _engine;
        private float _moveDirection;
        private float _rotateDirection;

        private void FixedUpdate()
        {
            if (_ground.IsGrounded() == false)
                return;

            if (_moveDirection != 0)
            {
                _rigidbody.velocity += transform.forward * _moveDirection * _moveSpeed * Time.deltaTime;
                if (_rotateDirection != 0)
                {
                    var direction = transform.right * _moveDirection * _rotateDirection;
                    var rotation = Vector3.Lerp(transform.forward, direction, _rotateSpeed);
                    _rigidbody.rotation = Quaternion.LookRotation(rotation);
                }
            }
            _engine.pitch = Mathf.Clamp(_rigidbody.velocity.magnitude / 7f, 1, float.MaxValue);
        }

        public float GetMoveSpeed()
        {
            return _moveSpeed;
        }

        public void SetMoveSpeed(float speed)
        {
            if (speed > 0f)
                _moveSpeed = speed;
        }

        public float GetRotateSpeed()
        {
            return _rotateSpeed;
        }

        public void SetRotateSpeed(float speed)
        {
            if (speed > 0f)
                _rotateSpeed = speed;
        }

        public void SetMovement(float movement)
        {
            _moveDirection = movement;
        }

        public void SetRotation(float rotation)
        {
            _rotateDirection = rotation;
        }

        public void Disable()
        {
            _engine.volume = 0;
            _moveDirection = 0;
            _rotateDirection = 0;
        }
    }
}