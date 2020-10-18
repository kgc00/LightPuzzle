using System.Collections;
using System.Collections.Generic;
using System.Interfaces;
using Models;
using UnityEngine;

namespace System.Interactions {
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PersistentGate : MonoBehaviour, ILightInteractable, IInteractionHistoryProvider {
        private void OnEnable() {
            interactors = new List<ILightInteractor>();
        }

        private void OnDisable() {
            interactors = null;
        }

        public IEnumerator HandleInteraction(ILightInteractor interactor) {
            while (Vector3.Distance(interactor.Behaviour.transform.position, transform.position) > .05f) {
                yield return new WaitForEndOfFrame();
            }
            
            if(interactors == null) yield break;
            
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

        private List<ILightInteractor> interactors;

        private bool HasBeenUnlockd() {
            return interactors.Count > 1;
        }

        public InteractionEvent TrackInteraction(IInteractionTracker tracker) {
            return new InteractionEvent {
                position = transform.position,
                eulerRotation = transform.eulerAngles,
                name = gameObject.name,
                type = typeof(PersistentGate)
            };
        }
    }
}