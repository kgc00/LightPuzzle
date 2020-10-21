using System.Collections;
using System.Interfaces;
using Models;
using UnityEngine;

namespace System.Interactions {
    public class LightCollision : MonoBehaviour, ILightInteractable, IInteractionHistoryProvider {
        public IEnumerator HandleInteraction(ILightInteractor interactor) {
            interactor.Behaviour.SetActive(false);
            yield break;
        }

        public InteractionEvent TrackInteraction(IInteractionTracker tracker) {
            return new InteractionEvent(tracker.Behaviour.transform, GetType());
        }
    }
}