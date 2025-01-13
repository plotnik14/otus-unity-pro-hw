using System;
using JetBrains.Annotations;
using UnityEngine;
using Utils;

namespace Units
{
    public class Worker : MonoBehaviour
    {
        public event Action<int> OnWoodCountChanged;

        [SerializeField] private float _speed;

        private Vector3 _direction;
        private Transform _transformCached;
        private int _woodCount;

        public int WoodCount
        {
            get => _woodCount;
            set
            {
                _woodCount = value;
                OnWoodCountChanged.SafeInvoke(_woodCount);
            }
        }

        [UsedImplicitly]
        private void Awake() => _transformCached = transform;

        [UsedImplicitly]
        private void Update()
        {
            if (_direction == Vector3.zero)
                return;

            Vector3 nextPosition = _transformCached.position + _direction * (_speed * Time.deltaTime);
            _transformCached.LookAt(nextPosition);
            _transformCached.position = nextPosition;
        }

        public void SetDirection(Vector3 direction) => _direction = direction;
    }
}