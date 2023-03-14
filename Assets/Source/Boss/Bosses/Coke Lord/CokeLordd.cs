using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GK.Boss
{
    public class CokeLordd : Boss
    {
        [Header("Coke Lord")]
        [SerializeReference] private float _projectileDamage;

        [Header("Spiral Attack")]
        [SerializeReference] private int _spiralProjectileAmount;
        [SerializeReference] private int _spiralLoops;
        [SerializeReference] private float _spiralDelay;
        [SerializeReference] private float _spiralProjectileSpeed;
        [SerializeReference] private float _rotIncrease;

        [SerializeReference] private float _testTime;

        private IEnumerator SpiralAttack(float extraAngle)
        {
            // spread out all projectiles in 360 degrees
            float angleBetweenProjectiles = 360f / _spiralProjectileAmount;;

            for (int i = 0; i < _spiralProjectileAmount; i++)
            {
                // calculate rotation and convert to vector3
                float rotation = i * angleBetweenProjectiles + extraAngle;
                Vector3 direction = Quaternion.Euler(0, rotation, 0) * Vector3.forward;

                ProjectileShooter.CreateProjectile(direction, _spiralProjectileSpeed, _projectileDamage);

                // Wait for the delay before spawning the next projectile
                yield return new WaitForSeconds(_spiralDelay);
            }

            yield return new WaitForSeconds(_testTime);
            yield return SpiralAttack(extraAngle + _rotIncrease);
        }

        protected override void SetPhases()
        {
            AddPhase(new CL_PhaseOne());
        }
    }

}