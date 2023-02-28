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
        private ProjectileData _data;
        private Rigidbody _rigidbody;

        private void Awake()
        {
            SetupRigidbody();
        }

        private void OnCollisionEnter(Collision collision)
        {
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
