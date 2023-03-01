using GK.Common;
using GK.Core;
using GK.Damage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GK.Projectiles
{
    /// <summary>
    /// The main component for any projectile in the game. Is responsible for the movement of
    /// the projectile through the world, as well as any collision detection. For more complex
    /// behaviours, additional components can be added to the projectile.
    /// 
    /// Uses a rigidbody for movement, and a collider for collision detection.
    /// </summary>
    [RequireComponent(typeof(Rigidbody), typeof(Collider))]
    public class Projectile : MonoBehaviour
    {
        [Header("Visuals")]
        [SerializeField] private GameObject _hitPrefab;

        private ProjectileData _data;
        private Rigidbody _rigidbody;
        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            SetupRigidbody();
            AssignTag();
        }

        private void OnCollisionEnter(Collision collision)
        {
            // Projectiles might sometimes collide with other projectiles. This should be ignored.
            if (collision.gameObject.CompareTag(gameObject.tag))
                HandleInvalidCollision(collision);

            // Similarly, the projectile might collide with someone of the same affiliation as the
            // projectile. This should also be ignored.
            else if (collision.gameObject.CompareTag(_data.Affiliation.GetTag()))
                HandleInvalidCollision(collision);

            else
                HandleValidCollision(collision);
        }

        /// <summary>
        /// When the projectile collides with either another projectile or someone of the same affiliation,
        /// the collision should simply be ignored. This method handles that.
        /// </summary>
        /// <param name="collision"></param>
        private void HandleInvalidCollision(Collision collision)
        {
            // Tell the physics system to just ignore the collision.
            Physics.IgnoreCollision(collision.collider, _collider);
        }

        /// <summary>
        /// If the collision is not invalid, the collision is handled here. This checks if the projectile hit
        /// an entity or a normal surface.
        /// </summary>
        /// <param name="collision"></param>
        private void HandleValidCollision(Collision collision)
        {
            IDamagable damagable = collision.gameObject.GetComponent<IDamagable>();
            if (damagable != null)
            {
                // We hit something that should be able to take damage.
                DamageInfo di = new DamageInfo();
                di.Receiver = damagable;
                di.Amount = _data.Damage;

                damagable.Damage(di);
            }

            // When creating the hit particles, we make sure to set the object hit as the
            // parent of the particles, so that they follow the object hit and look more
            // correct.
            GameObject particlesGO = Instantiate(_hitPrefab, collision.contacts[0].point, Quaternion.identity);
            particlesGO.transform.parent = collision.gameObject.transform;
            particlesGO.AddComponent<TimedDestruction>().Delay = 1f;

            // The projectile gets destroyed anyways. Maybe add bounces/penetrations in the future.
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            // Projectiles might sometimes collide with other projectiles. This should be ignored.
            if (other.gameObject.CompareTag(gameObject.tag))
                return;

            // Similarly, the projectile might collide with someone of the same affiliation as the
            // projectile. This should also be ignored.
            if (other.gameObject.CompareTag(_data.Affiliation.GetTag()))
                return;


            Destroy(gameObject);
        }


        /// <summary>
        /// Sets the data that this projectile will use. This will cause the projectile to
        /// move in the direction and speed specified in the data.
        /// </summary>
        /// <param name="data"></param>
        public void SetData(ProjectileData data)
        {
            _data = data;
            ModifyFromData();
        }

        /// <summary>
        /// Since the rigidbody needs modified settings, I have made this method to avoid having to 
        /// remember set the correct settings for all projectiles. Probably less performant, but
        /// who cares \o/
        /// </summary>
        private void SetupRigidbody()
        {
            _rigidbody = GetComponent<Rigidbody>();

            _rigidbody.useGravity = false;
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            _rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
        }

        /// <summary>
        /// To make sure I don't forget to assign a tag to the projectile, I have made this method
        /// that will automatically assign the "Projectile" tag to projectiles on creation.
        /// </summary>
        private void AssignTag()
        {
            gameObject.tag = "Projectile";
        }

        /// <summary>
        /// Sets the values based on the projectile data provided.
        /// </summary>
        private void ModifyFromData()
        {
            // Begone evil null references.
            if (_data == null)
                return;

            _rigidbody.velocity = _data.Velocity;
            transform.forward = _data.Direction;
        }
    }
}
