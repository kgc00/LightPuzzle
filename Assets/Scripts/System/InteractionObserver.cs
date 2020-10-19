using System.Interfaces;
using UnityEngine;

namespace System {
    public static class InteractionObserver {
        public static Action<Vector3> OnInteractionEvent = delegate(Vector3 vector3) {  };
        public static Action<Vector3, ILightInteractor> OnNonPersistentGateInteractionRemoved = delegate(Vector3 vector3, ILightInteractor interactor) {  };
        public static Action<Vector3> OnNonPersistentGateReEnabled = delegate(Vector3 vector3) {  };
    }
}