using System.Collections;
using System.Interactions;
using System.Interfaces;
using Models;
using UnityEngine;

namespace System {
    [RequireComponent(typeof(CircleCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class LightReflect : MonoBehaviour {
        private void OnTriggerEnter2D(Collider2D other) {
            if (other.gameObject.CompareTag(Tags.Board)) Destroy(gameObject);
            var surface = other.GetComponent<IReflectiveSurface>();
            if (surface == null) return;

            StartCoroutine(surface.Reflect(this));
        }
    }
}