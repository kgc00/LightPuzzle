using System;
using TypeReferences;
using UnityEngine;

namespace Models {
    [Serializable]
    public readonly struct InteractionEvent {
        public readonly Vector3 SnappedPosition;
        public readonly Vector3 EulerRotation;
        public readonly string Name;

        public readonly Type Type;
        // can use typeref to visualize in inspector

        public InteractionEvent(Transform transform, Type interactionType) {
            SnappedPosition = transform.position.Snapped();
            EulerRotation = transform.rotation.eulerAngles;
            Name = transform.name;
            Type = interactionType;
        }
    }
}