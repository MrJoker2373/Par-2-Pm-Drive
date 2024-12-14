namespace CarGame
{
    using UnityEngine;
    using UnityEngine.AI;

    public class MachineInput : MonoBehaviour
    {
        [SerializeField] private PlayerHealth _target;
        [SerializeField] private CarDeath _death;
        [SerializeField] private CarMovement _movement;

        private void Update()
        {
            if (_target == null || _target.IsDisable() == true || _death.IsDead() == true)
            {
                _movement.SetMovement(0);
                _movement.SetRotation(0);
                return;
            }

            if (FindPath(out var direction))
            {
                var moveAmount = Vector3.Dot(_movement.transform.forward, direction);
                _movement.SetMovement(moveAmount);

                var rotateAmount = Vector3.Dot(_movement.transform.right, direction);
                _movement.SetRotation(rotateAmount);
            }
        }

        public void SetTarget(PlayerHealth target)
        {
            _target = target;
        }

        private float GetDistance()
        {
            return Vector3.Distance(_movement.transform.position, _target.transform.position);
        }

        private bool FindPath(out Vector3 direction)
        {
            var layer = NavMesh.AllAreas;
            var path = new NavMeshPath();
            NavMesh.CalculatePath(_movement.transform.position, _target.transform.position, layer, path);
            bool hasPath = path.corners.Length > 0;
            direction = hasPath ? (path.corners[1] - path.corners[0]).normalized : default;
            return hasPath;
        }
    }
}