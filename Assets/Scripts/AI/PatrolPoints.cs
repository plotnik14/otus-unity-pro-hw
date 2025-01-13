using System;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class PatrolPoints : MonoBehaviour
    {
        [SerializeField] private List<Transform> _points;

        private int _currentPointIndex = 0;

        public Vector3 CurrentPoint => _points[_currentPointIndex].position;

        public void NextPoint() => _currentPointIndex = ++_currentPointIndex % _points.Count;

        public void SetNearestPoint(Vector3 currentPosition)
        {
            int nearestPointIndex = -1;
            float minSqrDistance = float.MaxValue;

            for (int index = 0; index < _points.Count; index++)
            {
                Vector3 point = _points[index].position;
                float sqrDistance = (point - currentPosition).sqrMagnitude;

                if (sqrDistance < minSqrDistance)
                {
                    nearestPointIndex = index;
                    minSqrDistance = sqrDistance;
                }
            }

            if (nearestPointIndex == -1)
                throw new InvalidOperationException("No patrol points");

            _currentPointIndex = nearestPointIndex;
        }
    }
}