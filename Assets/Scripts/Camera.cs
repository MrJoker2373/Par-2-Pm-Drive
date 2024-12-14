namespace CarGame
{
    using UnityEngine;

    public class Camera : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _speed;

        private void LateUpdate()
        {
            if (_target == null)
                return;
            transform.position = Vector3.Lerp(transform.position, _target.position, _speed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, _target.rotation, _speed * Time.deltaTime);
        }
    }
}