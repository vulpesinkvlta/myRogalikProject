using Unity.Cinemachine;
using UnityEngine;

namespace Core
{
    public class RoomCameraController : MonoBehaviour
    {
        [SerializeField] private CinemachineCamera _camera;
        [SerializeField] private CinemachineConfiner2D _confiner;

        public void SetRoom(RoomController room)
        {
            if(room == null)
                return;

            if(room.CameraBounds == null)
                return;

            _confiner.BoundingShape2D = room.CameraBounds;
            _confiner.InvalidateBoundingShapeCache();
        }

        public void NotifyPlayerWarped(Transform playerTransform, Vector3 positionDelta)
        {
            if (_camera == null)
                return;

            _camera.OnTargetObjectWarped(playerTransform, positionDelta);
        }
    }
}
