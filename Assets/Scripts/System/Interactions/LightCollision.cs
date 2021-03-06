﻿using System.Collections;
using System.Collections.Generic;
using System.Interfaces;
using LightPuzzleUtils;
using Models;
using UnityEditor;
using UnityEngine;

namespace System.Interactions {
    [RequireComponent(typeof(BoxCollider2D))]
    public class LightCollision : MonoBehaviour, ILightInteractable, IInteractionHistoryProvider {
        private BoxCollider2D boxCollider2D;
        private void Awake() {
            boxCollider2D = GetComponent<BoxCollider2D>();
        }

        public IEnumerator HandleInteraction(ILightInteractor interactor) {
            var collisionPos = Helpers.GetMultiCellSnappedCollisionPos(interactor.Behaviour.transform, boxCollider2D);

            while (Vector3.Distance(interactor.Behaviour.transform.position, collisionPos) > .05f) {
                yield return new WaitForEndOfFrame();
            }
            interactor.HandleBlockedInteraction();
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