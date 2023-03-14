using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GK.Core;

namespace GK.Enemies
{
    public class WanderMotor : EnemyMotor
    {
        [Header("Wandering")]
        [Tooltip("How long to wait before starting to wander.")]
        [SerializeField] private RandomRange _waitDelay;

        [Tooltip("How long to wander for before stopping.")]
        [SerializeField] private RandomRange _wanderDelay;

        [Tooltip("How far the enemy can wander.")]
        [SerializeField] private RandomRange _wanderRange;

        private void Start()
        {
            StartCoroutine(WanderCoroutine());
        }

        private IEnumerator WanderCoroutine()
        {
            yield return new WaitForSeconds(_waitDelay.GetValue());

            while (true)
            {
                float wanderTime = _wanderDelay.GetValue();
                float wanderDistance = _wanderRange.GetValue();

                Vector3 destination = transform.position + Random.insideUnitSphere * wanderDistance;
                destination.y = transform.position.y;

                SetDestination(destination);

                yield return new WaitForSeconds(wanderTime);
                StopMoving();
            }
        }
    }
}