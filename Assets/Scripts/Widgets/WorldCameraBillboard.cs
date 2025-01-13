using Sirenix.OdinInspector;
using UnityEngine;

namespace Widgets
{
    [ExecuteAlways]
    public sealed class WorldCameraBillboard : MonoBehaviour
    {
        [SerializeField] private bool _lookAtStart;

        private void Start()
        {
            if (_lookAtStart)
                LookAtCamera();
        }

        [Button]
        public void LookAtCamera()
        {
            Vector3 rootPosition = transform.position;
            Camera cameraInstance = Camera.main;

            if (cameraInstance is null)
            {
                return;
            }

            Quaternion cameraRotation = cameraInstance.transform.rotation;
            Vector3 cameraVector = cameraRotation * Vector3.forward;
            Vector3 worldUp = cameraRotation * Vector3.up;
            transform.LookAt(rootPosition + cameraVector, worldUp);
        }
    }
}