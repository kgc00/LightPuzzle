using System.Collections;
using System.Interfaces;
using UnityEngine;

namespace System.Interactions {
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class SoftGate : MonoBehaviour, ILightInteractable {
        public IEnumerator HandleInteraction(ILightInteractor interactor) {
            Destroy(gameObject);
            yield break;
        }
    }
}