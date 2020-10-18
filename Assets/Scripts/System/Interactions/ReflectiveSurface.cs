using System.Collections;
using System.Interfaces;
using Models;
using UnityEngine;

namespace System.Interactions {
    public class ReflectiveSurface : MonoBehaviour, IReflectiveSurface, IInteractionHistoryProvider {
        public IEnumerator Reflect(LightReflect light) {
            while (Vector3.Distance(light.transform.position, transform.position) > .05f) {
                yield return new WaitForEndOfFrame();
            }

            light.transform.SetPositionAndRotation(transform.position, transform.rotation);
        }

        public InteractionEvent TrackInteraction(IInteractionTracker tracker) {
            return new InteractionEvent {
                position = transform.position,
                eulerRotation = transform.eulerAngles,
                name = gameObject.name,
                type = typeof(ReflectiveSurface)
            };
        }
    }
}