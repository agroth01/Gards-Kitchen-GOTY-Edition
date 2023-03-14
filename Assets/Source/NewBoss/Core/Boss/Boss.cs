using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GK.Damage;
using UnityEngine.AI;
using GK.Entities;
using GK.Common;
using GK.Projectiles;
using GK.Core;

namespace GK.NewBoss
{
    public class Boss : Entity
    {
        [Header("General settings")]
        [SerializeField] private string _displayName;
        [SerializeField] private float _movementSpeed;

        private NavMeshAgent _navMeshAgent;

        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        protected override void Death()
        {
            Destroy(gameObject);
        }

        protected override void TakeDamage(DamageInfo damageInfo)
        {
            Health = Health - damageInfo.Amount;
        }

        /// <summary>
        /// Creates a new projectile with parameters for the data passed directly into the method.
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="speed"></param>
        /// <param name="damage"></param>
        /// <returns></returns>
        public Projectile CreateProjectile(Projectile projectile, Vector3 direction, float speed, float damage)
        {
            Vector3 modifiedDirection = CalculateProjectileDirection(direction);
            ProjectileData data = new ProjectileData(speed, modifiedDirection, damage, Core.Affiliation.Hostile, transform.position);
            return CreateProjectile(data, projectile, transform.position);
        }

        public Projectile CreateProjectile(Projectile projectile, Vector3 position, Vector3 direction, float speed, float damage)
        {
            Vector3 modifiedDirection = CalculateProjectileDirection(direction);
            ProjectileData data = new ProjectileData(speed, modifiedDirection, damage, Core.Affiliation.Hostile, transform.position);
            return CreateProjectile(data, projectile, position);
        }

        /// <summary>
        /// Creates a new projectile gameObject from projectile data provided.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Projectile CreateProjectile(ProjectileData data, Projectile projectilePrefab, Vector3 position, bool modifyForward=true)
        {
            Quaternion direction = Quaternion.LookRotation(data.Direction);
            GameObject projectileGO = Instantiate(projectilePrefab.gameObject, position, direction);
            Projectile projectile = projectileGO.GetComponent<Projectile>();
            projectile.SetData(data, modifyForward);
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
            return transform.DirectionTo(farAwayPoint);
        }

        /// <summary>
        /// Instantiates the flash particles and makes the origin point the parent. The reason for parenting like this
        /// is to make sure the particle follows the shooter. Otherwise it looks strange while moving at higher speeds.
        /// </summary>
        //private void CreateFlashParticles()
        //{
        //    GameObject flashGO = Instantiate(_flashParticles, _projectileOriginPoint.position, Quaternion.identity);
        //    flashGO.transform.parent = _projectileOriginPoint;

        //    if (_flashLifetime > 0f)
        //    {
        //        flashGO.AddComponent<TimedDestruction>().Delay = _flashLifetime;
        //    }
        //}
    }
}