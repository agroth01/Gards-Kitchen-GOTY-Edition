using GK.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GK.Projectiles
{
    /// <summary>
    /// The base for any component that involves firing projectiles. By itself, this class only
    /// provides methods for creating and shooting out projectiles. It does not handle logic for
    /// when to shoot, where to shoot from, etc.
    /// </summary>
    public abstract class ProjectileShooter : MonoBehaviour
    {
        [Header("Projectile")]
        [Tooltip("The projectile that will be created.")]
        [SerializeField] private Projectile _projectilePrefab;
        
        [Tooltip("The affiliation that will be given to projectiles created by this.")]
        [SerializeField] private Affiliation _defaultAffiliation;

        [Tooltip("The origin point where projectiles will originate from.")]
        [SerializeField] private Transform _projectileOriginPoint;

        /// <summary>
        /// Creates a new projectile with parameters for the data passed directly into the method.
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="speed"></param>
        /// <param name="damage"></param>
        /// <returns></returns>
        protected Projectile CreateProjectile(Vector3 direction, float speed, float damage)
        {
            ProjectileData data = new ProjectileData(speed, direction, damage, _defaultAffiliation);
            return CreateProjectile(data);
        }

        /// <summary>
        /// Creates a new projectile gameObject from projectile data provided.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        protected Projectile CreateProjectile(ProjectileData data)
        {
            GameObject projectileGO = Instantiate(_projectilePrefab.gameObject, _projectileOriginPoint.position, Quaternion.identity);
            Projectile projectile = projectileGO.GetComponent<Projectile>();
            projectile.SetData(data);
            return projectile;
        }
    }
}