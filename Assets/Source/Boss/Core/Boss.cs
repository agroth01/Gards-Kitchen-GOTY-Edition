using GK.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GK.Core;
using GK.Projectiles;
using UnityEngine.AI;
using Gk.Boss;

namespace GK.Boss
{
    /// <summary>
    /// The base class for a boss in the game. Contains methods for moving the boss, taking damage and information like
    /// name, theme music etc. All bosses's behaviour is state driven. All bosses that inherits from this will be driven
    /// by a finite-state machine.
    /// </summary>
    public abstract class Boss : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] private string _displayName;     

        [Header("Music")]
        [SerializeField] private AudioClip _theme;
        [SerializeField] private float _themeVolume = 1f;
        [SerializeField] private float _themeEaseInDuration = 3f;

        [Header("Movement")]
        [SerializeField] private float _defaultMovementSpeed;

        [Header("Other")]
        [SerializeField] private List<Minion> _minions;

        private ProjectileShooter _projectileShooter;
        private Transform _player;
        private NavMeshAgent _navMeshAgent;

        private List<Phase> _phases = new List<Phase>();
        private Phase _activePhase;
        
        public ProjectileShooter ProjectileShooter
        {
            get { return _projectileShooter; }
        }

        public Transform Player
        {
            get { return _player; }
        }

        private Vector3 _targetPosition;

        protected void Awake()
        {
            _player = GameObject.FindGameObjectWithTag("Friendly").transform;
            _projectileShooter = GetComponent<ProjectileShooter>();
            SetupAgent();
            SetPhases();
            _activePhase = _phases[0];
        }

        protected virtual void Start()
        {
            MusicManager.Instance.PlaySong(_theme, volume:_themeVolume);
            MusicManager.Instance.EaseIn(_themeEaseInDuration);
        }

        protected virtual void Update()
        {
            _activePhase.UpdatePhase();
        }

        protected void AddPhase(Phase phase)
        {
            _phases.Add(phase);
            phase.Boss = this;
        }

        public void NextPhase()
        {
            _activePhase = _phases[_phases.IndexOf(_activePhase) + 1];
        }

        /// <summary>
        /// Sets the player's transform position as target for the navmeshagent.
        /// </summary>
        public void ChasePlayer()
        {
            _navMeshAgent.SetDestination(_player.position);
            _navMeshAgent.isStopped = false;
        }

        public void MoveTo(Vector3 position)
        {
            _navMeshAgent.SetDestination(position);
            _navMeshAgent.isStopped = false;
        }

        public void SetSpeed(float speed)
        {
            _navMeshAgent.speed = speed;
        }

        public void StopMovement()
        {
            _navMeshAgent.isStopped = true;
        }

        protected abstract void SetPhases();

        private void SetupAgent()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();

            _navMeshAgent.speed = _defaultMovementSpeed;
        }
    }
}