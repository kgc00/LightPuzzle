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
        private BoxCollider2D boxCollider2D;
        private Vector3 collisionPos = Vector3.zero;
        private Vector3 interactorPos = Vector3.zero;

        private void Awake() {
            InteractionObserver.OnNonPersistentGateInteractionRemoved += HandleInteractionRemoved;
            currentInteractors = new List<ILightInteractor>();
            boxCollider2D = GetComponent<BoxCollider2D>();
        }

        private void OnDestroy() {
            InteractionObserver.OnNonPersistentGateInteractionRemoved -= HandleInteractionRemoved;
            currentInteractors = null;
        }

        private void HandleInteractionRemoved(Vector3 interactableSnappedPos, ILightInteractor interactor) {
            if (interactableSnappedPos.Snapped() != transform.position.Snapped()) return;

            if (currentInteractors.Contains(interactor)) {
                currentInteractors.Remove(interactor);
                interactor.HandleUnblockedInteraction();
            }

            if (IsUnlocked()) return;
            ReEnableGate();
        }

        private void ReEnableGate() {
            gameObject.SetActive(true);
            InteractionObserver.OnNonPersistentGateReEnabled(transform.position.Snapped());
        }

        // when a light's history is updated, it needs to search discarded interaction...
        // if type -- nonpersistentgate, fire off event to remove light from currentInteractors
        public IEnumerator HandleInteraction(ILightInteractor interactor) {
            interactorPos = interactor.Behaviour.transform.position.Snapped();
            collisionPos = Helpers.GetMultiCellSnappedCollisionPos(interactor.Behaviour.transform, boxCollider2D);

            while (Vector3.Distance(interactor.Behaviour.transform.position, collisionPos) > .05f) {
                yield return new WaitForEndOfFrame();
            }

            if (!gameObject.activeSelf) yield break;

            if (!currentInteractors.Contains(interactor)) currentInteractors.Add(interactor);

            if (!IsUnlocked()) {
                interactor.HandleBlockedInteraction();
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

        public InteractionEvent TrackInteraction(IInteractionTracker tracker) {
            if (boxCollider2D.bounds.size.magnitude > Vector3.one.magnitude) {
                return new InteractionEvent(
                    Helpers.GetMultiCellSnappedCollisionPos(tracker.Behaviour.transform, boxCollider2D),
                    tracker.Behaviour.transform.eulerAngles,
                    transform.position.Snapped(),
                    name,
                    GetType());
            }else {
                return new InteractionEvent(
                    transform,
                    GetType());
            }
            
        }

        private void OnDrawGizmos() {
            Gizmos.DrawSphere(collisionPos, 0.25f);
            Gizmos.DrawWireSphere(interactorPos, 0.25f);
        }
    }
}