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

        public IEnumerator HandleInteraction(ILightInteractor interactor) {
            
            while (Vector3.Distance(transform.position, interactor.Behaviour.transform.position) > .01f) {
                yield return new WaitForEndOfFrame();
                // case where something spawned too close and entered this coroutine
                if (Vector3.Distance(transform.position, interactor.Behaviour.transform.position) > 1.5f) yield break;
            }
            SpawnLight(interactor);
            Destroy(interactor.Behaviour);
        }

        private void SpawnLight(ILightInteractor interactor) {
            Vector3 spawnPos = transform.position + transform.up * 1.1f;
            var spawnNegPos = transform.position + (transform.up * -1.1f);
            spawnPos.z = 0f;
            spawnNegPos.z = 0f;

            var lightInstance = Instantiate(lightPrefab, spawnPos, GetLightRotationPositive());
            lightInstance.transform.SetParent(transform);
            
            var otherLightInstance = Instantiate(lightPrefab, spawnNegPos, GetLightRotationNegative());
            otherLightInstance.transform.SetParent(transform);
        }

        private Quaternion GetLightRotationPositive() => Quaternion.Euler(transform.eulerAngles);

        private Quaternion GetLightRotationNegative() => Quaternion.Inverse(GetLightRotationPositive());
        
        public InteractionEvent TrackInteraction(IInteractionTracker tracker) {
            return new InteractionEvent(transform, GetType());

        }
    }
}