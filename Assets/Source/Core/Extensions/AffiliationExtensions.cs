using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GK.Core
{
    /// <summary>
    /// Extension methods for the affiliation enum.
    /// </summary>
    public static class AffiliationExtensions
    {
        /// <summary>
        /// Gets the tag that is associated with the affiliation.
        /// </summary>
        /// <param name="affiliation"></param>
        /// <returns></returns>
        public static string GetTag(this Affiliation affiliation)
        {
            switch (affiliation)
            {
                case Affiliation.Friendly:
                    return "Friendly";
                case Affiliation.Hostile:
                    return "Hostile";
                default:
                    return "Untagged";
            }
        }

        /// <summary>
        /// Gets the tag that is associated with the opposite affiliation. This is used
        /// for projectiles when determining collisions.
        /// </summary>
        /// <param name="affiliation"></param>
        /// <returns></returns>
        public static string GetOppositeTag(this Affiliation affiliation)
        {
            switch (affiliation)
            {
                case Affiliation.Friendly:
                    return "Hostile";
                case Affiliation.Hostile:
                    return "Friendly";
                default:
                    return "Untagged";
            }
        }

        /// <summary>
        /// Returns the color that is associated with this affiliation.
        /// </summary>
        /// <param name="affiliation"></param>
        /// <returns></returns>
        public static Color GetColor(this Affiliation affiliation)
        {
            switch (affiliation)
            {
                case Affiliation.Friendly:
                    return Color.green;
                case Affiliation.Hostile:
                    return Color.red;
                default:
                    return Color.white;
            }
        }
    }
}
