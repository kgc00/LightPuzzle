using System.Collections;
using System.Collections.Generic;
using System.Interfaces;
using Models;
using UnityEngine;

namespace System.Interactions {
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class LightEmitter : MonoBehaviour, IInteractable, IInteractionHistoryProvider {
        public bool repeat;
        private bool emitting;
        [SerializeField] private GameObject lightPrefab;
        private GameObject lightInstance;
        private const float Lifetime = 2f;
        private bool active = true;

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
            return new InteractionEvent {
                position = transform.position.Snapped(),
                eulerRotation = transform.eulerAngles,
                name = gameObject.name,
                type = typeof(LightEmitter)
            };
        }
    }
}