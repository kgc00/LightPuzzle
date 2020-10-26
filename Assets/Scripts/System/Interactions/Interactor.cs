using System.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace System.Interactions {
    [RequireComponent(typeof(InteractableGatherer))]
    public class Interactor : MonoBehaviour, IInteractor, PlayerAction.IGameplayActions {
        [SerializeField] private InteractableGatherer interactableGatherer;

        private PlayerAction controls;
        private void OnEnable() {
            if (controls == null) {
                controls = new PlayerAction();
                controls.Gameplay.SetCallbacks(this);
            }

            controls.Gameplay.Enable();
        }

        private void OnDisable() {
            controls.Gameplay.Disable();
        }

        private void Awake() {
            interactableGatherer = GetComponent<InteractableGatherer>();
        }

        public void Interact() {
            foreach (var interactable in interactableGatherer.interactables) {
                StartCoroutine(interactable.HandleInteraction());
            }
        }

        public void OnMove(InputAction.CallbackContext context) { }

        public void OnInteract(InputAction.CallbackContext context) {
            if (context.phase != InputActionPhase.Performed) return;
            Interact();
        }

        public void OnRestart(InputAction.CallbackContext context) {
            if (context.phase != InputActionPhase.Started) {
                LevelManager.Instance.ReloadCurrent();
            }
        }
    }
}