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
    public abstract class StateMonoBehaviour : MonoBehaviour
    {
        /// <summary>
        /// The current state of this state machine.
        /// </summary>
        protected State CurrentState { get; private set; }

        private List<State> _states = new List<State>();
        private Dictionary<string, int> _stateNameToIndex = new Dictionary<string, int>();

        /// <summary>
        /// Gets a state from the state machine by name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public State GetStateFromName(string name)
        {
            if (!_stateNameToIndex.ContainsKey(name))
                return null;

            return _states[_stateNameToIndex[name]];
        }

        #region State methods

        /// <summary>
        /// Adds a state to the state machine.
        /// </summary>
        /// <param name="state"></param>
        protected void AddState(State state)
        {
            _states.Add(state);

            if (CurrentState == null)
                SwitchState(state);
        }

        /// <summary>
        /// Switches the active state to the provided one. Will call
        /// OnStateEnter on state.
        /// </summary>
        /// <param name="state"></param>
        protected void SwitchState(State state)
        {
            if (CurrentState != null)
                CurrentState.OnStateExit(this);
            
            CurrentState = state;
            
            state.OnStateEnter(this);
        }

        protected virtual void SetupStates()
        {
            
        }

        #endregion

        #region Lifecycle methods

        private void Update()
        {
            CurrentState.OnStateUpdate(this);
            OnUpdate();
        }

        private void Awake()
        {
            SetupStates();
            foreach (State state in _states)
            {
                _stateNameToIndex.Add(state.GetType().Name, _states.IndexOf(state));
            }
            OnAwake();            
        }

        #endregion

        #region Lifecycle Overrides

        protected virtual void OnUpdate()
        {
            
        }

        protected virtual void OnAwake()
        {
            
        }

        #endregion

    }
}