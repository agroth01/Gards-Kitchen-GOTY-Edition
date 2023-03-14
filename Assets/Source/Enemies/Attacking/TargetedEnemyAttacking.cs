using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GK.Projectiles;
using GK.Core;

namespace GK.Enemies
{
    /// <summary>
    /// Enemy attacking component where the enemy will fire projectiles that are 
    /// targeted and aimed directly towards the player.
    /// </summary>
    public class TargetedEnemyAttacking : ProjectileShooter
    {
        [Header("Enemy shooting")]
        [SerializeField] private float _fireRate;
        [SerializeField] private float _attackRange;
        [SerializeField] private float _damage;
        [SerializeField] private float _projectileSpeed;

        [SerializeField] private Transform _target;

        private float _shotTimer;

        private void Awake()
        {
            if (_target == null)
                _target = GameObject.FindGameObjectWithTag("Friendly").transform;

            _shotTimer = 1 / _fireRate;
        }

        private void Update()
        {
            _shotTimer -= Time.deltaTime;
            float distance = transform.DistanceTo(_target);
            if (_shotTimer <= 0f && distance <= _attackRange)
            {
                Fire();
            }
        }

        private void Fire()
        {
            _shotTimer = 1 / _fireRate;

            Vector3 direction = transform.DirectionTo(_target);
            Debug.Log(direction);
            CreateProjectile(direction, _projectileSpeed, _damage);
        }
    }
}