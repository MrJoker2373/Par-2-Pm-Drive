namespace CarGame
{
    using UnityEngine;
    using UnityEngine.InputSystem;

    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private InputAction _move;
        [SerializeField] private CarMovement _movement;

        private void Awake()
        {
            Enable();
        }

        public void Enable()
        {
            _move.Enable();
            _move.performed += OnMove;
        }

        public void Disable()
        {
            _move.Disable();
            _move.performed -= OnMove;
            _movement.SetMovement(0);
            _movement.SetRotation(0);
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            var input = context.ReadValue<Vector2>();
            float y = Vector2.Dot(input, Vector2.up);
            float x = Vector2.Dot(input, Vector2.right);

            _movement.SetMovement(y);
            _movement.SetRotation(x);
        }
    }
}