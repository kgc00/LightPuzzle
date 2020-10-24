using System.Collections;
using System.Interfaces;
using Models;
using UnityEngine;

namespace System.Interactions {
    public class MirrorSurface : MonoBehaviour, IReflectiveSurface, IInteractionHistoryProvider {
        [field: SerializeField] public readonly string ReadOnlyTest = "Guh";
        public IEnumerator Reflect(LightReflect light) {
            while (Vector3.Distance(light.transform.position, transform.position) > .05f) {
                yield return new WaitForEndOfFrame();
            }

            light.transform.SetPositionAndRotation(transform.position, transform.rotation);
        }

        public InteractionEvent TrackInteraction(IInteractionTracker tracker) {
            return new InteractionEvent(transform, GetType());

        }
    }
}