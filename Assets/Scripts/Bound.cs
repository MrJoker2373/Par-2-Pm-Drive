namespace CarGame
{
    using UnityEngine;

    public class Bound : MonoBehaviour
    {
        [SerializeField] private float _force;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<Ball>() == true)
            {
                collision.rigidbody.velocity += -collision.GetContact(0).normal * _force;
            }
        }
    }
}