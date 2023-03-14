using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GK.Core
{
    /// <summary>
    /// A simple class to represent a random range between two floats. This is mainly used for 
    /// a more clean inspector.
    /// </summary>
    [System.Serializable]
    public class RandomRange
    {
        [SerializeField] private float _minValue;
        [SerializeField] private float _maxValue;

        /// <summary>
        /// Returns a random value between the minimum and maximum value.
        /// </summary>
        /// <returns></returns>
        public float GetValue()
        {
            return Random.Range(_minValue, _maxValue);
        }
    }
}
