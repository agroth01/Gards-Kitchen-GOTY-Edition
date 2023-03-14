using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GK.Boss
{
    /// <summary>
    /// The class that represents a phase in a boss. This is where the logic of each phase is 
    /// handles, as well as condition for ending the phase and transitioning to the next one.
    /// </summary>
    public abstract class Phase
    {
        public Boss Boss { get; set; }
        
        public abstract bool ShouldEnd();

        public abstract void UpdatePhase();
    }
}
