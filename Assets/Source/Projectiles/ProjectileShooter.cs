using GK.Common;
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
    public class ProjectileShooter : MonoBehaviour
    {
        [Header("Projectile")]
        [Tooltip("The projectile that will be created.")]
        [SerializeField] private Projectile _projectilePrefab;
        
        [Tooltip("The affiliation that will be given to projectiles created by this.")]
        [SerializeField] private Affiliation _defaultAffiliation;

        [Tooltip("The origin point where projectiles will originate from.")]
        [SerializeField] private Transform _projectileOriginPoint;

        [Header("Visuals")]
        [Tooltip("The particles that will be instantiated when a projectile is fired. Leave empty for no particles.")]
        [SerializeField] private GameObject _flashParticles;

        [Tooltip("How long the flash particles should be alive. Leave empty for infinite.")]
        [SerializeField] private float _flashLifetime;

        /// <summary>
        /// Creates a new projectile with parameters for the data passed directly into the method.
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="speed"></param>
        /// <param name="damage"></param>
        /// <returns></returns>
        public Projectile CreateProjectile(Vector3 direction, float speed, float damage)
        {
            Vector3 modifiedDirection = CalculateProjectileDirection(direction);
            ProjectileData data = new ProjectileData(speed, modifiedDirection, damage, _defaultAffiliation, _projectileOriginPoint.position);
            return CreateProjectile(data);
        }

        /// <summary>
        /// Creates a new projectile gameObject from projectile data provided.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Projectile CreateProjectile(ProjectileData data)
        {
            if (_flashParticles != null)
                CreateFlashParticles();
            
            Quaternion direction = Quaternion.LookRotation(data.Direction);

            GameObject projectileGO = Instantiate(_projectilePrefab.gameObject, _projectileOriginPoint.position, direction);
            Projectile projectile = projectileGO.GetComponent<Projectile>();
            projectile.SetData(data);
            return projectile;
        }

        /// <summary>
        /// Since the direction that projectiles are fired in is based on the entity itself and not accounting
        /// for the origin point, this method is used to account for the offset from the origin point.
        /// </summary>
        /// <param name="originalDirection"></param>
        /// <returns></returns>
        private Vector3 CalculateProjectileDirection(Vector3 originalDirection)
        {
            // We need to find a point that is far away from the origin point in the direction of the original direction.
            // This can then be used to find the actual direction our projectile needs to go in.
            Vector3 farAwayPoint = transform.position + originalDirection * 7.5f;
            return _projectileOriginPoint.DirectionTo(farAwayPoint);
        }

        /// <summary>
        /// Instantiates the flash particles and makes the origin point the parent. The reason for parenting like this
        /// is to make sure the particle follows the shooter. Otherwise it looks strange while moving at higher speeds.
        /// </summary>
        private void CreateFlashParticles()
        {
            GameObject flashGO = Instantiate(_flashParticles, _projectileOriginPoint.position, Quaternion.identity);
            flashGO.transform.parent = _projectileOriginPoint;

            if (_flashLifetime > 0f)
            {
                flashGO.AddComponent<TimedDestruction>().Delay = _flashLifetime;
            }
        }
    }
}