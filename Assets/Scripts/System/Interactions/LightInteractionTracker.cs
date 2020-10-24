using System.Collections.Generic;
using System.Interfaces;
using System.Linq;
using Models;
using UnityEngine;

namespace System.Interactions {
    [RequireComponent(typeof(CircleCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class LightInteractionTracker : MonoBehaviour, IInteractionTracker {
        [field: SerializeField] public List<InteractionEvent> History { get; private set; }

        [field: SerializeField]
        public List<(Vector3 interactableSnappedPosition, ILightInteractor lightInteractor)>
            NonPersistentGateInteractionsRemoved { get; private set; }

        [field: SerializeField]
        public List<(Vector3 interactableSnappedPosition, ILightInteractor lightInteractor)>
            DividerInteractionsRemoved { get; private set; }

        public GameObject Behaviour => gameObject;

        private void Awake() {
            History = new List<InteractionEvent>();
            NonPersistentGateInteractionsRemoved =
                new List<(Vector3 interactableSnappedPosition, ILightInteractor lightInteractor)>();
            DividerInteractionsRemoved = new List<(Vector3 interactableSnappedPosition, ILightInteractor lightInteractor)>();
            InteractionObserver.OnInteractionEvent += OnInteraction;
            InteractionObserver.OnNonPersistentGateReEnabled += OnGateReEnabled;
            InteractionObserver.OnDividerInteractionRemoved += OnDividerInteractionRemoved;
        }

        private void OnDestroy() {
            History = null;
            InteractionObserver.OnInteractionEvent -= OnInteraction;
            InteractionObserver.OnNonPersistentGateReEnabled -= OnGateReEnabled;
            InteractionObserver.OnDividerInteractionRemoved -= OnDividerInteractionRemoved;
        }

        private void OnDividerInteractionRemoved(Vector3 dividerSnappedPosition, ILightInteractor interactor) {
            if (interactor == GetComponent<ILightInteractor>()) Destroy(gameObject);
        }

        private void OnGateReEnabled(Vector3 gatePos) {
            HandleInteraction(gatePos);
        }

        private void OnTriggerEnter2D(Collider2D other) {
            var provider = other.GetComponent<IInteractionHistoryProvider>();

            if (provider == null) return;

            History.Insert(0, provider.TrackInteraction(this));
        }

        public void OnInteraction(Vector3 position) {
            HandleInteraction(position);
        }

        public void InitializeHistory(List<InteractionEvent> history) {
            // need to copy values, rather than point to memory ref
            this.History = history.ConvertAll(x => x);
        }

        private void HandleInteraction(Vector3 interactionOccurancePosition) {
            void StoreNonPersistentGateInteractions(int i) {
                for (int j = 0; j < i + 1; j++) {
                    if (History[j].Type != typeof(NonPersistentGate)) continue;

                    NonPersistentGateInteractionsRemoved.Add((History[j].InteractableSnappedPosition,
                        Behaviour.GetComponent<ILightInteractor>()));
                    break;
                }
            }
            
            void StoreDividerInteractions(int i) {
                for (int k = 0; k < i + 1; k++) {
                    if (History[k].Type != typeof(LightDivider)) continue;

                    DividerInteractionsRemoved.Add((History[k].InteractableSnappedPosition,
                        Behaviour.GetComponent<ILightInteractor>()));
                    break;
                }
            }

            void UpdateComponentStateToMatchInteraction(int i) {
                // if(History[i].Type == typeof(LightDivider)) Destroy(gameObject);
                
                gameObject.transform.position = History[i].InteractorSnappedPosition;
                // print(history[i].LightColor);
                // if (history[i].LightColor != null)
                //     gameObject.GetComponent<ILightColor>().LightColor = (LightColor) history[i].LightColor;
                gameObject.SetActive(true);
            }

            void TrimOldInteractions(int i) {
                // spawning the object on top of that position will 'regenerate' the extra event we are deleting
                History.RemoveRange(0, i + 1);
            }

            void HandleNonPersistentGateStateUpdates() {
                for (int j = 0; j < NonPersistentGateInteractionsRemoved.Count; j++) {
                    InteractionObserver.OnNonPersistentGateInteractionRemoved(
                        NonPersistentGateInteractionsRemoved[j].interactableSnappedPosition,
                        NonPersistentGateInteractionsRemoved[j].lightInteractor);
                }

                NonPersistentGateInteractionsRemoved.Clear();
            }

            void HandleDividerStateUpdates() {
                for (int k = 0; k < DividerInteractionsRemoved.Count; k++) {
                    InteractionObserver.OnDividerInteractionRemoved(
                        DividerInteractionsRemoved[k].interactableSnappedPosition,
                        DividerInteractionsRemoved[k].lightInteractor);
                }

                DividerInteractionsRemoved.Clear();
            }

            for (int i = 0; i < History.Count; i++) {
                if (History[i].InteractableSnappedPosition.Snapped() != interactionOccurancePosition.Snapped()) continue;

                StoreNonPersistentGateInteractions(i);
                StoreDividerInteractions(i);

                UpdateComponentStateToMatchInteraction(i);

                TrimOldInteractions(i);

                if (NonPersistentGateInteractionsRemoved.Count > 0) HandleNonPersistentGateStateUpdates();
                if (DividerInteractionsRemoved.Count > 0) HandleDividerStateUpdates();
            }
        }

        public void HandleDeactivation(LightDivider lightDivider) {
            Debug.Assert(History[0].Type == lightDivider.GetType());
            History.RemoveAt(0);
        }
    }
}