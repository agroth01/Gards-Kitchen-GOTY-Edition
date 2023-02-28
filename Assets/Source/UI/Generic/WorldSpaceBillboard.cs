using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GK.UI
{
    /// <summary>
    /// A script that will make a UI element always face the camera.
    /// </summary>
    public class WorldSpaceBillboard : MonoBehaviour
    {
        private Transform _cameraTransform;

        private void Awake()
        {
            _cameraTransform = UnityEngine.Camera.main.transform;
        }

        private void LateUpdate()
        {
            transform.LookAt(transform.position + _cameraTransform.rotation * Vector3.forward, _cameraTransform.rotation * Vector3.up);
        }
    }
}