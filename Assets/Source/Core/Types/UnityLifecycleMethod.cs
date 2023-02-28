using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GK.Core
{
    /// <summary>
    /// The different lifecycle methods that is provided by Unity's MonoBehaviour class. This is used for
    /// spawning particles.
    /// </summary>
    public enum UnityLifecycleMethod
    {
        Awake,
        Start,
        OnEnable,
        OnDisable,
        Update,
        FixedUpdate,
        OnDestroy
    }
}
