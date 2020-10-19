using UnityEngine;

namespace System {
    public static class InteractionObserver {
        public static Action<Vector3> OnInteractionEvent = delegate(Vector3 vector3) {  };
    }
}