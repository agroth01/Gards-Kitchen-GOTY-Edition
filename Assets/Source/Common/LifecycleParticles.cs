using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GK.Core;

namespace GK.Common
{
    /// <summary>
    /// Spawn particles on various Unity lifecycle methods such as Start, Update, etc.
    /// This is mainly used by projectiles to spawn particles, for an example flashes
    /// or hit effects.
    /// </summary>
    public class LifecycleParticles : MonoBehaviour
    {
        [System.Serializable]
        private struct ParticleData
        {
            [Tooltip("The Unity lifecycle method to spawn the particles on.")]
            public UnityLifecycleMethod LifecycleMethod;

            [Tooltip("How long the particles should be alive before automatic destruction. Leave at 0 to never destroy.")]
            public float ParticleLifetime;

            [Tooltip("The particle system to spawn.")]
            public ParticleSystem ParticleSystem;

            [Tooltip("The offset from the parent object to spawn the particles at.")]
            public Vector3 Offset;

            [Tooltip("The parent object to spawn the particles at. Leave empty for no parent.")]
            public Transform Parent;
        }

        public Action<GameObject> OnParticleSpawned;

        [SerializeField] private List<ParticleData> _particles;

        private bool _isQuitting = false;

        private void Awake()
        {
            SpawnParticles(UnityLifecycleMethod.Awake);
        }

        private void Start()
        {
            SpawnParticles(UnityLifecycleMethod.Start);
        }

        private void OnEnable()
        {
            SpawnParticles(UnityLifecycleMethod.OnEnable);
        }

        private void OnDisable()
        {
            SpawnParticles(UnityLifecycleMethod.OnDisable);
        }

        private void Update()
        {
            SpawnParticles(UnityLifecycleMethod.Update);
        }

        private void FixedUpdate()
        {
            SpawnParticles(UnityLifecycleMethod.FixedUpdate);
        }

        private void OnDestroy()
        {
            // If the OnDestroy method is called because of the application quitting,
            // we do not want to instantiate anything.
            if (_isQuitting)
                return;

            SpawnParticles(UnityLifecycleMethod.OnDestroy);
        }

        private void OnApplicationQuit()
        {
            _isQuitting = true;
        }

        private void SpawnParticles(UnityLifecycleMethod lifecycleMethod)
        {
            foreach (var particleData in _particles)
            {
                if (particleData.LifecycleMethod == lifecycleMethod)
                {
                    Vector3 spawnPosition = transform.position + particleData.Offset;

                    GameObject particleObject = Instantiate(particleData.ParticleSystem.gameObject, spawnPosition, transform.rotation);

                    if (particleData.ParticleLifetime > 0f)
                        particleObject.AddComponent<TimedDestruction>().Delay = particleData.ParticleLifetime;

                    OnParticleSpawned?.Invoke(particleObject);
                }
            }
        }
    }
}