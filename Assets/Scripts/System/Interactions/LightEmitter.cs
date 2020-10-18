using System.Collections;
using System.Interfaces;
using UnityEngine;

namespace System.Interactions {
    public class LightEmitter : MonoBehaviour, IInteractable {
        private bool emitting;
        [SerializeField] private GameObject lightPrefab;
        private GameObject lightInstance;
        private const float Lifetime = 2f;

        private void Start() {
            StartCoroutine(SpawnLight());
        }

        private IEnumerator SpawnLight() {
            Vector3 spawnPos = transform.position + transform.up;
            spawnPos.z = 0f;
            
            Destroy(lightInstance);
            lightInstance = Instantiate(lightPrefab, spawnPos, GetLightRotation());
            lightInstance.transform.SetParent(transform);

            yield return new WaitForSeconds(Lifetime);

            StartCoroutine(SpawnLight());
        }

        private Quaternion GetLightRotation() => Quaternion.Euler(transform.eulerAngles);


        public IEnumerator HandleInteraction() {
        yield break;
        }
    }
}
