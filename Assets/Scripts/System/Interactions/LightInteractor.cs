using System.Interfaces;
using Models;
using Tools;
using UnityEngine;

namespace System.Interactions {
    [RequireComponent(typeof(CircleCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(LightInteractionTracker))]
    [RequireComponent(typeof(LightMovement))]
    public class LightInteractor : MonoBehaviour, ILightInteractor {
        public GameObject Behaviour { get; private set; }
        private void Awake() => Behaviour = gameObject;

        public void Interact(ILightInteractable interactable) {
            StartCoroutine(interactable.HandleInteraction(this));
        }

        public void HandleDeactivation(LightDivider lightDivider) {
            GetComponent<LightInteractionTracker>().HandleDeactivation(lightDivider);
            HandleBlockedInteraction();
        }

        public void HandleReactivation() {
            gameObject.SetActive(true);
        }

        public void HandleBlockedInteraction() {
            GetComponent<LightMovement>().enabled = false;
        }

        public void HandleUnblockedInteraction() {
            GetComponent<LightMovement>().enabled = true;
        }

        private void OnCollisionEnter2D(Collision2D other) {
            var interactable = other.gameObject.GetComponent<ILightInteractable>();
            if (interactable == null) return;

            Interact(interactable);
        }

        private void OnTriggerEnter2D(Collider2D other) {
            var interactable = other.gameObject.GetComponent<ILightInteractable>();
            if (interactable == null) return;

            Interact(interactable);
        }
    }
}