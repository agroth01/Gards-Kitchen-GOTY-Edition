using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GK.Core;

namespace GK.Camera
{
    /// <summary>
    /// Controls the camera by keeping a reference to the player and following it.
    /// Will also move the "World Cursor", which is a GameObject that is placed at the
    /// position of the cursor. Cinemachine is used to move the camera to also face this
    /// world cursor.
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private Transform _player;
        [SerializeField] private Transform _worldCursor;

        [Header("Look settings")]
        [SerializeField] private float _maxLookDistance;

        private void Update()
        {
            MoveWorldCursor();
        }

        private void MoveWorldCursor()
        {
            Vector3 mousePosition = MouseUtils.GetMousePosition();
            mousePosition.y = 0f;

            Vector3 playerPosition = _player.position.WithY(0);
            Vector3 direction = playerPosition.RawDirectionTo(mousePosition);

            // Clamp the distance to the max look distance
            if (direction.magnitude > _maxLookDistance)
            {
                direction = direction.normalized * _maxLookDistance;
            }

            _worldCursor.position = playerPosition + direction;
        }
    }
}