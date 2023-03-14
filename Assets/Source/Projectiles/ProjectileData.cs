using GK.Core;
using GK.Entities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GK.Projectiles
{
    /// <summary>
    /// Data about a projectile that is being fired. The reason for this class is to allow for 
    /// modifications of projectiles without having to modify the projectile itself. For example,
    /// changing the speed of a projectile without having to modify the projectile's script.
    /// </summary>
    public class ProjectileData
    {
        public float Speed;
        public Vector3 Direction;
        public float Damage;
        public Affiliation Affiliation;
        public Vector3 OriginPoint;
        public bool IgnoreSurfaceCollisions = false;

        /// <summary>
        /// The velocity of the projectile. Is combined from the speed and direction.
        /// </summary>
        public Vector3 Velocity
        {
            get { return Speed * Direction; }
        }

        public ProjectileData(float speed, Vector3 direction, float damage, Affiliation affiliation, Vector3 originPoint)
        {
            Speed = speed;
            Direction = direction;
            Damage = damage;
            Affiliation = affiliation;
            OriginPoint = originPoint;
        }
    }
}