namespace CarGame
{
    using System.Collections.Generic;
    using UnityEngine;

    public class GameController : MonoBehaviour
    {
        [SerializeField] private int _playRewardID;
        [SerializeField] private PlayerHealth _player;
        [SerializeField] private Obstacle _barrel;
        [SerializeField] private MovingObstacle _fence;
        [SerializeField] private MachineInput _enemy;
        [SerializeField] private int _maxPointsX;
        [SerializeField] private int _maxPointsZ;
        [SerializeField] private int _maxItems;
        [SerializeField] private Transform _pointMin;
        [SerializeField] private Transform _pointMax;

        private void Awake()
        {
            var inventory = Inventory.GetInstance();
            if (inventory.GetPlayCount() == 0)
                inventory.CompleteReward(_playRewardID);
            inventory.IncreasePlayCount();

            GenerateMap(GetPoints());
        }

        private Vector3[,] GetPoints()
        {
            Vector3[,] points = new Vector3[_maxPointsX, _maxPointsZ];

            float lengthX = _pointMax.position.x - _pointMin.position.x;
            float lengthZ = _pointMax.position.z - _pointMin.position.z;

            float offsetX = lengthX / (points.GetLength(0) + 1);
            float offsetZ = lengthZ / (points.GetLength(1) + 1);

            for (int x = 0; x < points.GetLength(0); x++)
            {
                for (int z = 0; z < points.GetLength(1); z++)
                {
                    points[x, z] = new Vector3(_pointMin.position.x + offsetX * (x + 1), _pointMin.position.y, _pointMin.position.z + offsetZ * (z + 1));
                }
            }

            return points;
        }

        private void GenerateMap(Vector3[,] points)
        {
            int cars = 0;
            List<Vector3> currentPositions = new();
            for (int i = 0; i < _maxItems;)
            {
                int randomX = Random.Range(0, points.GetLength(0));
                int randomZ = Random.Range(0, points.GetLength(1));
                var position = new Vector3(points[randomX, randomZ].x, points[randomX, randomZ].y, points[randomX, randomZ].z);
                if (currentPositions.Contains(position))
                    continue;
                currentPositions.Add(position);
                int obstacleChance = 0;
                if (cars >= 2)
                    obstacleChance = Random.Range(0, 2);
                else
                    obstacleChance = Random.Range(0, 3);
                if (obstacleChance == 0)
                    Instantiate(_barrel, position, Quaternion.identity);
                else if (obstacleChance == 1)
                    Instantiate(_fence, position, Quaternion.identity);
                else
                {
                    var enemy = Instantiate(_enemy, position, Quaternion.identity);
                    enemy.SetTarget(_player);
                    cars++;
                }
                i++;
            }
        }
    }
}