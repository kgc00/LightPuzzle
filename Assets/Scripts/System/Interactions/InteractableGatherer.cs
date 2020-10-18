using System.Collections.Generic;
using System.Interfaces;
using UnityEngine;

namespace System.Interactions {
    [RequireComponent(typeof(CircleCollider2D))]
    public class InteractableGatherer : MonoBehaviour {
         public List<IInteractable> interactables { get; private set; }
        [SerializeField] private CircleCollider2D collider;

        private void Awake() {
            collider = GetComponent<CircleCollider2D>();
        }

        private void OnEnable() {
            interactables = new List<IInteractable>();
        }

        private void OnDisable() {
            interactables.Clear();
            interactables = null;
        }

        private void OnTriggerEnter2D(Collider2D other) {
            var interactable = (IInteractable) other.gameObject.GetComponent(typeof(IInteractable));

            if (interactable == null) return;
            interactables.Add(interactable);
        }
        
        private void OnTriggerExit2D(Collider2D other) {
            var interactable = (IInteractable) other.gameObject.GetComponent(typeof(IInteractable));
            if (interactable == null) return;
            interactables.Remove(interactable);
        }

        private void OnGUI() {
            GUILayout.Box($"interactables: {interactables.Count}");
        }
    }
}