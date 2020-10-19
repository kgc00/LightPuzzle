using System.Collections.Generic;
using System.Interactions;
using System.Interfaces;
using Models;
using UnityEngine;

namespace System {
    [RequireComponent(typeof(CircleCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class LightInteractionTracker : MonoBehaviour, IInteractionTracker {
        // easy way to visualize interactions wthout dealing with unity serialization nonsense
        // not anymore... can't serialize readonly structs, apparently
        [SerializeField] private List<InteractionEvent> history;

        public List<InteractionEvent> History {
            get => history;
            private set => history = value;
        }

        public GameObject Behaviour => gameObject;

        private void Awake() {
            History = new List<InteractionEvent>();
            InteractionObserver.OnInteractionEvent += OnInteraction;
            InteractionObserver.OnNonPersistentGateReEnabled += OnGateReEnabled;
        }

        private void OnDestroy() {
            history = null;
            InteractionObserver.OnInteractionEvent -= OnInteraction;
            InteractionObserver.OnNonPersistentGateReEnabled -= OnGateReEnabled;
        }

        private void OnGateReEnabled(Vector3 gatePos) {
            RevertFurthestStateToGateInteraction(gatePos);
        }

        private void OnTriggerEnter2D(Collider2D other) {
            var provider = other.GetComponent<IInteractionHistoryProvider>();

            if (provider == null) return;

            History.Insert(0, provider.TrackInteraction(this));
        }

        public void OnInteraction(Vector3 position) {
            RevertFurthestStateToGateInteraction(position);
        }

        private void RevertFurthestStateToGateInteraction(Vector3 position) {
            for (int i = 0; i < history.Count; i++) {
                if (history[i].SnappedPosition.Snapped() != position.Snapped()) continue;

                gameObject.transform.SetPositionAndRotation(history[i].SnappedPosition,
                    Quaternion.Euler(history[i].EulerRotation));
                gameObject.SetActive(true);

                for (int j = 0; j < i + 1; j++) {
                    if (history[j].Type != typeof(NonPersistentGate)) continue;

                    InteractionObserver.OnNonPersistentGateInteractionRemoved(history[j].SnappedPosition,
                        Behaviour.GetComponent<ILightInteractor>());
                    // spawning the object on top of that position will 'regenerate' the extra event we are deleting
                    history.RemoveRange(0, i + 1);
                    break;
                }
            }
        }
    }
}