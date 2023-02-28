using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GK.Core
{
    /// <summary>
    /// A single state used for a state machine.
    /// </summary>
    public abstract class State
    {
        public abstract void OnStateEnter(StateMonoBehaviour stateMonoBehaviour);

        public abstract void OnStateExit(StateMonoBehaviour stateMonoBehaviour);

        public abstract void OnStateUpdate(StateMonoBehaviour stateMonoBehaviour);
    }
}