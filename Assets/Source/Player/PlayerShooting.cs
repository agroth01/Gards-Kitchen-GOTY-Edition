using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GK.Projectiles;
using GK.Core;
using GK.Input;
using MoreMountains.Feedbacks;

namespace GK.Player
{
    /// <summary>
    /// The class that is responsible for controlling the shooting of the player.
    /// </summary>
    public class PlayerShooting : ProjectileShooter, IInputListener
    {
        [Header("Shooting")]
        [SerializeField] private ScaledFloat _fireRate;
        [SerializeField] private ScaledFloat _damage;
        [SerializeField] private float _projectileSpeed;

        [Header("Feel")]
        [SerializeField] private MMF_Player _feedback;

        private float _nextShotTimer;

        /// <summary>
        /// Can the player fire?
        /// </summary>
        public bool CanFire
        {
            get { return _nextShotTimer <= 0f; }
        }

        /// <summary>
        /// How much time is left before the next shot is available.
        /// </summary>
        public float RemainingShotCooldown
        {
            get { return Mathf.Max(0f, _nextShotTimer); }
        }

        private void Awake()
        {
            _fireRate.Initialize();
            _damage.Initialize();
        }

        private void Update()
        {
            _nextShotTimer -= Time.deltaTime;
        }

        public void OnInput(InputData data)
        {
            if (data.Fire && CanFire)
            {
                Fire();
            }
        }

        private void Fire()
        {
            _nextShotTimer = 1 / _fireRate.Value;

            // Since the player is always facing the cursor, we can use the forward direction
            // of the player to base the projectile's direction on.
            Vector3 direction = transform.forward;
            CreateProjectile(direction, _projectileSpeed, _damage.Value);

            _feedback.PlayFeedbacks();
        }
    }
}