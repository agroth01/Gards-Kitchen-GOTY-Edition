using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GK.Common
{
    /// <summary>
    /// Component that will automatically destroy the GameObject that this is attached to
    /// after a certain amount of time.
    /// </summary>
    public class TimedDestruction : MonoBehaviour
    {
        public Action<GameObject> OnDestruction;

        [Header("Settings")]
        [Tooltip("How long to wait before destroying the GameObject.")]
        public float Delay = 1f;

        private float _timer;

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer >= Delay)
            {
                OnDestruction?.Invoke(gameObject);
                Destroy(gameObject);
            }
        }
    }
}