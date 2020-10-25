using System.Collections;
using System.Interfaces;
using Models;
using UnityEngine;

namespace System.Interactions {
    [RequireComponent(typeof(RotationInteraction))]
    public class MirrorSurface : MonoBehaviour, ILightInteractable, IInteractionHistoryProvider {
        private RotationInteraction rotationInteraction;
        private ILightInteractor currentInteractor;

        private void Awake() {
            InteractionObserver.OnInteractionEvent += InterruptCurrentInteraction;
            rotationInteraction = GetComponent<RotationInteraction>();
        }

        private void OnDestroy() {
            InteractionObserver.OnInteractionEvent -= InterruptCurrentInteraction;
        }

        private void InterruptCurrentInteraction(Vector3 interactableSnappedPos) {
            if (interactableSnappedPos != transform.position.Snapped()) return;
            print("Handling thing");
            if (currentInteractor != null) {
                print("IN CRx");
                currentInteractor.StopAllCR();
                this.StopAllCoroutines();
                currentInteractor.HandleBlockedInteraction();
            }
        }

        public InteractionEvent TrackInteraction(IInteractionTracker tracker) {
            return new InteractionEvent(transform, GetType());
        }

        public IEnumerator HandleInteraction(ILightInteractor interactor) {
            print("MIRROR SURF INT");
            currentInteractor = interactor;
            while (Vector3.Distance(interactor.Behaviour.transform.position, transform.position) > .05f) {
                yield return new WaitForEndOfFrame();
            }

            // stop movement if mirror is currently rotating
            interactor.Behaviour.GetComponent<LightMovement>().enabled = false;
            while (rotationInteraction.Rotating) {
                yield return new WaitForEndOfFrame();
            }

            interactor.Behaviour.transform.SetPositionAndRotation(transform.position.Snapped(), transform.rotation);
            interactor.HandleUnblockedInteraction();
            interactor.HandleReactivation();
            currentInteractor = null;
        }
    }
}