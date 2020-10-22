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
            NonPersistentGateInteractionsRemoved = new List<(Vector3, ILightInteractor)>();
            InteractionObserver.OnInteractionEvent += OnInteraction;
            InteractionObserver.OnNonPersistentGateReEnabled += OnGateReEnabled;
        }

        private void OnDestroy() {
            history = null;
            InteractionObserver.OnInteractionEvent -= OnInteraction;
            InteractionObserver.OnNonPersistentGateReEnabled -= OnGateReEnabled;
        }

        private void OnGateReEnabled(Vector3 gatePos) {
            HandleInteraction(gatePos);
        }

        private void OnTriggerEnter2D(Collider2D other) {
            var provider = other.GetComponent<IInteractionHistoryProvider>();

            if (provider == null) return;

            History.Insert(0, provider.TrackInteraction(this));
        }

        private List<(Vector3, ILightInteractor)> NonPersistentGateInteractionsRemoved;
        public void OnInteraction(Vector3 position) {
            HandleInteraction(position);
        }

        private void HandleInteraction(Vector3 position) {
            void StoreNonPersistentGateInteractions(int i) {
                for (int j = 0; j < i + 1; j++) {
                    if (history[j].Type != typeof(NonPersistentGate)) continue;

                    NonPersistentGateInteractionsRemoved.Add((history[j].SnappedPosition, Behaviour.GetComponent<ILightInteractor>()));
                    break;
                }
            }

            void UpdateComponentStateToMatchInteraction(int i) {
                gameObject.transform.position = history[i].SnappedPosition;
                gameObject.SetActive(true);
            }

            void TrimOldInteractions(int i) {
                // spawning the object on top of that position will 'regenerate' the extra event we are deleting
                history.RemoveRange(0, i + 1);
            }

            void TriggerNonPersistentGateStateUpdates() {
                for (int j = 0; j < NonPersistentGateInteractionsRemoved.Count; j++) {
                    InteractionObserver.OnNonPersistentGateInteractionRemoved(NonPersistentGateInteractionsRemoved[j].Item1,
                        NonPersistentGateInteractionsRemoved[j].Item2);
                }

                NonPersistentGateInteractionsRemoved.Clear();
            }

            for (int i = 0; i < history.Count; i++) {
                if (history[i].SnappedPosition.Snapped() != position.Snapped()) continue;

                StoreNonPersistentGateInteractions(i);

                UpdateComponentStateToMatchInteraction(i);

                TrimOldInteractions(i);

                if (NonPersistentGateInteractionsRemoved.Count > 0) TriggerNonPersistentGateStateUpdates();
            }
        }
    }
}