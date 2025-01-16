using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

namespace Units
{
    public class MoveEngine : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _reachTargetDistance = 0.2f;
        [SerializeField] private LineRenderer _debugPathLine;

        private Transform _transformCached;
        private float _reachTargetDistanceSqr;
        private Vector3 _destinationPoint;
        private Vector3[] _pathPoints;
        private Vector3 _nextPoint;
        private int _nextPointIndex;

        public bool IsMoving => _nextPointIndex >= 0;

        [UsedImplicitly]
        private void Awake()
        {
            _transformCached = transform;
            _reachTargetDistanceSqr = _reachTargetDistance * _reachTargetDistance;
            _nextPointIndex = -1;
        }

        [UsedImplicitly]
        private void OnValidate() => _reachTargetDistanceSqr = _reachTargetDistance * _reachTargetDistance;

        [UsedImplicitly]
        private void Update()
        {
            MoveToNextPoint();
            DrawDebugPathLine();
        }

        public void MoveTo(Vector3 destinationPoint)
        {
            if (destinationPoint == _destinationPoint)
                return;

            _destinationPoint = destinationPoint;
            StartMovement();
        }

        private void StartMovement()
        {
            if (!TryCalculatePath(out Vector3[] pathPoints))
            {
                Debug.LogError($"Failed to calculate path to point:{_destinationPoint}");
                return;
            }

            _pathPoints = pathPoints;
            _nextPointIndex = -1;

            if (!TrySetNextPoint())
            {
                Debug.LogError($"Path has zero points");
            }
        }

        private void MoveToNextPoint()
        {
            if (_nextPointIndex < 0)
                return;

            if (IsOnPoint(_nextPoint) && !TrySetNextPoint())
                StopMovement();

            Vector3 currentPosition = _transformCached.position;
            Vector3 direction = (_nextPoint - currentPosition).normalized;
            Vector3 nextPosition = currentPosition + direction * (_speed * Time.deltaTime);
            _transformCached.LookAt(nextPosition);
            _transformCached.position = nextPosition;
        }

        private bool TrySetNextPoint()
        {
            ++_nextPointIndex;

            if (_nextPointIndex == _pathPoints.Length)
                return false;

            _nextPoint = _pathPoints[_nextPointIndex];
            return true;
        }

        private void StopMovement()
        {
            _nextPointIndex = -1;
            _pathPoints = null;
            _nextPoint = default;
            _destinationPoint = default;
        }

        private bool IsOnPoint(Vector3 point) => (point - _transformCached.position).sqrMagnitude < _reachTargetDistanceSqr;

        private bool TryCalculatePath(out Vector3[] pathPoints)
        {
            NavMeshPath path = new();

            if (NavMesh.CalculatePath(_transformCached.position, _destinationPoint, NavMesh.AllAreas, path))
            {
                pathPoints = path.corners;
                return true;
            }

            pathPoints = null;
            return false;
        }

        private void DrawDebugPathLine()
        {
            if (_nextPointIndex < 0)
                return;

            _debugPathLine.positionCount = _pathPoints.Length;

            for (int i = 0; i < _pathPoints.Length; i++)
                _debugPathLine.SetPosition(i, _pathPoints[i]);
        }
    }
}