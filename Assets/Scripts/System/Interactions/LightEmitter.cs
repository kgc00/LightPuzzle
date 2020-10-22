using System.Collections;
using System.Collections.Generic;
using System.Interfaces;
using Models;
using UnityEngine;

namespace System.Interactions {
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class LightEmitter : MonoBehaviour, IInteractable, IInteractionHistoryProvider, ILightColorProvider {
        public bool repeat;
        private bool emitting;
        [SerializeField] private GameObject lightPrefab;
        private GameObject lightInstance;
        private const float Lifetime = 2f;
        private bool active = true;
        [SerializeField] private LightColor colorToProvide;
        public LightColor ColorToProvide => colorToProvide;

        private void Start() {
            StartCoroutine(SpawnLight());
        }

        private IEnumerator SpawnLight() {
            if (!active) yield break;

            Vector3 spawnPos = transform.position + transform.up;
            spawnPos.z = 0f;

            Destroy(lightInstance);
            lightInstance = Instantiate(lightPrefab, spawnPos, GetLightRotation());
            lightInstance.transform.SetParent(transform);
            lightInstance.GetComponent<LightInteractor>().LightColor = colorToProvide;

            yield return new WaitForSeconds(Lifetime);

            if (!repeat) yield break;
            
            StartCoroutine(SpawnLight());
        }

        private Quaternion GetLightRotation() => Quaternion.Euler(transform.eulerAngles);


        public IEnumerator HandleInteraction() {
            active = !active;
            if (!active) {
                StopAllCoroutines();
            }
            else {
                StartCoroutine(SpawnLight());
            }
            
            yield break;
        }

        public InteractionEvent TrackInteraction(IInteractionTracker tracker) {
            return new InteractionEvent(transform, GetType(), colorToProvide);
        }

        public void SetLightColor(ILightColor light) {
            light.LightColor = ColorToProvide;
        }
    }
}