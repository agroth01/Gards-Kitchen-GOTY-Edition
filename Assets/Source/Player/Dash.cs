using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GK.Core;
using GK.Input;
using MoreMountains.Feedbacks;

namespace GK.Player
{
    /// <summary>
    /// The behaviour responsible for controlling the dash of the player.
    /// </summary>
    public class Dash : MonoBehaviour, IInputListener
    {
        [Header("Dash settings")]
        [SerializeField] private float _dashSpeed;
        [SerializeField] private float _dashDuration;
        [SerializeField] private ScaledFloat _dashCooldown;
        [SerializeField] private bool _instantCooldownStart;

        //[Header("Feedback")]

        [Header("References")]
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private MMF_Player _feedback;

        private float _dashCooldownTimer;
        private bool _isDashing;

        /// <summary>
        /// Is the player able to dash at the moment.
        /// </summary>
        public bool CanDash
        {
            get { return _dashCooldownTimer <= 0f; }
        }

        /// <summary>
        /// How much time is left before player can dash again.
        /// </summary>
        public float RemainingCooldown
        {
            get { return Mathf.Max(0, _dashCooldownTimer); }
        }

        private void Awake()
        {
            _dashCooldown.Initialize();
        }

        private void Update()
        {
            UpdateTimer();
        }

        private void UpdateTimer()
        {
            // Only reduce cooldown while in a dash if toggled.
            if (!_instantCooldownStart && _isDashing)
                return;
            
            _dashCooldownTimer -= Time.deltaTime;
        }

        public void OnInput(InputData data)
        {
            if (data.Dash && CanDash)
            {
                if (data.MovementDirection == Vector3.zero)
                    return;

                StartCoroutine(DashCoroutine(data.MovementDirection));
            }
        }

        private IEnumerator DashCoroutine(Vector3 direction)
        {
            _isDashing = true;
            _playerMovement.Blocked = true;
            _playerMovement.Rigidbody.velocity = direction * _dashSpeed;

            _feedback.PlayFeedbacks();

            yield return new WaitForSeconds(_dashDuration);

            _isDashing = false;
            _playerMovement.Blocked = false;
            _dashCooldownTimer = _dashCooldown.Value;
        }
    }
}