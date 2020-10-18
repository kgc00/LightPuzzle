using System.Interfaces;
using Models;
using UnityEngine;

namespace System.Interactions {
    [RequireComponent(typeof(CircleCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class LightInteractor : MonoBehaviour, ILightInteractor {
        public GameObject Behaviour { get; private set; }
        public LightColor Color { get; private set; } = LightColor.White;

        private void Awake() => Behaviour = gameObject;

        public void Interact(ILightInteractable interactable) {
            StartCoroutine(interactable.HandleInteraction(this));
        }

        private void OnTriggerEnter2D(Collider2D other) {
            var interactable = other.gameObject.GetComponent<ILightInteractable>();
            if (interactable == null) return;

            Interact(interactable);
        }
    }
}