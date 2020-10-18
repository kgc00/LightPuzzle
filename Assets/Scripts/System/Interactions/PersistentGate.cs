using System.Collections;
using System.Interfaces;
using UnityEngine;

namespace System.Interactions {
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PersistentGate : MonoBehaviour, ILightInteractable {
        
        public IEnumerator HandleInteraction(ILightInteractor interactor) {
            interactorCount++;
            if (!HasBeenUnlockd()) yield break;
            gameObject.SetActive(false);
            yield break;
        }

        private int interactorCount;
        private bool HasBeenUnlockd() {
            return interactorCount > 1;
        }
    }
}