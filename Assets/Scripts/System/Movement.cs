using LightPuzzleUtils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace System {
    [RequireComponent(typeof(Rigidbody2D))]
    public class Movement : MonoBehaviour, PlayerAction.IGameplayActions {
        private Rigidbody2D rig;
        private Vector2 relativeForce;
        private Vector2 moveDir;
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
            rig = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate() {
            relativeForce = Vector2.zero;
            relativeForce += Force();

            rig.AddForce(relativeForce, ForceMode2D.Force);

            var noInputX = moveDir.x == 0;
            var noInputY = moveDir.y == 0;
            if (noInputX || noInputY) {
                rig.velocity = new Vector2(noInputX ? 0 : rig.velocity.x, noInputY ? 0 : rig.velocity.y);
            }
        }

        [SerializeField] public float forceDef;
        [SerializeField] public float velCapDef;

        private Vector2 Force() => Helpers.ProvideCappedForceV2(new Vector2(forceDef, forceDef),
            new Vector2(velCapDef, velCapDef), moveDir, rig.velocity);

        public void OnMove(InputAction.CallbackContext context) {
            if (context.phase != InputActionPhase.Started) {
                moveDir = context.ReadValue<Vector2>();
            }
        }

        public void OnInteract(InputAction.CallbackContext context) { }
        public void OnRestart(InputAction.CallbackContext context) { }
    }
}