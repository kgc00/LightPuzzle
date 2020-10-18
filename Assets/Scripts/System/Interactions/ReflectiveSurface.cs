using System.Collections;
using System.Interfaces;
using UnityEngine;

namespace System.Interactions {
    public class ReflectiveSurface : MonoBehaviour, IReflectiveSurface {
        public IEnumerator Reflect(LightReflect light) {
            while (Vector3.Distance(light.transform.position, transform.position) > .05f) {
                yield return new WaitForEndOfFrame();
            }

            light.transform.SetPositionAndRotation(transform.position, transform.rotation);
        }
    }
}