using GK.Projectiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GK.NewBoss
{
    public class CL_AreaAttack : Attack
    {
        [Header("Area attack")]
        [SerializeField] private Projectile _projectile;
        [SerializeField] private int _areaProjectileAmount = 12;
        [SerializeField] private float _areaProjectileSpeed = 15f;
        [SerializeField] private int _areaAmount = 5;
        [SerializeField] private float _areaDelay = 0.25f;
        [SerializeField] private float _projectileDamage;
        [SerializeField] private float _rotationOffset;

        protected override void OnAttack()
        {
            StartCoroutine(AreaAttack());
        }

        private IEnumerator AreaAttack()
        {
            yield return new WaitForSeconds(Delay);

            float angleBetweenProjectiles = 360 / _areaProjectileAmount;

            for (int j = 0; j < _areaAmount; j++)
            {
                for (int i = 0; i < _areaProjectileAmount; i++)
                {
                    float rotation = i * angleBetweenProjectiles + _rotationOffset;
                    Vector3 direction = Quaternion.Euler(0, rotation, 0) * Vector3.forward;

                    Boss.CreateProjectile(_projectile, direction, _areaProjectileSpeed, _projectileDamage);
                }

                yield return new WaitForSeconds(_areaDelay);
            }

            CompleteAttack();
        }
    }
}
