using System.Collections.Generic;
using Models;
using UnityEngine;

namespace System.Interfaces {
    public interface IInteractionTracker {
        List<InteractionEvent> History { get; }
        GameObject Behaviour { get; }

        void OnInteraction(Vector3 position);
    }
}