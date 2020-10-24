using System.Collections;
using System.Collections.Generic;
using System.Interfaces;
using LightPuzzleUtils;
using Models;
using UnityEngine;

namespace System.Interactions {
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class NonPersistentGate : MonoBehaviour, ILightInteractable, IInteractionHistoryProvider {
        private List<ILightInteractor> currentInteractors;

        private void Awake() {
            InteractionObserver.OnNonPersistentGateInteractionRemoved += HandleInteractionRemoved;
            currentInteractors = new List<ILightInteractor>();
        }

        private void OnDestroy() {
            InteractionObserver.OnNonPersistentGateInteractionRemoved -= HandleInteractionRemoved;
            currentInteractors = null;
        }

        private void HandleInteractionRemoved(Vector3 position, ILightInteractor interactor) {
            if (position.Snapped() != transform.position.Snapped()) return;

            if (currentInteractors.Contains(interactor)) {
                currentInteractors.Remove(interactor);
                GetMovement(interactor).enabled = true;
            }
            if (IsUnlocked()) return;
            ReEnableGate();
        }

        private void ReEnableGate() {
            gameObject.SetActive(true);
            // TODO - have any light which passed through the gate return to it
            // use event system to trigger all lights to search for this interaction
            InteractionObserver.OnNonPersistentGateReEnabled(transform.position.Snapped());
        }

        // when a light's history is updated, it needs to search discarded interaction...
        // if type -- nonpersistentgate, fire off event to remove light from currentInteractors

        public IEnumerator HandleInteraction(ILightInteractor interactor) {
            while (Vector3.Distance(interactor.Behaviour.transform.position, transform.position) > .05f) {
                yield return new WaitForEndOfFrame();
            }

            if (!gameObject.activeSelf) yield break;

            if (!currentInteractors.Contains(interactor)) currentInteractors.Add(interactor);

            if (!IsUnlocked()) {
                GetMovement(interactor).enabled = false;
                yield break;
            }

            DisableGate();
        }

        private void DisableGate() {
            foreach (var lightInteractor in currentInteractors) {
                GetMovement(lightInteractor).enabled = true;
            }

            gameObject.SetActive(false);
        }

        private LightMovement GetMovement(ILightInteractor interactor) =>
            interactor.Behaviour.GetComponent<LightMovement>();

        private bool IsUnlocked() => currentInteractors.Count > 1;

        public InteractionEvent TrackInteraction(IInteractionTracker tracker) =>
            new InteractionEvent(Helpers.SnappedCollisionPosFromInteractorPos(tracker.Behaviour.transform),
                tracker.Behaviour.transform.eulerAngles,
                transform.position.Snapped(),
                name,
                GetType());
    }
}