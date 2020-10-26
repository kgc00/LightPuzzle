using System.Collections;
using System.Interfaces;
using Models;
using UnityEngine;

namespace System.Interactions {
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    // TODO - disable / enable via player interaction, e.g. IInteractable
    public class LightDivider : MonoBehaviour, ILightInteractable, IInteractionHistoryProvider {
        [SerializeField] private GameObject lightPrefab;

        private void Awake() {
            InteractionObserver.OnDividerInteractionRemoved += HandleInteractionRemoved;
        }

        private void OnDestroy() {
            InteractionObserver.OnDividerInteractionRemoved -= HandleInteractionRemoved;
        }

        private void HandleInteractionRemoved(Vector3 interactableSnappedPos, ILightInteractor interactor) {
            // if(interactableSnappedPos == transform.position.Snapped())
            //     interactor.HandleReactivation(this);
        }

        public IEnumerator HandleInteraction(ILightInteractor interactor) {
            var hist = interactor.Behaviour.GetComponent<LightInteractionTracker>().History;
            if (hist != null && hist.Count > 0) {
                if (hist[0].Type == GetType() &&
                    hist[0].InteractableSnappedPosition == transform.position.Snapped()) yield break;
            }

            while (Vector3.Distance(transform.position, interactor.Behaviour.transform.position) > .01f) {
                yield return new WaitForEndOfFrame();
                // case where something spawned too close and entered this coroutine
                if (Vector3.Distance(transform.position, interactor.Behaviour.transform.position) > 1.5f) yield break;
            }

            SpawnLight(interactor);

            interactor.HandleDeactivation(this);
        }

        private void SpawnLight(ILightInteractor interactor) {
            var lightHistory = interactor.Behaviour.GetComponent<IInteractionTracker>().History;
            var lightColor = interactor.Behaviour.GetComponent<ILightColor>().LightColor;

            var lightInstance = Instantiate(lightPrefab, transform.position, GetLightRotationPositive());
            lightInstance.transform.SetParent(transform);
            lightInstance.GetComponent<IInteractionTracker>().InitializeHistory(lightHistory);
            lightInstance.GetComponent<ILightColor>().UpdateLightColor(lightColor);

            var otherLightInstance = Instantiate(lightPrefab, transform.position, GetLightRotationNegative());
            otherLightInstance.transform.SetParent(transform);
            otherLightInstance.GetComponent<IInteractionTracker>().InitializeHistory(lightHistory);
            otherLightInstance.GetComponent<ILightColor>().UpdateLightColor(lightColor);
        }

        // - 90 on z to get the right hand / left hand side
        private Quaternion GetLightRotationPositive() =>
            Quaternion.Euler(transform.rotation.eulerAngles - new Vector3(0, 0, 90f));

        private Quaternion GetLightRotationNegative() =>
            Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, 0, 90f));

        public InteractionEvent TrackInteraction(IInteractionTracker tracker) {
            return new InteractionEvent(transform, GetType());
        }
    }
}