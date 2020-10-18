using UnityEngine;

namespace System {
    [RequireComponent(typeof(Rigidbody2D))]
    public class LightMovement : MonoBehaviour {
        private Rigidbody2D rig;
        [SerializeField] private float speed = 120f;
        private void Awake() {
            rig = GetComponent<Rigidbody2D>();
        }

        private void OnDisable() {
            rig.velocity = Vector2.zero;
        }

        private void FixedUpdate() {
            rig.velocity = transform.up * (speed * Time.fixedDeltaTime) ; 
        }

        private void OnGUI() {
            Debug.DrawLine(transform.position, transform.position + (transform.up * 2));
        }
    }
}