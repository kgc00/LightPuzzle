using System;
using TypeReferences;
using UnityEngine;

namespace Models {
    [Serializable]
    public struct InteractionEvent {
        public Vector3 position;
        public Vector3 eulerRotation;
        public string name;
        public Type type;
        // can use typeref to visualize in inspector
    }
}