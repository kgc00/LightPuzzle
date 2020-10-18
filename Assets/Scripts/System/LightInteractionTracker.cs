using System.Collections.Generic;
using System.Interfaces;
using Models;
using UnityEngine;

namespace System {
    [RequireComponent(typeof(CircleCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class LightInteractionTracker : MonoBehaviour, IInteractionTracker {
        // easy way to visualize interactions wthout dealing with unity serialization nonsense
        [SerializeField] private List<InteractionEvent> history;

        public List<InteractionEvent> History {
            get => history;
            private set => history = value;
        }

        private void OnEnable() {
            History = new List<InteractionEvent>();
        }

        private void OnDisable() {
            History = null;
        }

        private void OnTriggerEnter2D(Collider2D other) {
            var provider = other.GetComponent<IInteractionHistoryProvider>();

            if (provider == null) return;

            History.Add(provider.TrackInteraction(this));
        }

        public GameObject Behaviour => gameObject;
    }
}