using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GK.Core
{
    /// <summary>
    /// Base class for all state based monobehaviours.
    /// Instead of using the MonoBehaviour's Update method, OnUpdate should instead be
    /// overwritten. Alternatively, call base update in the overwritten method if overwriting
    /// Update().
    /// </summary>
    public class StateMonoBehaviour : MonoBehaviour
    {
        /// <summary>
        /// The current state of this state machine.
        /// </summary>
        protected State CurrentState { get; private set; }

        private List<State> _states = new List<State>();

        #region State methods

        /// <summary>
        /// Adds a state to the state machine.
        /// </summary>
        /// <param name="state"></param>
        protected void AddState(State state)
        {
            _states.Add(state);
        }

        /// <summary>
        /// Switches the active state to the provided one. Will call
        /// OnStateEnter on state.
        /// </summary>
        /// <param name="state"></param>
        protected void SwitchState(State state)
        {
            CurrentState.OnStateExit(this);
            CurrentState = state;
            
            state.OnStateEnter(this);
        }

        #endregion

        #region Lifecycle methods

        public void Update()
        {
            CurrentState.OnStateUpdate(this);
            OnUpdate();
        }

        #endregion


        protected virtual void OnUpdate()
        {
            
        }
    }
}