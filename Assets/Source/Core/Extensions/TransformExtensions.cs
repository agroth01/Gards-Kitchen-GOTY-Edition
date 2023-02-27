using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GK.Core
{
    /// <summary>
    /// Extension methods for the Transform component. Mostly just wrappers around Vector3Extensions
    /// to avoid having to get a transform's position, but also contains some other useful methods.
    /// </summary>
    public static class TransformExtensions
    {
        /// <summary>
        /// Calculates the direction from one transform to another.
        /// The resulting vector is normalized.
        /// </summary>
        /// <param name="tf">The transform to base directional calculation from.</param>
        /// <param name="tt">The transform to get direction towards.</param>
        /// <returns>A normalized Vector3</returns>
        public static Vector3 DirectionTo(this Transform tf, Transform tt)
        {
            Vector3 direction = tf.position.DirectionTo(tt.position);
            return direction;
        }

        /// <summary>
        /// Calculates the direction from a transform to a Vector3 point.
        /// The resulting vector is normalized.
        /// </summary>
        /// <param name="tf"></param>
        /// <param name="vt"></param>
        /// <returns></returns>
        public static Vector3 DirectionTo(this Transform tf, Vector3 vt)
        {
            Vector3 direction = tf.position.DirectionTo(vt);
            return direction;
        }

        /// <summary>
        /// Returns the distance between the positions of two transforms.
        /// </summary>
        /// <param name="tf">The first transform</param>
        /// <param name="tt">The second transform</param>
        /// <returns>A float representing distance in world units from eachother.</returns>
        public static float DistanceTo(this Transform tf, Transform tt)
        {
            float distance = tf.position.DistanceTo(tt.position);
            return distance;
        }
    }
}
