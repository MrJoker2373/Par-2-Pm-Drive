namespace CarGame
{
    using UnityEngine;

    public class GroundTracker : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _groundDrag;
        [SerializeField] private float _airDrag;
        private Collision _ground;

        private void OnCollisionStay(Collision collision)
        {
            if (_ground == null)
            {
                var dot = Vector3.Dot(collision.GetContact(0).normal, Vector3.up);
                if (dot > 0.45f)
                {
                    _ground = collision;
                    _rigidbody.drag = _groundDrag;
                }
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (_ground == collision)
            {
                _ground = null;
                _rigidbody.drag = _airDrag;
            }
        }

        public bool IsGrounded()
        {
            return _ground != null;
        }
    }
}