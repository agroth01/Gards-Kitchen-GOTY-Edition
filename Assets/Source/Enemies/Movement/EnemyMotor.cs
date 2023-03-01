using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace GK.Enemies
{
    /// <summary>
    /// The base class for all enemy movement scripts. Contains a reference to the enemy's
    /// navagent and a method for moving the enemy to a target position. It also has functionality
    /// for pushing enemies back, via the attached rigidbody.
    /// </summary>
    [RequireComponent(typeof(NavMeshAgent), typeof(Rigidbody))]
    public abstract class EnemyMotor : MonoBehaviour
    {
        [Header("Movement")]
        [Tooltip("How fast the enemy moves.")]
        [SerializeField] private float _speed;

        private NavMeshAgent _agent;
        private Rigidbody _rigidbody;

        private Vector3 _destination;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _rigidbody = GetComponent<Rigidbody>();
            InitializeComponents();
        }

        protected void SetDestination(Vector3 destination)
        {
            _agent.isStopped = false;
            _destination = destination;
            _agent.SetDestination(_destination);
        }

        protected void StopMoving()
        {
            _agent.isStopped = true;
        }

        /// <summary>
        /// Set the correct settings for referenced components.
        /// </summary>
        private void InitializeComponents()
        {
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
            _rigidbody.isKinematic = true;
            _rigidbody.useGravity = false;

            _agent.speed = _speed;
        }
    }
}