using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GK.Core
{
    /// <summary>
    /// Extension classes for Vector3. This class is used for either convenience, readability of code
    /// or to extend the functionality of the Vector3 class.
    /// </summary>
    public static class Vector3Extensions
    {
        /// <summary>
        /// Calculates the direction from one Vector3 to another Vector3.
        /// The resulting vector is normalized before returning.
        /// </summary>
        /// <param name="vf">The vector to base direction from.</param>
        /// <param name="vt">The vector to get direction to.</param>
        /// <returns>A normalized Vector3 pointing towards VT</returns>
        public static Vector3 DirectionTo(this Vector3 vf, Vector3 vt)
        {
            Vector3 direction = vt - vf;
            return direction.normalized;
        }

        /// <summary>
        /// Calculates the direction from one Vector3 to another Vector3.
        /// The resulting vector is not normalized.
        /// </summary>
        /// <param name="vf"></param>
        /// <param name="vt"></param>
        /// <returns></returns>
        public static Vector3 RawDirectionTo(this Vector3 vf, Vector3 vt)
        {
            return (vt - vf);
        }

        /// <summary>
        /// Another way to get the distance between two vector3's. Could also just use
        /// Vector3.Distance(), but this method I think gives better syntax all in all.
        /// </summary>
        /// <param name="vf"></param>
        /// <param name="vt"></param>
        /// <returns>A float representing distance in world units between the points.</returns>
        public static float DistanceTo(this Vector3 vf, Vector3 vt)
        {
            float distance = Vector3.Distance(vf, vt);
            return distance;
        }

        /// <summary>
        /// Calculates the distance between a point in world space and a transform's
        /// position.
        /// </summary>
        /// <param name="vf">The first point in distance calculation.</param>
        /// <param name="tt">The transform for second point in distance calculation.</param>
        /// <returns>A float representing distance in world units between the transform and point.</returns>
        public static float DistanceTo(this Vector3 vf, Transform tt)
        {
            float distance = Vector3.Distance(vf, tt.position);
            return distance;
        }

        public static Vector3 WithX(this Vector3 v, float x)
        {
            return new Vector3(x, v.y, v.z);
        }

        public static Vector3 WithY(this Vector3 v, float y)
        {
            return new Vector3(v.x, y, v.z);
        }

        public static Vector3 WithZ(this Vector3 v, float z)
        {
            return new Vector3(v.x, v.y, z);
        }

    }
}
