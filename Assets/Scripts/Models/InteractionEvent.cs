using System;
using TypeReferences;
using UnityEngine;

namespace Models {
    [Serializable]
    public  struct InteractionEvent {
        public  Vector3 SnappedPosition;
        public  Vector3 EulerRotation;
        public  string Name;

        // can use typeref to visualize in inspector
        public  Type Type;

        public LightColor? LightColor;

        public InteractionEvent(Transform transform, Type interactionType, LightColor? newColor = null) {
            SnappedPosition = transform.position.Snapped();
            EulerRotation = transform.rotation.eulerAngles;
            Name = transform.name;
            Type = interactionType;
            LightColor = newColor;
        }
    }
}