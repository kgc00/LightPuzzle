using System.Collections;
using System.Interactions;
using System.Interfaces;
using Entity;
using Models;
using UnityEngine;

namespace System {
    [RequireComponent(typeof(CircleCollider2D))]
    public class LightReflect : MonoBehaviour {
        private void OnCollisionEnter2D(Collision2D other) {
            if (other.gameObject.CompareTag(Tags.Board)) Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other) {
            var surface = other.GetComponent<ReflectiveSurface>();
            if (surface == null) return;

            StartCoroutine(Reflect(surface));
        }

        private IEnumerator Reflect(ReflectiveSurface surface) {
            while (Vector3.Distance(transform.position, surface.transform.position) > .01f) {
                yield return new WaitForEndOfFrame();
            }

            transform.SetPositionAndRotation(surface.transform.position, surface.transform.rotation);
        }
    }
}