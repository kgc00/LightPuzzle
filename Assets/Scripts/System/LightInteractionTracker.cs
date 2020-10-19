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

        public GameObject Behaviour => gameObject;

        private void Awake() {
            History = new List<InteractionEvent>();
            InteractionObserver.OnInteractionEvent += OnInteraction;
        }

        private void OnDestroy() {
            history = null;
            InteractionObserver.OnInteractionEvent -= OnInteraction;
        }

        private void OnTriggerEnter2D(Collider2D other) {
            var provider = other.GetComponent<IInteractionHistoryProvider>();

            if (provider == null) return;

            History.Insert(0, provider.TrackInteraction(this));
        }

        public void OnInteraction(Vector3 position) {
            print("Called");
            for (int i = 0; i < history.Count; i++) {
                if (history[i].position.Snapped() != position.Snapped()) continue;

                print("Found Interaction");

                gameObject.transform.SetPositionAndRotation(history[i].position,
                    Quaternion.Euler(history[i].eulerRotation));
                gameObject.SetActive(true);

                history.RemoveRange(0, i);
                break;
            }
        }
    }
}