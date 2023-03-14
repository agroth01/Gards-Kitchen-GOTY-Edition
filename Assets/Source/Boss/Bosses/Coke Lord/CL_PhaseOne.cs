using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GK.Core;
using GK.Projectiles;

namespace GK.Boss
{
    /// <summary>
    /// In phase 1 of the Coke Lord boss fight, he will have a spiral attack,
    /// a shotgun attack and an area attack.
    /// </summary>
    public class CL_PhaseOne : Phase
    {
        // Percentage of health that the boss has to be at to transition to the next phase.
        private float _healthTransitionPercent = 0.7f;

        private int _spiralProjectileAmount = 18;    // How many projectiles should be fired in the spiral attack
        private float _spiralProjectileDelay = 0.03f;   // How much time between each projectile being fired in the spiral attack
        private float _spiralProjectileSpeed = 6f;
        private float _spiralRotationIncrease = 12f;
        private float _spiralAmount = 10f;

        private int _shotgunProjectileAmount = 8;   // How many projectiles are fired in shotgun like attack
        private float _shotgunArcAngle = 45f;         // How wide the projectiles should be spread out.
        private float _shotgunProjectileSpeed = 12f;

        private int _areaProjectileAmount = 12;      // How many projectiles are fired in the area attack
        private float _areaProjectileSpeed = 15f;
        private int _areaAmount = 5;
        private float _areaDelay = 0.25f;

        private float _projectileDamage = 1f;

        private float _attackDelay = 3f;
        private bool _inAttack;

        private float _desiredDistance = 5f;

        public override bool ShouldEnd() 
        {
            return false;
        }

        public override void UpdatePhase()
        {
            _attackDelay -= (_inAttack) ? 0 : Time.deltaTime;
            if (_attackDelay <= 0f)
            {
                // Roll a random number between 1 and 3 to determine which attack to use
                int attack = Random.Range(1, 4);

                switch (attack)
                {
                    case 1:
                        // Start the spiral attack
                        Boss.StartCoroutine(SpiralAttack(0));
                        break;
                    case 2:
                        // Start the shotgun attack
                        Boss.StartCoroutine(ShotgunAttack());
                        break; 
                    case 3:
                        // Start the area attack
                        Boss.StartCoroutine(AreaAttack());
                        break;
                }

                // Reset the attack delay
                _attackDelay = 3f;
            }

            if (_inAttack)
                Boss.StopMovement();
            else
            {
                float distance = Boss.transform.DistanceTo(Boss.Player);
                if (distance > _desiredDistance)
                    Boss.ChasePlayer();

                else
                    Boss.StopMovement();
            }
        }

        

        private IEnumerator SpiralAttack(float extraAngle)
        {
            _inAttack = true;

            // spread out all projectiles in 360 degrees
            float angleBetweenProjectiles = 360f / _spiralProjectileAmount; ;

            for (int j = 0; j < _spiralAmount; j++)
            {
                for (int i = 0; i < _spiralProjectileAmount; i++)
                {
                    // calculate rotation and convert to vector3
                    float rotation = i * angleBetweenProjectiles + extraAngle;
                    Vector3 direction = Quaternion.Euler(0, rotation, 0) * Vector3.forward;

                    Boss.ProjectileShooter.CreateProjectile(direction, _spiralProjectileSpeed, _projectileDamage);

                    // Wait for the delay before spawning the next projectile
                    yield return new WaitForSeconds(_spiralProjectileDelay);
                }

                extraAngle += _spiralRotationIncrease;
            }

            _inAttack = false;
        }

        private IEnumerator ShotgunAttack()
        {
            _inAttack = true;

            // evenly spread out all shots across angle
            float angleBetweenProjectiles = _shotgunArcAngle / _shotgunProjectileAmount;

            Vector3 playerDirection = Boss.transform.DirectionTo(Boss.Player);

            float extraAngle = -(_shotgunArcAngle / 2);

            for (int i = 0; i <_shotgunProjectileAmount; i++)
            {
                float rotation = i * angleBetweenProjectiles + extraAngle;
                Vector3 direction = Quaternion.Euler(0, rotation, 0) * playerDirection;

                Boss.ProjectileShooter.CreateProjectile(direction, _shotgunProjectileSpeed, _projectileDamage);

                extraAngle += angleBetweenProjectiles;
            }

            _attackDelay += 1f;
            _inAttack = false;

            yield return 0;
        }

        private IEnumerator AreaAttack()
        {
            _inAttack = true;

            float angleBetweenProjectiles = 360 / _areaProjectileAmount;

            for (int j = 0; j < _areaAmount; j++)
            {
                for (int i = 0; i < _areaProjectileAmount; i++)
                {
                    float rotation = i * angleBetweenProjectiles;
                    Vector3 direction = Quaternion.Euler(0, rotation, 0) * Vector3.forward;

                    Boss.ProjectileShooter.CreateProjectile(direction, _areaProjectileSpeed, _projectileDamage);
                }

                yield return new WaitForSeconds(_areaDelay);
            }

            _inAttack = false;
        }
    }
}
