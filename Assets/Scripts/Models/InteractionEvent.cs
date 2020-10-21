using System;
using TypeReferences;
using UnityEngine;

namespace Models {
    [Serializable]
    public  struct InteractionEvent {
        public  Vector3 SnappedPosition;
        public  Vector3 EulerRotation;
        public  string Name;

        public  Type Type;
        // can use typeref to visualize in inspector

        public InteractionEvent(Transform transform, Type interactionType) {
            SnappedPosition = transform.position.Snapped();
            EulerRotation = transform.rotation.eulerAngles;
            Name = transform.name;
            Type = interactionType;
        }
    }
}