using GK.Core;
using GK.Damage;
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
        [SerializeField] private bool _canBePushed = true;
        [SerializeField] private float _defaultPushStrength = 1f;
        [SerializeField] private float _pushTimeMultiplier = 10f;

        private NavMeshAgent _agent;
        private Rigidbody _rigidbody;
        private Enemy _enemy;

        private Vector3 _destination;
        private bool _isPushed;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _rigidbody = GetComponent<Rigidbody>();
            _enemy = GetComponent<Enemy>();
            InitializeComponents();
        }

        /// <summary>
        /// Push the enemy back by the given force. Temporarily enables the rigidbody of
        /// the enemy for the force. Duration of push before enemy ragains control is
        /// based on the force magnitude.
        /// </summary>
        /// <param name="force"></param>
        public void Push(Vector3 force)
        {
            if (!_canBePushed)
                return;

            StartCoroutine(PushCoroutine(force));
        }

        protected void SetDestination(Vector3 destination)
        {
            // Disable using SetDestination while the enemy is being pushed, as the
            // enemy is simply moved with the physics system.
            if (_isPushed)
                return;
            
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

            _enemy.OnDamageTaken += OnDamage;
        }

        private IEnumerator PushCoroutine(Vector3 force)
        {
            StopMoving();
            _isPushed = true;
            _rigidbody.isKinematic = false;
            _rigidbody.velocity = force;

            float wait = force.magnitude / _pushTimeMultiplier;
            yield return new WaitForSeconds(wait);

            _rigidbody.isKinematic = true;
            _isPushed = false;
        }

        private void OnDamage(DamageInfo info)
        {
            Vector3 force = info.OriginPoint.DirectionTo(transform) * _defaultPushStrength;
            Push(force);
        }
    }
}