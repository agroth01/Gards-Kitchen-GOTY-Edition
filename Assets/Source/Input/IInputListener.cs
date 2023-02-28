using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GK.Input
{
    /// <summary>
    /// Interface that is used by all classes that wants to receive input from an input listener.
    /// </summary>
    public interface IInputListener
    {
        public void OnInput(InputData data);
    }

}