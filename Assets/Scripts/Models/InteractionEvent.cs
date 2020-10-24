using System;
using UnityEngine;

namespace Models {
    [Serializable]
    public struct InteractionEvent {
        [SerializeField] private Vector3 interactorEulerRotation;
        [SerializeField] private Vector3 interactorSnappedPosition;
        [SerializeField] private Vector3 interactableSnappedPosition;
        [SerializeField] private string name;
        [SerializeField] private Type type;
        [SerializeField] private LightColor? lightColor;
        public Vector3 InteractableSnappedPosition => interactableSnappedPosition;
        public Vector3 InteractorSnappedPosition => interactorSnappedPosition;
        public Vector3 InteractorEulerRotation => interactorEulerRotation;        
        public string Name => name;        
        // can use typeref to visualize in inspector
        public Type Type => type;
        public LightColor? LightColor => lightColor;
        public InteractionEvent(Transform transform, Type interactionType, LightColor? newColor = null) {
            interactorSnappedPosition = transform.position.Snapped();
            interactableSnappedPosition = transform.position.Snapped();
            interactorEulerRotation = transform.rotation.eulerAngles;
            name = transform.name;
            type = interactionType;
            lightColor = newColor;
        }
        public InteractionEvent(Transform transform, Transform interactableTransform, Type interactionType, LightColor? newColor = null) {
            interactorSnappedPosition = transform.position.Snapped();
            interactableSnappedPosition = interactableTransform.position.Snapped();
            interactorEulerRotation = transform.rotation.eulerAngles;
            name = transform.name;
            type = interactionType;
            lightColor = newColor;
        }
        
        public InteractionEvent(Vector3 snappedPosition, Vector3 interactorEulerRotation, Vector3 interactableSnappedPosition, string name, Type interactionType, LightColor? newColor = null) {
            this.interactorSnappedPosition = snappedPosition.Snapped();
            this.interactableSnappedPosition = interactableSnappedPosition.Snapped();
            this.interactorEulerRotation = interactorEulerRotation;
            this.name = name;
            this.type = interactionType;
            this.lightColor = newColor;
        }
    }
}