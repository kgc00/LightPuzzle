using System.Collections;
using System.Collections.Generic;
using System.Interfaces;
using LightPuzzleUtils;
using Models;
using UnityEngine;

namespace System.Interactions {
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PersistentGate : MonoBehaviour, ILightInteractable, IInteractionHistoryProvider {
        private List<ILightInteractor> interactors;
        private BoxCollider2D boxCollider2D;

        private void Awake() {
            boxCollider2D = GetComponent<BoxCollider2D>();
        }

        private void OnEnable() {
            interactors = new List<ILightInteractor>();
        }

        private void OnDisable() {
            interactors = null;
        }

        private void OnTriggerExit2D(Collider2D other) {
            if(!gameObject.activeSelf) return;

            var lightInteractor = other.GetComponent<ILightInteractor>();
            if (interactors.Contains(lightInteractor)) {
                lightInteractor.Behaviour.GetComponent<LightMovement>().enabled = true;
                interactors.Remove(lightInteractor);
            }
        }

        public IEnumerator HandleInteraction(ILightInteractor interactor) {
            var collisionPos = Helpers.GetMultiCellSnappedCollisionPos(interactor.Behaviour.transform, boxCollider2D);

            while (Vector3.Distance(interactor.Behaviour.transform.position, collisionPos) > .05f) {
                yield return new WaitForEndOfFrame();
            }
            
            if (!gameObject.activeSelf) yield break;
            
            if (!interactors.Contains(interactor)) interactors.Add(interactor);
            
            if (!HasBeenUnlockd()) {
                GetMovement(interactor).enabled = false;
                yield break;
            }

            foreach (var lightInteractor in interactors) {
                GetMovement(lightInteractor).enabled = true;
            }
            
            gameObject.SetActive(false);
        }

        private LightMovement GetMovement(ILightInteractor interactor) => 
            interactor.Behaviour.GetComponent<LightMovement>();

        private bool HasBeenUnlockd() => interactors.Count > 1;

        public InteractionEvent TrackInteraction(IInteractionTracker tracker) =>
            new InteractionEvent(Helpers.SnappedCollisionPosFromInteractorPos(tracker.Behaviour.transform),
                tracker.Behaviour.transform.eulerAngles,
                transform.position.Snapped(),
                name,
                GetType());
    }
}