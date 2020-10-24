using System.Collections;
using System.Interfaces;
using Models;
using UnityEngine;

namespace System.Interactions {
    public class MirrorSurface : MonoBehaviour, ILightInteractable, IInteractionHistoryProvider {
        public InteractionEvent TrackInteraction(IInteractionTracker tracker) {
            return new InteractionEvent(transform, GetType());
        }

        public IEnumerator HandleInteraction(ILightInteractor interactor) {
            while (Vector3.Distance(interactor.Behaviour.transform.position, transform.position) > .05f) {
                yield return new WaitForEndOfFrame();
            }

            interactor.Behaviour.transform.SetPositionAndRotation(transform.position.Snapped(), transform.rotation);
            interactor.HandleUnblockedInteraction();
        }
    }
}