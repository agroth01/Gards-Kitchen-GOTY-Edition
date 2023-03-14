using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GK.Core;
using GK.Damage;
using System;

namespace GK.Entities
{
    /// <summary>
    /// An entity is the base class for both enemies and the player. This is used when taking damage, etc.
    /// </summary>
    public abstract class Entity : MonoBehaviour, IDamagable
    {
        public Action<DamageInfo> OnDamageTaken;
        public Action<Entity> OnDeath;

        [Header("Health")]
        [SerializeField] private ScaledFloat _maxHealth;

        private float _currentHealth;
        public float Health
        {
            get { return _currentHealth; }
            protected set
            {
                ModifyHealth(value);
            }
        }

        public float HealthPercentage
        {
            get { return _currentHealth / _maxHealth.Value; }
        }

        private void Awake()
        {
            _maxHealth.Initialize();
            _currentHealth = _maxHealth.Value;
        }


        /// <summary>
        /// IDamagable method implementation. This method does not actually handle the logic
        /// of taking damage, but is rather passed onto the abstract method TakeDamage, where
        /// all inheritors of this class can handle the logic of taking damage.
        /// </summary>
        /// <param name="damageInfo"></param>
        public void Damage(DamageInfo damageInfo)
        {
            OnDamageTaken?.Invoke(damageInfo);
            TakeDamage(damageInfo);
        }

        /// <summary>
        /// Specific implementation for when taking damage. This is where each inheriting class
        /// needs to determine how to handle damage.
        /// </summary>
        /// <param name="damageInfo"></param>
        protected abstract void TakeDamage(DamageInfo damageInfo);
        
        /// <summary>
        /// The specific implementation for what happens when the entity dies. The OnDeath event
        /// is triggered before this is called.
        /// </summary>
        protected abstract void Death();

        /// <summary>
        /// This will be called when using the setter of the Health property. This is the method
        /// that handles logic related to death. The reason for doing this is to allow stuff 
        /// like powers to modify the incoming damage from the TakeDamage() method before
        /// being applied.
        /// </summary>
        /// <param name="newAmount"></param>
        private void ModifyHealth(float newAmount)
        {
            _currentHealth = Mathf.Clamp(newAmount, 0, _maxHealth.Value);
            if (_currentHealth <= 0)
            {
                OnDeath?.Invoke(this);
                Death();
            }
        }
    }
}