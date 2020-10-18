using Cinemachine;
using UnityEngine;

namespace System {
    public class SetVirtualCamFollowTarget : MonoBehaviour {
        void Awake() {
            var vCam = FindObjectOfType<CinemachineVirtualCamera>();
            if (!vCam) return;
            vCam.Follow = gameObject.transform;
        }
    }
}