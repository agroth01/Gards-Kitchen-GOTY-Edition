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
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour, IInputListener
    {
        [Header("Movement settings")]
        [SerializeField] private ScaledFloat _speed;

        private CharacterController _controller;

        /// <summary>
        /// Block the player from being able to use inputs to move.
        /// </summary>
        public bool InputBlocked { get; set; }

        public Vector3 Velocity { get; set; }

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
            _speed.Initialize();
        }

        private void Update()
        {
            Rotation();
            Movement();
        }

        public void OnInput(InputData data)
        {
            if (!InputBlocked)
            {
                Velocity = data.MovementDirection * _speed.Value;
            }
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

        private void Movement()
        {
            Vector3 move = Velocity * Time.deltaTime;
            _controller.Move(move);
        }
    }
}
