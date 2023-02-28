using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GK.Core
{
    /// <summary>
    /// Collection of utility methods for the mouse.
    /// </summary>
    public static class MouseUtils
    {
        /// <summary>
        /// Sends a ray from the cursor position into the scene and returns the hit point.
        /// Will return Vector3.Zero if no hit is found.
        /// </summary>
        /// <returns></returns>
        public static Vector3 GetMousePosition()
        {
            Ray ray = UnityEngine.Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                return hit.point;
            }
            return Vector3.zero;
        }

        /// <summary>
        /// Sends a ray from the cursor position into the scene and returns the hit point
        /// with specified layermask. Will return Vector3.Zero if no hit is found.
        /// </summary>
        /// <param name="layerMask"></param>
        /// <returns></returns>
        public static Vector3 GetMousePosition(LayerMask layerMask)
        {
            Ray ray = UnityEngine.Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                return hit.point;
            }
            return Vector3.zero;
        }
    }
}
