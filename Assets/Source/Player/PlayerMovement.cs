using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GK.Core;
using GK.Input;

namespace GK.Player
{
    /// <summary>
    /// The script responsible for moving the player.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour, IInputListener
    {
        [Header("Movement settings")]
        [SerializeField] private ScaledFloat _speed;

        private Vector3 _movementDirection;
        private Rigidbody _rididbody;

        /// <summary>
        /// If true, player movement will not be updated.
        /// </summary>
        public bool Blocked { get; set; }

        public Rigidbody Rigidbody
        {
            get { return _rididbody; }
        }

        private void Awake()
        {          
            InitializeRigidbody();
            _speed.Initialize();
        }

        private void Update()
        {
            Rotation();
        }

        public void OnInput(InputData data)
        {
            _movementDirection = data.MovementDirection;
        }

        private void FixedUpdate()
        {
            if (Blocked)
                return;

            Vector3 v = _movementDirection * _speed.Value;
            _rididbody.velocity = v;
        }

        /// <summary>
        /// Every frame the player will orient itself towards the mouse position.
        /// </summary>
        private void Rotation()
        {
            Vector3 mousePosition = MouseUtils.GetMousePosition();
            Vector3 direction = transform.position.DirectionTo(mousePosition).WithY(0);
            transform.forward = direction;
        }

        /// <summary>
        /// Sets the needed settings for the rigidbody to work properly.
        /// </summary>
        private void InitializeRigidbody()
        {
            _rididbody = GetComponent<Rigidbody>();

            _rididbody.useGravity = false;
            _rididbody.interpolation = RigidbodyInterpolation.Interpolate;
            _rididbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
            _rididbody.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }
}
