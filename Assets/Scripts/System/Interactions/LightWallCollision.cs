using System.Collections;
using System.Collections.Generic;
using System.Interfaces;
using LightPuzzleUtils;
using Models;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace System.Interactions {
    [RequireComponent(typeof(TilemapCollider2D))]
    public class LightWallCollision : MonoBehaviour, ILightInteractable, IInteractionHistoryProvider {
        private TilemapCollider2D tilemapCollider2D;
        private void Awake() {
            tilemapCollider2D = GetComponent<TilemapCollider2D>();
        }

        public IEnumerator HandleInteraction(ILightInteractor interactor) {
            var collisionPos = Helpers.SnappedCollisionPosFromInteractorPos(interactor.Behaviour.transform);

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