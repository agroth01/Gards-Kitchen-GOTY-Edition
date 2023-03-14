using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GK.Projectiles;

namespace GK.NewBoss
{
    public class CL_SpiralAttack : Attack
    {
        [Header("Spiral attack")]
        [SerializeField] private Projectile _projectile;
        [SerializeField] private int _spirals;
        [SerializeField] private int _projectiles;
        [SerializeField] private float _projectileDelay;
        [SerializeField] private float _projectileSpeed;
        [SerializeField] private float _projectileDamage;
        [SerializeField] private float _spiralRotationIncrease;

        protected override void OnAttack()
        {
            StartCoroutine(SpiralAttack(0));
        }

        private IEnumerator SpiralAttack(float extraAngle)
        {
            yield return new WaitForSeconds(Delay);

            // spread out all projectiles in 360 degrees
            float angleBetweenProjectiles = 360f / _projectiles;

            for (int j = 0; j < _spirals; j++)
            {
                for (int i = 0; i < _projectiles; i++)
                {
                    // calculate rotation and convert to vector3
                    float rotation = i * angleBetweenProjectiles + extraAngle;
                    Vector3 direction = Quaternion.Euler(0, rotation, 0) * Vector3.forward;

                    Projectile p = Boss.CreateProjectile(_projectile, direction, _projectileSpeed, _projectileDamage);
                    p.transform.parent = Phase.transform;
                    

                    // Wait for the delay before spawning the next projectile
                    yield return new WaitForSeconds(_projectileDelay);
                }

                extraAngle += _spiralRotationIncrease;
            }

            CompleteAttack();
        }
    }
}
