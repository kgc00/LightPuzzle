using System.Collections;
using System.Collections.Generic;
using System.Interfaces;
using LightPuzzleUtils;
using Models;
using UnityEditor;
using UnityEngine;

namespace System.Interactions {
    public class LightCollision : MonoBehaviour, ILightInteractable, IInteractionHistoryProvider {
        public IEnumerator HandleInteraction(ILightInteractor interactor) {
            interactor.Behaviour.SetActive(false);
            yield break;
        }


        public InteractionEvent TrackInteraction(IInteractionTracker tracker) =>
            new InteractionEvent(Helpers.SnappedCollisionPosFromInteractorPos(tracker.Behaviour.transform),
                tracker.Behaviour.transform.eulerAngles,
                transform.position.Snapped(),
                name,
                GetType());
    }
}