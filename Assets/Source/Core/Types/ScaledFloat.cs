using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GK.Core
{
    /// <summary>
    /// A float that consists of a base, an additive multiplier as well as an 
    /// multiplicitive multiplier. The final value is calculated as follows:
    /// 
    /// (Base + Additive) * Multiplicative.
    /// 
    /// This is used for scaling values, such as damage, health, movement speed, etc.
    /// </summary>
    [System.Serializable]
    public class ScaledFloat
    {
        [SerializeField]
        private float _baseValue;
        private float _additiveMultiplier = 1f;
        private float _multiplicativeMultiplier = 1f;

        /// <summary>
        /// The real value of the float, accounting for scaling.
        /// </summary>
        public float Value
        {
            get { return (_baseValue * _additiveMultiplier) * _multiplicativeMultiplier; }
        }

        public ScaledFloat(float baseValue)
        {
            _baseValue = baseValue;
            Initialize();
        }

        public void Initialize()
        {
            _additiveMultiplier = 1f;
            _multiplicativeMultiplier = 1f;
        }

        /// <summary>
        /// Adds to the float in the specified way.
        /// </summary>
        /// <param name="amount">The amount to add.</param>
        /// <param name="addingType">What part of the float to target.</param>
        public void Add(float amount, ScaledFloatAddingType addingType)
        {
            switch(addingType)
            {
                case ScaledFloatAddingType.Additive:
                    _additiveMultiplier += amount;
                    break;
                case ScaledFloatAddingType.Multiplicative:
                    _multiplicativeMultiplier += amount;
                    break;
                case ScaledFloatAddingType.Fixed:
                    _baseValue += amount;
                    break;
            }
        }

        /// <summary>
        /// Removes from the float in a specified way.
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="addingType"></param>
        public void Subtract(float amount, ScaledFloatAddingType addingType)
        {
            switch (addingType)
            {
                case ScaledFloatAddingType.Additive:
                    _additiveMultiplier -= amount;
                    break;
                case ScaledFloatAddingType.Multiplicative:
                    _multiplicativeMultiplier -= amount;
                    break;
                case ScaledFloatAddingType.Fixed:
                    _baseValue -= amount;
                    break;
            }
        }

        /// <summary>
        /// Sets the value directly instead of adding or subtracting.
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="addingType"></param>
        public void Set(float amount, ScaledFloatAddingType addingType)
        {
            switch (addingType)
            {
                case ScaledFloatAddingType.Additive:
                    _additiveMultiplier = amount;
                    break;
                case ScaledFloatAddingType.Multiplicative:
                    _multiplicativeMultiplier = amount;
                    break;
                case ScaledFloatAddingType.Fixed:
                    _baseValue = amount;
                    break;
            }
        }
    }

    /// <summary>
    /// The different types of ways that a scaled float can be added to.
    /// </summary>
    public enum ScaledFloatAddingType
    {
        Additive,
        Multiplicative,
        Fixed
    }
}


