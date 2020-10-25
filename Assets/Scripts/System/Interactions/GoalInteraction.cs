using System.Collections;
using System.Interfaces;
using UnityEngine;

namespace System.Interactions {
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class GoalInteraction : MonoBehaviour, ILightInteractable {

        public IEnumerator HandleInteraction(ILightInteractor interactor) {
            LevelManager.Instance.LoadNext();
            yield break;
        }
    }
}