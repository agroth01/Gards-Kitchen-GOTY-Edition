using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GK.Input
{
    /// <summary>
    /// Monobehaviour that listens for input and sends it to all IInputListener scripts
    /// on the same GameObject.
    /// </summary>
    public class InputListener : MonoBehaviour
    {
        private List<IInputListener> _listeners = new List<IInputListener>();

        private void Awake()
        {
            // Get all IInputListener scripts on this GameObject
            _listeners.AddRange(GetComponents<IInputListener>());
        }

        private void Update()
        {
            InputData data = new InputData();
            data.MovementDirection = new Vector3(UnityEngine.Input.GetAxisRaw("Horizontal"), 0, UnityEngine.Input.GetAxisRaw("Vertical")).normalized;
            data.Fire = UnityEngine.Input.GetKey(KeyCode.Mouse0);
            data.Dash = UnityEngine.Input.GetKey(KeyCode.LeftShift);

            NotifyListeners(data);
        }

        private void NotifyListeners(InputData data)
        {
            _listeners.ForEach(l => l.OnInput(data));
        }
    }
}