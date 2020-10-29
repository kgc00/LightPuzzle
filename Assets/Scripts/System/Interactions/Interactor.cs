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
            Instantiate(UnityEngine.Resources.Load<GameObject>("Controls UI"));
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
            if (context.phase != InputActionPhase.Performed) return;
            
                LevelManager.Instance.ReloadCurrent();
        }

        public void OnNextLevel(InputAction.CallbackContext context) {
           if (context.phase != InputActionPhase.Performed) return;
         
           LevelManager.Instance.LoadIndex(LevelManager.Instance.CurrentIndex + 1);
        }

        public void OnPreviousLevel(InputAction.CallbackContext context) {
           if (context.phase != InputActionPhase.Performed) return;
            
                LevelManager.Instance.LoadIndex(LevelManager.Instance.CurrentIndex - 1);
        }

        public void OnMainMenu(InputAction.CallbackContext context) {
           if (context.phase != InputActionPhase.Performed) return;
            
                LevelManager.Instance.LoadMenu();
        }
    }
}