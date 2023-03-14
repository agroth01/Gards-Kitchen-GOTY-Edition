using GK.Core;
using GK.Projectiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GK.Projectiles;
using GK.Player;

namespace GK.NewBoss
{
    /// <summary>
    /// Spawns multiple bullets that are all telegraphed. Reference: https://youtu.be/tVDCtm7EwsM?t=407 (WoW boss fight)
    /// </summary>
    public class CL_BottleBullet : Attack
    {
        [Header("Bullets")]
        [SerializeField] private Projectile _projectile;
        [SerializeField] private int _bulletAmount;
        [SerializeField] private float _bulletSpeed;
        [SerializeField] private float _spawnRate;
        [SerializeField] private float _delay;
        [SerializeField] private float _spawnDistance;
        [SerializeField] private float _damage;

        [Header("Telegraph")]
        [SerializeField] private Material _telegraphMaterial;
        [SerializeField] private float _telegraphScale;

        private Player.Player _player;

        private void Start()
        {
            _player = FindObjectOfType<Player.Player>();
        }

        protected override void OnAttack()
        {
            StartCoroutine(BottleBullets());
        }

        private IEnumerator BottleBullets()
        {
            yield return new WaitForSeconds(Delay);

            CompleteAttack();

            for (int i = 0; i < _bulletAmount; i++)
            {
                // Pick a random point with unitSphere and make the magnitude equal to spawnDistance
                Vector3 randomPoint = Random.insideUnitSphere.WithY(0f);
                randomPoint = randomPoint.normalized * _spawnDistance;
                randomPoint += transform.position;

                Vector3 playerDir = randomPoint.DirectionTo(_player.transform.position).WithY(0f);

                // Create the data for the projectile
                ProjectileData pd = new ProjectileData(_bulletSpeed, playerDir, _damage, Affiliation.Hostile, randomPoint);
                pd.IgnoreSurfaceCollisions = true;
                Projectile projectile = Boss.CreateProjectile(pd, _projectile, randomPoint, modifyForward:false);
                projectile.transform.up = playerDir;
                projectile.transform.parent = Phase.transform;

                // Create the telegraph. It will be placed at 0 0 at the upwards direction of it will point towards bullet spawn point
                Telegraph telegraph = TelegraphMaker.Instance.CreateTelegraph(_telegraphMaterial, new Vector2(randomPoint.x, randomPoint.z), _telegraphScale, _delay + 1 / _bulletSpeed);
                Quaternion l = Quaternion.LookRotation(playerDir, Vector3.up);
                Quaternion add = Quaternion.AngleAxis(90f, Vector3.right);
                telegraph.transform.rotation = l * add;

                telegraph.transform.position += telegraph.transform.up * 20f;

                yield return new WaitForSeconds(1 / _spawnRate);
            }

            
        }
    }
}