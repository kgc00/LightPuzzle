using System.Collections;
using System.Collections.Generic;
using System.Interfaces;
using LightPuzzleUtils;
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
        [SerializeField] private LightColor colorToProvide;
        public LightColor ColorToProvide => colorToProvide;

        private void Start() {
            StartCoroutine(SpawnLight());
            var renderer = GetComponent<SpriteRenderer>();
            renderer.color = Helpers.ColorFromLightColor(colorToProvide);
        }

        private IEnumerator SpawnLight() {
            if (!active) yield break;

            Vector3 spawnPos = transform.position;
            spawnPos.z = 0f;

            Destroy(lightInstance);
            lightInstance = Instantiate(lightPrefab, spawnPos, GetLightRotation());
            lightInstance.transform.SetParent(transform);
            lightInstance.GetComponent<ILightColor>().UpdateLightColor(colorToProvide);

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
    }
}